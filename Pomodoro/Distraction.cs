using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pomodoro {

    [Serializable]
    public partial class Distraction : Form {
        public DistractionClass DistractionClass { get; set; }
        public Distraction(Color back, Color front, Color panel) {
            InitializeComponent();
            AcceptButton = btnAdd;
            CancelButton = btnCancel;
            this.BackColor = back;
            this.ForeColor = front;
            tbDistraction.BackColor = panel;
        }

        public Distraction() {
            InitializeComponent();
            AcceptButton = btnAdd;
            CancelButton = btnCancel;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            DistractionClass = new DistractionClass(tbDistraction.Text);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        
    }
}
