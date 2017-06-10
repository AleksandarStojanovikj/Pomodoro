namespace Pomodoro
{
    partial class TransaprencySettings
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
            this.tbTransparency = new System.Windows.Forms.TrackBar();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // tbTransparency
            // 
            this.tbTransparency.LargeChange = 10;
            this.tbTransparency.Location = new System.Drawing.Point(24, 23);
            this.tbTransparency.Maximum = 100;
            this.tbTransparency.Name = "tbTransparency";
            this.tbTransparency.Size = new System.Drawing.Size(293, 56);
            this.tbTransparency.TabIndex = 0;
            this.tbTransparency.Scroll += new System.EventHandler(this.tbTransparency_Scroll);
            this.tbTransparency.ValueChanged += new System.EventHandler(this.tbTransparency_ValueChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(323, 23);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // TransaprencySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 85);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbTransparency);
            this.Name = "TransaprencySettings";
            this.Text = "TransaprencySettings";
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbTransparency;
        private System.Windows.Forms.Button btnOK;
    }
}