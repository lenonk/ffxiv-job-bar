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
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private bool minimized = false;
        private bool hotkeysEnabled;

        public JobButton activeButton = null;
        public static SettingsForm sf;
        public static JobConfigureForm jcf;
        public short actionWait;

        public MainForm() {
            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");

            InitializeComponent();

            db.ExecuteNonQuery("create table if not exists settings(variable text, value text)");
            db.ExecuteNonQuery("create table if not exists items(position text, name text)");
            db.ExecuteNonQuery("create table if not exists button_labels(button_name text, " +
                "button_label text, use_job text)");
            db.ExecuteNonQuery("create table if not exists buttons(button_name text, " +
                "position text, value text)");
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

            // Get a handle to the FFXIV application. The window class
            // and window name were obtained using the Spy++ tool.
            IntPtr ffHandle = NativeImports.FindWindow("RAPTURE", "FINAL FANTASY XIV");

            if (ffHandle != IntPtr.Zero) {
                // Make FFXIV the foreground application and send it 
                // a set of calculations.
                NativeImports.SetForegroundWindow(ffHandle);

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
                sf.Save();
            }

            // Get a handle to the FFXIV application. The window class
            // and window name were obtained using the Spy++ tool.
            IntPtr ffHandle = NativeImports.FindWindow("RAPTURE", "FINAL FANTASY XIV");

            if (ffHandle != IntPtr.Zero) {
                // Make FFXIV the foreground application and send it 
                // a set of calculations.
                NativeImports.SetForegroundWindow(ffHandle);

            }
        }

        private void SettingsButton_Click(object sender, EventArgs e) {
            sf.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");
            DataTable settings;
            
            String query = "Select * from settings;";
            settings = db.GetDataTable(query);

            int x = 0, y = 0;
            foreach (DataRow r in settings.Rows) {
                switch (r["variable"].ToString()) {
                    case "opacity":
                        this.Opacity = Convert.ToDouble(r["value"].ToString()) / 100;
                        break;
                    case "locationX":
                        x = Convert.ToInt32(r["value"].ToString());
                        break;
                    case "locationY":
                        y = Convert.ToInt32(r["value"].ToString());
                        break;
                    case "actionwait":
                        this.actionWait = Convert.ToInt16(r["value"].ToString());
                        break;
                    case "hotkeysenabled":
                        this.hotkeysEnabled = Convert.ToBoolean(r["value"].ToString());
                        break;
                }
            }
            this.Location = new Point(x, y);
            this.Refresh();

            // Create all the job buttons
            sf = new SettingsForm(this);
            jcf = new JobConfigureForm();

            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            sf.ItemDataChanged += new ItemDataChangedHandler(jcf.LoadItemData);
            Point point = new Point(0, 10);
            for (int i = 0; i < 20; i++) {
                JobButton b = new JobButton(this);

                if ((i > 0) && ((i % 10) == 0)) {
                    point.Y += 20;
                    point.X = 0;
                }
                else if (i > 0) {
                    point.X += 34;
                }

                b.Font = new System.Drawing.Font("Arial", 5.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                b.Location = point;
                b.Name = "JobButton" + (i + 1);
                b.Size = new System.Drawing.Size(35, 21);
                b.TabIndex = i;
                b.UseVisualStyleBackColor = true;
                panel2.Controls.Add(b);
                b.LoadSettings();
            }
        }

        public void ToggleJobButtons() {
            panel2.Enabled = !panel2.Enabled; 
        }

        public void ToggleStopButton() {
            StopButton.Enabled = !StopButton.Enabled;
        }

        private void StopButton_Click(object sender, EventArgs e) {
            activeButton.CancelJobChange();
            panel2.Enabled = true;
            StopButton.Enabled = false;
        }
    }
}
