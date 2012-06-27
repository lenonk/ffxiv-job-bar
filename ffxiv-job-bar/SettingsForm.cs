using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ffxiv_job_bar
{
    public partial class SettingsForm : Form
    {
        private MainForm parent;

        public SettingsForm(MainForm parent) {
            InitializeComponent();
            this.parent = parent;
        }

        private void opacityChanger_ValueChanged(object sender, EventArgs e) {
            parent.Opacity = (double)opacityChanger.Value / 100;
            parent.Refresh();
        }
    }
}
