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
    public partial class JobConfigureForm : Form
    {
        private JobButton owner;

        public JobConfigureForm() {
            InitializeComponent();
            owner = null;
            LoadButtonSettings();
        }

        private void LoadButtonSettings() {
            LoadItemData(null, null);
        }

        public void SetOwner(JobButton owner) {
            this.owner = owner;
        }

        public void LoadItemData(object sender, EventArgs e) {
            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");
            DataTable data;

            try {
                // Ears
                String query = "select name from items where position = \"ears\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    earGear.Items.Add(r["name"].ToString());
                }

                // Head
                query = "select name from items where position = \"head\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    headGear.Items.Add(r["name"].ToString());
                }

                // Neck
                DataTable neckData;
                query = "select name from items where position = \"neck\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    neckGear.Items.Add(r["name"].ToString());
                }

                // Pouch
                query = "select name from items where position = \"pouch\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    pouchGear.Items.Add(r["name"].ToString());
                }

                // Pack
                query = "select name from items where position = \"pack\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    packGear.Items.Add(r["name"].ToString());
                }

                // Throwing
                query = "select name from items where position = \"throwing\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    throwingGear.Items.Add(r["name"].ToString());
                }

                // Main Hand
                query = "select name from items where position = \"mh\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    mainHandGear.Items.Add(r["name"].ToString());
                }

                // Off Hand
                query = "select name from items where position = \"oh\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    offHandGear.Items.Add(r["name"].ToString());
                }

                // Body
                query = "select name from items where position = \"body\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    bodyGear.Items.Add(r["name"].ToString());
                }

                // Waist
                query = "select name from items where position = \"waist\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    waistGear.Items.Add(r["name"].ToString());
                }

                // Hands
                query = "select name from items where position = \"hands\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    handGear.Items.Add(r["name"].ToString());
                }

                // Legs
                query = "select name from items where position = \"legs\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    legGear.Items.Add(r["name"].ToString());
                }

                // Feet
                query = "select name from items where position = \"feet\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    footGear.Items.Add(r["name"].ToString());
                }

                // Wrists
                query = "select name from items where position = \"wrists\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    wristGear.Items.Add(r["name"].ToString());
                }

                // L.ring
                query = "select name from items where position = \"ring\";";
                data = db.GetDataTable(query);
                foreach (DataRow r in data.Rows) {
                    ring1.Items.Add(r["name"].ToString());
                    ring2.Items.Add(r["name"].ToString());
                }
            }
            catch (Exception q) {
                MessageBox.Show(q.Message);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");
            Dictionary<String, String> data = new Dictionary<String, String>();

            db.Delete("button_labels", String.Format("button_name = \"{0}\"", owner.Name));

            data.Add("button_name", owner.Name);
            data.Add("button_label", buttonLabel.Text);
            data.Add("use_job", useJobCheckbox.Checked.ToString());
            db.Insert("button_labels", data);
            owner.Text = buttonLabel.Text;
            owner.use_job = useJobCheckbox.Checked;
            data.Clear();

            db.Delete("buttons", String.Format("button_name = \"{0}\"", owner.Name));

            data.Add("button_name", owner.Name);
            data.Add("position", "");
            data.Add("value", "");

            // Ears
            data["position"] = "ears";
            data["value"] = earGear.Text;
            db.Insert("buttons", data);

            // Head
            data["position"] = "head";
            data["value"] = headGear.Text;
            db.Insert("buttons", data);
            
            // Neck         
            data["position"] = "neck";
            data["value"] = neckGear.Text;
            db.Insert("buttons", data);
            
            // Pouch
            data["position"] = "pouch";
            data["value"] = pouchGear.Text;
            db.Insert("buttons", data);
            
            // Pack          
            data["position"] = "pack";
            data["value"] = packGear.Text;
            db.Insert("buttons", data);
            
            // Throwing         
            data["position"] = "throwing";
            data["value"] = throwingGear.Text;
            db.Insert("buttons", data);

            // Main Hand 
            data["position"] = "mh";
            data["value"] = mainHandGear.Text;
            db.Insert("buttons", data);

            // Off Hand
            data["position"] = "oh";
            data["value"] = offHandGear.Text;
            db.Insert("buttons", data);

            // Body
            data["position"] = "body";
            data["value"] = bodyGear.Text;
            db.Insert("buttons", data);

            // Waist
            data["position"] = "waist";
            data["value"] = waistGear.Text;
            db.Insert("buttons", data);

            // Hands
            data["position"] = "hands";
            data["value"] = handGear.Text;
            db.Insert("buttons", data);
            
            // Legs
            data["position"] = "legs";
            data["value"] = legGear.Text;
            db.Insert("buttons", data);
            
            // Feet
            data["position"] = "feet";
            data["value"] = footGear.Text;
            db.Insert("buttons", data);
           
            // Wrists
            data["position"] = "wrists";
            data["value"] = wristGear.Text;
            db.Insert("buttons", data);
            
            // L.ring
            data["position"] = "L.ring";
            data["value"] = ring1.Text;
            db.Insert("buttons", data);
           
            // R.ring
            data["position"] = "R.ring";
            data["value"] = ring2.Text;
            db.Insert("buttons", data);
            
            this.Close();
        }

        protected override void OnVisibleChanged(EventArgs e) {
            if (this.Visible == false)
                return;

            SQLiteDatabase db = new SQLiteDatabase("ffxiv_job_bar.db");
            DataTable settings;

            String query = "select * from button_labels where button_name = \"" + owner.Name + "\";";
            settings = db.GetDataTable(query);

            if (settings.Rows.Count > 0) {
                buttonLabel.Text = settings.Rows[0]["button_label"].ToString();
                useJobCheckbox.Checked = Convert.ToBoolean(settings.Rows[0]["use_job"].ToString());
            }
            else {
                buttonLabel.Text = "";
                useJobCheckbox.Checked = false;
                earGear.Text = "";
                headGear.Text = "";
                neckGear.Text = "";
                pouchGear.Text = "";
                packGear.Text = "";
                throwingGear.Text = "";
                mainHandGear.Text = "";
                offHandGear.Text = "";
                bodyGear.Text = "";
                waistGear.Text = "";
                handGear.Text = "";
                legGear.Text = "";
                footGear.Text = "";
                wristGear.Text = "";
                ring1.Text = "";
                ring2.Text = "";
            }

            query = "select * from buttons where button_name = \"" + owner.Name + "\";";
            settings = db.GetDataTable(query);

            foreach (DataRow r in settings.Rows) {
                switch (r["position"].ToString()) {
                    case "ears":
                        earGear.Text = r["value"].ToString();
                        break;
                    case "head":
                        headGear.Text = r["value"].ToString();
                        break;
                    case "neck":
                        neckGear.Text = r["value"].ToString();
                        break;
                    case "pouch":
                        pouchGear.Text = r["value"].ToString();
                        break;
                    case "pack":
                        packGear.Text = r["value"].ToString();
                        break;
                    case "throwing":
                        throwingGear.Text = r["value"].ToString();
                        break;
                    case "mh":
                        mainHandGear.Text = r["value"].ToString();
                        break;
                    case "oh":
                        offHandGear.Text = r["value"].ToString();
                        break;
                    case "body":
                        bodyGear.Text = r["value"].ToString();
                        break;
                    case "waist":
                        waistGear.Text = r["value"].ToString();
                        break;
                    case "hands":
                        handGear.Text = r["value"].ToString();
                        break;
                    case "legs":
                        legGear.Text = r["value"].ToString();
                        break;
                    case "feet":
                        footGear.Text = r["value"].ToString();
                        break;
                    case "wrists":
                        wristGear.Text = r["value"].ToString();
                        break;
                    case "L.ring":
                        ring1.Text = r["value"].ToString();
                        break;
                    case "R.ring":
                        ring2.Text = r["value"].ToString();
                        break;
                }
            }

            base.OnVisibleChanged(e);
        }
        private void JobConfigureForm_Load(object sender, EventArgs e) {

        }
    }
}
