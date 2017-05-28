namespace Pomodoro
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnMainStart = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnMainStart
            // 
            this.btnMainStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMainStart.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMainStart.ForeColor = System.Drawing.SystemColors.Control;
            this.btnMainStart.Location = new System.Drawing.Point(124, 200);
            this.btnMainStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMainStart.Name = "btnMainStart";
            this.btnMainStart.Size = new System.Drawing.Size(180, 44);
            this.btnMainStart.TabIndex = 0;
            this.btnMainStart.Text = "START";
            this.btnMainStart.UseVisualStyleBackColor = true;
            this.btnMainStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(417, 482);
            this.Controls.Add(this.btnMainStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMainStart;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

