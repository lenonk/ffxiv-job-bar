using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace ffxiv_job_bar
{
    public class JobButton : Button
    {
        private Dictionary<string, string> itemDict = new Dictionary<string, string>();
        private MainForm owner;
        private Thread jobChangeThread;

        public bool use_job;

        public JobButton(MainForm owner) {
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(configureButtonClick);
            this.owner = owner;
        }

        public void LoadSettings() {
            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");
            DataTable settings;

            String query = "select * from button_labels where button_name = \"" + Name + "\";";
            settings = db.GetDataTable(query);

            foreach (DataRow r in settings.Rows) {
                Text = r["button_label"].ToString();
                use_job = Convert.ToBoolean(r["use_job"].ToString());
            }
        }

        private void configureButtonClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                MainForm.jcf.SetOwner(this);
                MainForm.jcf.ShowDialog();
            }
            else if (e.Button == MouseButtons.Left) {
                owner.activeButton = this;

                jobChangeThread = new Thread(new ThreadStart(ChangeJob));
                jobChangeThread.IsBackground = true;
                jobChangeThread.Start();

                // Get a handle to the FFXIV application. The window class
                // and window name were obtained using the Spy++ tool.
                IntPtr ffHandle = NativeImports.FindWindow("RAPTURE", "FINAL FANTASY XIV");

                if (ffHandle != IntPtr.Zero) {
                    // Make FFXIV the foreground application and send it 
                    // a set of calculations.
                    NativeImports.SetForegroundWindow(ffHandle);

                }
            }
        }

        public void CancelJobChange() {
            jobChangeThread.Abort();
        }

        delegate void ToggleButtonStatesDelegate();

        private void ToggleButtonStates() {
            if (owner.InvokeRequired) {
                ToggleButtonStatesDelegate del = new ToggleButtonStatesDelegate(ToggleButtonStates);
                owner.Invoke(del);
            }
            else {
                owner.ToggleJobButtons();
                owner.ToggleStopButton();
            }
        }

        private void ChangeJob() {
            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");
            DataTable settings;
            
            IntPtr ffHandle = NativeImports.FindWindow("RAPTURE", "FINAL FANTASY XIV");

            if (ffHandle == IntPtr.Zero) {
                MessageBox.Show("Final Fantasy is not running.");
                return;
            }

            String query = 
                String.Format("select position, value from buttons where position = 'mh' " +
                    "and button_name = '{0}';", Name);
            settings = db.GetDataTable(query);
            if (settings.Rows.Count <= 0)
                return;
            if (settings.Rows[0]["value"].ToString().Trim() == "") {
                db.Delete("buttons", String.Format("button_name = \"{0}\"", Name));
                return;
            }
            ToggleButtonStates();
            SendMessage(ffHandle, String.Format("/equip mh \"{0}\"", settings.Rows[0]["value"].ToString()));
            if (use_job) {
                SendMessage(ffHandle, "/job on");
                Thread.Sleep(Math.Max(owner.actionWait * 3, 1000));
            }
            else {
                SendMessage(ffHandle, "/job off");
            }

            query = "select position, value from buttons where button_name = '" + Name + 
                "' and position != 'mh';";
            settings = db.GetDataTable(query);

            foreach (DataRow r in settings.Rows) {
                String gearName = "";
                if (r["value"].ToString().Trim() == "")
                    continue;
                if (r["value"].ToString().Trim().ToLower() == "none") {
                    gearName = "";
                }
                else {
                    gearName = r["value"].ToString();
                }

                String equip = String.Format("/equip {0} \"{1}\"", r["position"], gearName);
                SendMessage(ffHandle, equip);
                if (r["position"].ToString() == "L.ring")
                    Thread.Sleep(Math.Max(owner.actionWait * 3, 1000));
                else
                    Thread.Sleep(owner.actionWait);
            }
            ToggleButtonStates();
        }

        private void SendMessage(IntPtr handle, string message) {
            const uint WM_CHAR = 0x102;
            const uint WM_KEYDOWN = 0x100;
            const uint WM_KEYUP = 0x101;

            foreach (char c in message) {
                IntPtr ch = (IntPtr)((int)c);
                NativeImports.PostMessage(handle, WM_CHAR, ch, IntPtr.Zero);
            }
            IntPtr enter = (IntPtr)((int)0x0d);
            NativeImports.PostMessage(handle, WM_KEYDOWN, enter, IntPtr.Zero);
            NativeImports.PostMessage(handle, WM_KEYUP, enter, IntPtr.Zero);
        }
    }
}
