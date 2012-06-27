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
        
        public static SettingsForm sf;
        public static JobConfigureForm jcf;

        public MainForm() {
            InitializeComponent();

            sf = new SettingsForm(this);
            jcf = new JobConfigureForm();

            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);

            Point point = new Point(0, 10);
            for (int x = 0; x < 20; x++) {
                JobButton b = new JobButton();

                if ((x > 0) && ((x % 10) == 0)) {
                    point.Y += 20;
                    point.X = 0;
                }
                else if (x > 0) {
                    point.X += 34;
                }

                b.Font = new System.Drawing.Font("Arial", 5.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                b.Location = point;
                b.Name = "configureButton" + (x + 1);
                b.Size = new System.Drawing.Size(35, 21);
                b.TabIndex = x;
                b.UseVisualStyleBackColor = true;
                this.Controls.Add(b);
                //b.Load();
            }
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

        private void SettingsButton_Click(object sender, EventArgs e) {
            sf.ShowDialog();
        }
    }
}
