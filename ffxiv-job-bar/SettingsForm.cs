using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;

namespace ffxiv_job_bar
{
    public delegate void ItemDataChangedHandler(object sender, EventArgs e);

    public partial class SettingsForm : Form
    {
        public event ItemDataChangedHandler ItemDataChanged;

        private MainForm parent;

        public SettingsForm(MainForm parent) {
            InitializeComponent();
            this.parent = parent;
        }

        protected virtual void OnItemDataChanged(EventArgs e) {
            ItemDataChanged(this, e);
        }

        private void opacityChanger_ValueChanged(object sender, EventArgs e) {
            parent.Opacity = (double)opacityChanger.Value / 100;
            parent.Refresh();
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            Save();
            this.Close();
        }

        public void Save() {
            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");
            Dictionary<String, String> data = new Dictionary<String, String>();

            db.Delete("settings", null);

            try {
                data.Add("variable", "opacity");
                data.Add("value", opacityChanger.Value.ToString());
                db.Insert("settings", data);
                data.Clear();

                data.Add("variable", "locationX");
                data.Add("value", parent.Location.X.ToString());
                db.Insert("settings", data);
                data.Clear();

                data.Add("variable", "locationY");
                data.Add("value", parent.Location.Y.ToString());
                db.Insert("settings", data);
                data.Clear();

                data.Add("variable", "actionwait");
                data.Add("value", actionWaitChanger.Value.ToString());
                db.Insert("settings", data);
                data.Clear();

                data.Add("variable", "hotkeysenabled");
                data.Add("value", hotkeysCheckbox.Checked.ToString());
                db.Insert("settings", data);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e) {
            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");
            DataTable settings;

            String query = "Select * from settings;";
            settings = db.GetDataTable(query);

            foreach (DataRow r in settings.Rows) {
                switch (r["variable"].ToString()) {
                    case "opacity":
                        this.opacityChanger.Value = Convert.ToInt32(r["value"].ToString());
                        break;
                    case "actionwait":
                        this.actionWaitChanger.Value = Convert.ToInt32(r["value"].ToString());
                        break;
                    case "hotkeysenabled":
                        this.hotkeysCheckbox.Checked = Convert.ToBoolean(r["value"].ToString());
                        break;
                }
            }
        }

        private void LoadDataButton_Click(object sender, EventArgs e) {
            SaveButton.Enabled = false;
            CancelButton.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            Thread t = new Thread(new ThreadStart(LoadDataFromWeb));
            t.IsBackground = true;
            t.Start();
            while (!t.IsAlive);
            while (t.IsAlive) {
                Thread.Sleep(100);
            }
            this.Cursor = Cursors.Arrow;
            SaveButton.Enabled = true;
            CancelButton.Enabled = true;
            OnItemDataChanged(new EventArgs());
            MessageBox.Show("All data imported!");
        }

        private void LoadDataFromWeb() {
            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");
            List<string> urls = new List<string>();

            urls.Add("/weapon/archer");
            urls.Add("/weapon/conjurer");
            urls.Add("/weapon/gladiator");
            urls.Add("/weapon/marauder");
            urls.Add("/weapon/lancer");
            urls.Add("/weapon/pugilist");
            urls.Add("/weapon/thaumaturge");
            urls.Add("/tool/alchemist");
            urls.Add("/tool/armorer");
            urls.Add("/tool/blacksmith");
            urls.Add("/tool/botanist");
            urls.Add("/tool/carpenter");
            urls.Add("/tool/culinarian");
            urls.Add("/tool/fisher");
            urls.Add("/tool/goldsmith");
            urls.Add("/tool/leatherworker");
            urls.Add("/tool/miner");
            urls.Add("/tool/weaver");
            urls.Add("/armor/head");
            urls.Add("/armor/body");
            urls.Add("/armor/hands");
            urls.Add("/armor/waist");
            urls.Add("/armor/legs");
            urls.Add("/armor/feet");
            urls.Add("/armor/shield");
            urls.Add("/armor/earring");
            urls.Add("/armor/bracelet");
            urls.Add("/armor/necklace");
            urls.Add("/armor/ring");

            db.Delete("items", null);

            String position = "";

            foreach (string url in urls) {
                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create("http://mooglebox.com" + url + ".php");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(resStream);

                foreach (HtmlAgilityPack.HtmlNode item in doc.DocumentNode.SelectNodes("//a[@class='imPop']")) {
                    if (url.StartsWith("/weapon") || url.StartsWith("/tool")) {
                        if (item.InnerHtml.Trim().EndsWith("File") ||
                            item.InnerHtml.Trim().EndsWith("Mortar") ||
                            item.InnerHtml.Trim().EndsWith("Pliers") ||
                            item.InnerHtml.Trim().EndsWith("Scythe") ||
                            item.InnerHtml.Trim().EndsWith("Claw Hammer") ||
                            item.InnerHtml.Trim().EndsWith("Culinary Knife") ||
                            item.InnerHtml.Trim().EndsWith("Gig") ||
                            item.InnerHtml.Trim().EndsWith("Grinding Wheel") ||
                            item.InnerHtml.Trim().EndsWith("Awl") ||
                            item.InnerHtml.Trim().EndsWith("Sledgehammer") ||
                            item.InnerHtml.Trim().EndsWith("Spinning Wheel")) {
                            position = "oh";
                        }
                        else if (item.InnerHtml.Trim().EndsWith("Francisca") ||
                            item.InnerHtml.Trim().EndsWith("Throwing Dagger") ||
                            item.InnerHtml.Trim().EndsWith("Javalin") ||
                            item.InnerHtml.Trim().EndsWith("Azagai") ||
                            item.InnerHtml.Trim().EndsWith("Chakram")) {
                            position = "throwing";
                        }
                        else if (item.InnerHtml.Trim().EndsWith("Arrow")) {
                            position = "pack";
                        }
                        else {
                            position = "mh";
                        }
                    }
                    else if (url.EndsWith("shield"))
                        position = "oh";
                    else if (url.EndsWith("earring"))
                        position = "ears";
                    else if (url.EndsWith("bracelet"))
                        position = "wrists";
                    else if (url.EndsWith("necklace"))
                        position = "neck";
                    else 
                        position = url.Substring(url.LastIndexOf("/") + 1);

                    db.ExecuteNonQuery(String.Format("insert into items values(\"{0}\", \"{1}\")", 
                        position, item.InnerHtml.Trim()));
                }
            }
        }
    }

}
