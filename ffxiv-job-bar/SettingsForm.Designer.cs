namespace ffxiv_job_bar
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SaveButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hotkeysCheckbox = new System.Windows.Forms.CheckBox();
            this.actionWaitChanger = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.opacityChanger = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.LoadDataButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actionWaitChanger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opacityChanger)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(230, 139);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 12;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hotkeysCheckbox);
            this.groupBox1.Controls.Add(this.actionWaitChanger);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.opacityChanger);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 101);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Settings:";
            // 
            // hotkeysCheckbox
            // 
            this.hotkeysCheckbox.AutoSize = true;
            this.hotkeysCheckbox.Enabled = false;
            this.hotkeysCheckbox.Location = new System.Drawing.Point(15, 74);
            this.hotkeysCheckbox.Name = "hotkeysCheckbox";
            this.hotkeysCheckbox.Size = new System.Drawing.Size(107, 17);
            this.hotkeysCheckbox.TabIndex = 5;
            this.hotkeysCheckbox.Text = "Hotkeys Enabled";
            this.hotkeysCheckbox.UseVisualStyleBackColor = true;
            // 
            // actionWaitChanger
            // 
            this.actionWaitChanger.Location = new System.Drawing.Point(231, 35);
            this.actionWaitChanger.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.actionWaitChanger.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.actionWaitChanger.Name = "actionWaitChanger";
            this.actionWaitChanger.Size = new System.Drawing.Size(50, 20);
            this.actionWaitChanger.TabIndex = 3;
            this.actionWaitChanger.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Milliseconds to wait between actions:";
            // 
            // opacityChanger
            // 
            this.opacityChanger.Location = new System.Drawing.Point(231, 14);
            this.opacityChanger.Name = "opacityChanger";
            this.opacityChanger.Size = new System.Drawing.Size(50, 20);
            this.opacityChanger.TabIndex = 1;
            this.opacityChanger.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.opacityChanger.ValueChanged += new System.EventHandler(this.opacityChanger_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Main Bar Opacity:";
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(149, 139);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 11;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // LoadDataButton
            // 
            this.LoadDataButton.Location = new System.Drawing.Point(12, 110);
            this.LoadDataButton.Name = "LoadDataButton";
            this.LoadDataButton.Size = new System.Drawing.Size(293, 23);
            this.LoadDataButton.TabIndex = 10;
            this.LoadDataButton.Text = "Update/Import Equipment Data (Uses MoogleBox)";
            this.LoadDataButton.UseVisualStyleBackColor = true;
            this.LoadDataButton.Click += new System.EventHandler(this.LoadDataButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 165);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.LoadDataButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actionWaitChanger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opacityChanger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox hotkeysCheckbox;
        private System.Windows.Forms.NumericUpDown actionWaitChanger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown opacityChanger;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button LoadDataButton;
    }
}