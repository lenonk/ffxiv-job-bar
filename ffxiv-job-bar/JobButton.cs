using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ffxiv_job_bar
{
    class JobButton : Button
    {
        private Dictionary<string, string> itemDict = new Dictionary<string, string>();

        public JobButton() {
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(configureButtonClick);
        }

        private void configureButtonClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                MainForm.jcf.ShowDialog();
            }

            if (e.Button == MouseButtons.Left) {
                //ChangeJob();
            }
        }
    }
}
