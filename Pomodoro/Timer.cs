using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Documents;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Media;

namespace Pomodoro {
    [Serializable]
    public partial class Timer : Form {

        public ToDo ToDo;
        Clock clock;
        public List<string> todos;
        public List<string> completedTodos;
        public List<string> checkedItems;
        bool nightMode;
        bool flag ;
        //public bool flag;
        public int progress;
        public int counter;
        public int tempCounter;
        public bool wasPaused;
        public string FileName;
        public string soundPath;

        public Timer() {
            InitializeComponent();
            clock = new Clock();
            //label2.Text = clock.ToString();            
            todos = new List<string>();
            completedTodos = new List<string>();
            checkedItems = new List<string>();
            wasPaused = false;
            flag = false;
            nightMode = false;
            progress = 0;
            counter = 15000;
            tempCounter = 15000;
            ToDo = new ToDo();
            FileName = null;
            this.BackColor = Color.FromArgb(162, 185, 214);
            pbTime.color = Color.FromArgb(56, 93, 122);
            menuStrip1.BackColor = this.BackColor;
        }

        private void btnAddDistraction_Click(object sender, EventArgs e) {
            Distraction distraction = new Distraction(this.BackColor,pbTime.color,clbDistractions.BackColor);
            if(distraction.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                clbDistractions.Items.Add(distraction.DistractionClass.text);
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e) {
            flag = !flag;
            if(flag) {
                btnStartStop.Text = "Pause";
                timer1.Start();
            }
            else {
                btnStartStop.Text = "Start";
                timer1.Stop();
            }

            if(clock.actualSec == 0 && clock.minutes == 0) {
                clock.restart();
                if(soundPath == null) {
                    using(var soundPlayer = new SoundPlayer(@"C:\Windows\Media\Alarm10.wav")) {
                        soundPlayer.Play();
                    }
                }
                else {
                    using(var soundPlayer = new SoundPlayer(@soundPath)) {
                        soundPlayer.Play();
                    }
                }
                label2.Text = clock.ToString();
            }

            Task.Factory.StartNew(() => {

                 for(int num = progress; num <= 100; num++) {

                     if(!flag) {
                         wasPaused = true;
                     }
                     else {
                         new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.ProgressUpgrade)).Start(num);

                         if(wasPaused) {
                             System.Threading.Thread.Sleep(tempCounter);
                             wasPaused = false;
                             //counter = tempCounter;
                         }
                         else {
                             System.Threading.Thread.Sleep(counter);
                         }

                     }
                 }

             });

            progress = pbTime.progress;
        }

        public void ProgressUpgrade(object progress) {
            if(!this.IsHandleCreated) {
                this.CreateHandle();
            }
            pbTime.Invoke((MethodInvoker)delegate { pbTime.upgradeProgress(Convert.ToInt32(progress)); });
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if(pbTime.progress == 100)
                timer1.Stop();
            clock.clockTick();
            label2.Text = clock.ToString();

            if(clock.actualSec % 15 == 0) {
                tempCounter = 15000;
                //wasPaused = false;
            }
            if(tempCounter >= 1000)
                tempCounter -= 1000;
            else {
                tempCounter = 15000;
                // wasPaused = false;
            }
        }

        private void Timer_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }

       private void saveFile() {
            if(FileName == null) {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "ToDo doc file (*.todo)|*.todo";
                saveFileDialog.Title = "Save ToDo list";
                if(saveFileDialog.ShowDialog() == DialogResult.OK) {
                    FileName = saveFileDialog.FileName;
                }
            }
            if(FileName != null) {
                using(FileStream fileStream = new FileStream(FileName, FileMode.Create)) {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, this);
                }
            }
        }

        private void toDoListToolStripMenuItem_Click(object sender, EventArgs e) {
            ToDo todo = new ToDo(todos, checkedItems, this.BackColor, pbTime.color, clbDistractions.BackColor);

            if(todo.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                todos = todo.todos;
                checkedItems = todo.checkedItems;
            }
                
        }

        private void Timer_Load(object sender, EventArgs e) {

        }

        private void nightModeToolStripMenuItem_Click_1(object sender, EventArgs e) {
            nightMode = !nightMode;
            if(nightMode) {
                this.BackColor = Color.Gray;
                pbTime.color = Color.Black;
                nightModeToolStripMenuItem.Text = "Day Mode";
            }
            else {
                this.BackColor = Color.FromArgb(162, 185, 214);
                pbTime.color = Color.FromArgb(56, 93, 122);
                nightModeToolStripMenuItem.Text = "Night Mode";

            }
        }

        private void colorsToolStripMenuItem_Click_1(object sender, EventArgs e) {
            if(colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                this.BackColor = colorDialog1.Color;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("The Pomodoro Technique is a time management method developed by Francesco Cirillo in the late 1980s. The technique uses a timer to break down work into intervals, traditionally 25 minutes in length, separated by short breaks. These intervals are named pomodoros, the plural in English of the Italian word pomodoro (tomato), after the tomato-shaped kitchen timer that Cirillo used as a university student.Closely related to concepts such as timeboxing and iterative and incremental development used in software design, the method has been adopted in pair programming contexts.", "What is Pomodoro?",MessageBoxButtons.OK);
        }

        private void soundsToolStripMenuItem_Click(object sender, EventArgs e) {
            openFile();
        }


        private void openFile() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Select sound (*.wav)|*.wav|(*.mp3)|*.mp3";
            openFileDialog.Title = "Select sound";
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                FileName = openFileDialog.FileName;
                try {
                    soundPath = Path.GetFullPath(FileName);
                }
                catch(Exception ex) {
                    MessageBox.Show("Could not read file: " + FileName);
                    FileName = null;
                    return;
                }
                Invalidate(true);
            }
        }

        
       

        
    }
}
