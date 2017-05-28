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

namespace Pomodoro {
    public partial class Timer : Form {

        Clock clock;
        public List<string> todos;
        public List<string> completedTodos;
       
        bool nightMode;
        bool flag ;
        //public bool flag;
        public int progress;
        public int counter;
        public int tempCounter;
        public bool wasPaused;

        public Timer() {
            InitializeComponent();
            clock = new Clock();
            //label2.Text = clock.ToString();            
            todos = new List<string>();
            completedTodos = new List<string>();
            wasPaused = false;
            flag = false;
            nightMode = false;
            progress = 0;
            counter = 15000;
            tempCounter = 15000;
            
        }

        private void btnAddDistraction_Click(object sender, EventArgs e) {
            Distraction distraction = new Distraction();
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
        }

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
                this.BackColor = Color.OrangeRed;
                pbTime.color = Color.Orange;
                nightModeToolStripMenuItem.Text = "Night Mode";

            }
        }

        private void toDoListToolStripMenuItem_Click(object sender, EventArgs e) {
            ToDo todo = new ToDo(todos);

            if(todo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                todos = todo.todos;
        }

        private void Timer_Load(object sender, EventArgs e) {

        }
    }
}
