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

            if(pbTime.progress == 100) {
                clock.restart();
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

            /*string filepath = @"C:\";
            string filename = "datasaved.bin";
            ToDo something = new ToDo();

            // serialize
            using(FileStream strm = File.OpenWrite(Path.Combine(filepath, filename))) {
                BinaryFormatter ser = new BinaryFormatter();
                ser.Serialize(strm, something);
            }

            // deserialize
            using(FileStream strm = File.OpenRead(Path.Combine(filepath, filename))) {
                BinaryFormatter ser = new BinaryFormatter();
                something = ser.Deserialize(strm) as ToDo;
            }*/
 

        }

       /* private void saveFile() {
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
                    formatter.Serialize(fileStream, ToDo);
                }
            }
        }*/

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e) {
            if(colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                this.BackColor = colorDialog1.Color;
            }
        }

        private void nightModeToolStripMenuItem_Click(object sender, EventArgs e) {
            nightMode = !nightMode;
            if(nightMode) {
                this.BackColor = Color.Gray;
                pbTime.color = Color.Black;
                nightModeToolStripMenuItem.Text = "Day Mode";
            }
            else {
                this.BackColor = Color.FromArgb(162,185,214);
                pbTime.color = Color.FromArgb(56,93,122);
                nightModeToolStripMenuItem.Text = "Night Mode";

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

        
    }
}
