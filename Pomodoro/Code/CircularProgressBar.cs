using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pomodoro.Code {
    
    public partial class CircularProgressBar : UserControl {
        public int progress;
        public Color color = Color.Red;
        public CircularProgressBar() {
            progress = 0;
            //progress = 100;
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        private void CircularProgressBar_Paint(object sender, PaintEventArgs e) {
            e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
            e.Graphics.RotateTransform(-90);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen pen = new Pen(color);
            Rectangle rect1 = new Rectangle(0 - this.Width / 2 + 20, 0 - this.Height / 2 + 20, this.Width - 40, this.Height - 40);
            e.Graphics.DrawPie(pen, rect1, 0, (int)(this.progress * 3.6));
            e.Graphics.FillPie(new SolidBrush(color), rect1, 0, 360 - (int)(this.progress * 3.6));

            pen = new Pen(Color.White);
            rect1 = new Rectangle(0 - this.Width / 2 + 30, 0 - this.Height / 2 + 30, this.Width - 60, this.Height - 60);
            e.Graphics.DrawPie(pen, rect1, 0, 360);
            e.Graphics.FillPie(new SolidBrush(Color.White), rect1, 0, 360);

            e.Graphics.RotateTransform(90);
            StringFormat ft = new StringFormat();
            ft.LineAlignment = StringAlignment.Center;
            ft.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(this.progress.ToString() + "%", new Font("Ariel", 20), new SolidBrush(color), rect1, ft);
        }

        public void upgradeProgress(int progress) {
            this.progress = progress;
            this.Invalidate();
        }
    }
}
