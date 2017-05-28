using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Media;

namespace Pomodoro {
    public partial class Pauza : Form {

        Clock clock;
        public int counter;
        public int progress;
        public string soundPath; 

        public Pauza(int countPomodoros, Color back, Color font,string soundPath) {
            InitializeComponent();
            if(soundPath == null) {
                this.soundPath = @"C:\Windows\Media\Alarm10.wav";
            }
            else {
                this.soundPath = @soundPath;
            }
            if(countPomodoros % 4 == 0 && countPomodoros != 0) {
                clock = new Clock(30);
                counter = 18000;
            }
            else {
                clock = new Clock(5);
                counter = 3000;
            }

            label1.Text = clock.ToString();
            progress = pbTime.progress;
            this.BackColor = back;
            this.ForeColor = font;
            pbTime.color = font;

        }


        public void ProgressUpgrade(object progress) {
            if(!this.IsHandleCreated) {
                this.CreateHandle();
            }
            pbTime.Invoke((MethodInvoker)delegate { pbTime.upgradeProgress(Convert.ToInt32(progress)); });
        }

        
        private void btnPause_Click(object sender, EventArgs e) {

            timer1.Start();

            Task.Factory.StartNew(() => {

                for(int num = progress; num <= 100; num++) {

                    new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.ProgressUpgrade)).Start(num);

                    System.Threading.Thread.Sleep(counter);
                }
            });

            btnPause.Enabled = false;
        }

       

        private void Pauza_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            this.Hide();
        }

        private void timer1_Tick_1(object sender, EventArgs e) {
            if(pbTime.progress == 100) {
                timer1.Stop();
                using(var soundPlayer = new SoundPlayer(soundPath)) {
                    soundPlayer.Play();
                }
            }
            clock.clockTick();
            label1.Text = clock.ToString();
        }
    }
}
