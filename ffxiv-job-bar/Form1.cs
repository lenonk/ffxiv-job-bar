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
    public partial class MainForm : Form
    {
        private bool minimized = false; 
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        public MainForm() {
            InitializeComponent();
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
        }

        private void MinimizeButton_Click(object sender, EventArgs e) {
            if (minimized) {
                minimized = false;
                this.Height = 51;
                this.Width = 388;
            }
            else {
                minimized = true;
                this.Height = 10;
                this.Width = 10;
            }
        }

        private void NotesButton_Click(object sender, EventArgs e) {
            if (this.Height == 281)
                this.Height = 51;
            else
                this.Height = 281;
        }

        private void closeButton_Click(object sender, EventArgs e) {
            System.Windows.Forms.Application.Exit();
        }

        //call functions to move the form in your form's MouseDown event
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                NativeImports.ReleaseCapture();
                NativeImports.SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                //sf.Save();
            }
        }
    }
}
