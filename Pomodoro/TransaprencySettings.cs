﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pomodoro
{
    public partial class TransaprencySettings : Form
    {
        public int quantity;

        public TransaprencySettings()
        {
            InitializeComponent();
        }

        private void tbTransparency_Scroll(object sender, EventArgs e)
        {
            quantity = tbTransparency.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            quantity = tbTransparency.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void tbTransparency_ValueChanged(object sender, EventArgs e)
        {
            quantity = tbTransparency.Value;
        }
    }
}
