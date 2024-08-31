﻿using System;
using System.Windows.Forms;

namespace Project_Manager
{
    public partial class AddProjectForm : Form
    {
        public string ProjectName { get; private set; }
        public string ProjectType { get; private set; }
        public string Deadline { get; private set; }
        public string Status { get; private set; }

        private TextBox textBoxProjectName;
        private CheckBox checkBoxWord;
        private CheckBox checkBoxPowerPoint;
        private TextBox textBoxDeadline;
        private Button buttonSave;
        private Button buttonCancel;
        private Label labelStatus;
        private TrackBar trackBarStatus;
        private Label labelStatusPercentage;

        public AddProjectForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.textBoxProjectName = new TextBox();
            this.checkBoxWord = new CheckBox();
            this.checkBoxPowerPoint = new CheckBox();
            this.textBoxDeadline = new TextBox();
            this.buttonSave = new Button();
            this.buttonCancel = new Button();
            this.labelStatus = new Label();
            this.trackBarStatus = new TrackBar();
            this.labelStatusPercentage = new Label();

            this.SuspendLayout();

            // textBoxProjectName
            this.textBoxProjectName.Location = new System.Drawing.Point(12, 12);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(260, 20);
            this.textBoxProjectName.TabIndex = 0;

            // checkBoxWord
            this.checkBoxWord.AutoSize = true;
            this.checkBoxWord.Location = new System.Drawing.Point(12, 38);
            this.checkBoxWord.Name = "checkBoxWord";
            this.checkBoxWord.Size = new System.Drawing.Size(51, 17);
            this.checkBoxWord.TabIndex = 1;
            this.checkBoxWord.Text = "Word";
            this.checkBoxWord.UseVisualStyleBackColor = true;

            // checkBoxPowerPoint
            this.checkBoxPowerPoint.AutoSize = true;
            this.checkBoxPowerPoint.Location = new System.Drawing.Point(12, 61);
            this.checkBoxPowerPoint.Name = "checkBoxPowerPoint";
            this.checkBoxPowerPoint.Size = new System.Drawing.Size(85, 17);
            this.checkBoxPowerPoint.TabIndex = 2;
            this.checkBoxPowerPoint.Text = "PowerPoint";
            this.checkBoxPowerPoint.UseVisualStyleBackColor = true;

            // textBoxDeadline
            this.textBoxDeadline.Location = new System.Drawing.Point(12, 84);
            this.textBoxDeadline.Name = "textBoxDeadline";
            this.textBoxDeadline.Size = new System.Drawing.Size(260, 20);
            this.textBoxDeadline.TabIndex = 3;

            // labelStatus
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 107);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(38, 13);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "Status:";

            // trackBarStatus
            this.trackBarStatus.Location = new System.Drawing.Point(12, 123);
            this.trackBarStatus.Maximum = 100;
            this.trackBarStatus.TickFrequency = 10;
            this.trackBarStatus.Value = 0;
            this.trackBarStatus.LargeChange = 10;
            this.trackBarStatus.SmallChange = 1;
            this.trackBarStatus.Size = new System.Drawing.Size(260, 45);
            this.trackBarStatus.TabIndex = 5;
            this.trackBarStatus.Scroll += new EventHandler(this.TrackBarStatus_Scroll);

            // labelStatusPercentage
            this.labelStatusPercentage.AutoSize = true;
            this.labelStatusPercentage.Location = new System.Drawing.Point(12, 171);
            this.labelStatusPercentage.Name = "labelStatusPercentage";
            this.labelStatusPercentage.Size = new System.Drawing.Size(57, 13);
            this.labelStatusPercentage.TabIndex = 6;
            this.labelStatusPercentage.Text = "Status: 0%";

            // buttonSave
            this.buttonSave.Location = new System.Drawing.Point(116, 190);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);

            // buttonCancel
            this.buttonCancel.Location = new System.Drawing.Point(197, 190);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);

            // AddProjectForm
            this.ClientSize = new System.Drawing.Size(284, 225);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelStatusPercentage);
            this.Controls.Add(this.trackBarStatus);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.textBoxDeadline);
            this.Controls.Add(this.checkBoxPowerPoint);
            this.Controls.Add(this.checkBoxWord);
            this.Controls.Add(this.textBoxProjectName);
            this.Name = "AddProjectForm";
            this.Text = "Add New Project";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void TrackBarStatus_Scroll(object sender, EventArgs e)
        {
            // Actualizează Label-ul cu procentajul selectat
            labelStatusPercentage.Text = $"Status: {trackBarStatus.Value}%";
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            ProjectName = textBoxProjectName.Text;
            Deadline = textBoxDeadline.Text;
            Status = $"{trackBarStatus.Value}%";

            if (checkBoxWord.Checked)
                ProjectType = "Word";
            else if (checkBoxPowerPoint.Checked)
                ProjectType = "PowerPoint";
            else
                ProjectType = "Unknown";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}