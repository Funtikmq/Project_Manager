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
        public int EstimatedPages { get; private set; } // Noua proprietate pentru numărul estimat de pagini

        private TextBox textBoxProjectName;
        private CheckBox checkBoxWord;
        private CheckBox checkBoxPowerPoint;
        private TextBox textBoxDeadline;
        private TextBox textBoxEstimatedPages; // Nou TextBox pentru numărul de pagini
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
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.checkBoxWord = new System.Windows.Forms.CheckBox();
            this.checkBoxPowerPoint = new System.Windows.Forms.CheckBox();
            this.textBoxDeadline = new System.Windows.Forms.TextBox();
            this.textBoxEstimatedPages = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.trackBarStatus = new System.Windows.Forms.TrackBar();
            this.labelStatusPercentage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Location = new System.Drawing.Point(12, 12);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(260, 22);
            this.textBoxProjectName.TabIndex = 0;
            this.textBoxProjectName.Text = "Project Name";
            // 
            // checkBoxWord
            // 
            this.checkBoxWord.AutoSize = true;
            this.checkBoxWord.Location = new System.Drawing.Point(12, 38);
            this.checkBoxWord.Name = "checkBoxWord";
            this.checkBoxWord.Size = new System.Drawing.Size(62, 20);
            this.checkBoxWord.TabIndex = 1;
            this.checkBoxWord.Text = "Word";
            this.checkBoxWord.UseVisualStyleBackColor = true;
            // 
            // checkBoxPowerPoint
            // 
            this.checkBoxPowerPoint.AutoSize = true;
            this.checkBoxPowerPoint.Location = new System.Drawing.Point(12, 61);
            this.checkBoxPowerPoint.Name = "checkBoxPowerPoint";
            this.checkBoxPowerPoint.Size = new System.Drawing.Size(97, 20);
            this.checkBoxPowerPoint.TabIndex = 2;
            this.checkBoxPowerPoint.Text = "PowerPoint";
            this.checkBoxPowerPoint.UseVisualStyleBackColor = true;
            // 
            // textBoxDeadline
            // 
            this.textBoxDeadline.Location = new System.Drawing.Point(12, 84);
            this.textBoxDeadline.Name = "textBoxDeadline";
            this.textBoxDeadline.Size = new System.Drawing.Size(260, 22);
            this.textBoxDeadline.TabIndex = 3;
            this.textBoxDeadline.Text = "Deadline";
            // 
            // textBoxEstimatedPages
            // 
            this.textBoxEstimatedPages.Location = new System.Drawing.Point(12, 110);
            this.textBoxEstimatedPages.Name = "textBoxEstimatedPages";
            this.textBoxEstimatedPages.Size = new System.Drawing.Size(260, 22);
            this.textBoxEstimatedPages.TabIndex = 4;
            this.textBoxEstimatedPages.Text = "Estimated Pages";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(116, 218);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(197, 218);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 135);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(47, 16);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status:";
            // 
            // trackBarStatus
            // 
            this.trackBarStatus.LargeChange = 10;
            this.trackBarStatus.Location = new System.Drawing.Point(12, 151);
            this.trackBarStatus.Maximum = 100;
            this.trackBarStatus.Name = "trackBarStatus";
            this.trackBarStatus.Size = new System.Drawing.Size(260, 56);
            this.trackBarStatus.TabIndex = 6;
            this.trackBarStatus.TickFrequency = 10;
            this.trackBarStatus.Scroll += new System.EventHandler(this.TrackBarStatus_Scroll);
            // 
            // labelStatusPercentage
            // 
            this.labelStatusPercentage.AutoSize = true;
            this.labelStatusPercentage.Location = new System.Drawing.Point(12, 199);
            this.labelStatusPercentage.Name = "labelStatusPercentage";
            this.labelStatusPercentage.Size = new System.Drawing.Size(69, 16);
            this.labelStatusPercentage.TabIndex = 7;
            this.labelStatusPercentage.Text = "Status: 0%";
            // 
            // AddProjectForm
            // 
            this.BackColor = System.Drawing.Color.LightPink;
            this.ClientSize = new System.Drawing.Size(284, 251);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelStatusPercentage);
            this.Controls.Add(this.trackBarStatus);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.textBoxEstimatedPages);
            this.Controls.Add(this.textBoxDeadline);
            this.Controls.Add(this.checkBoxPowerPoint);
            this.Controls.Add(this.checkBoxWord);
            this.Controls.Add(this.textBoxProjectName);
            this.Name = "AddProjectForm";
            this.Text = "Add New Project";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void TrackBarStatus_Scroll(object sender, EventArgs e)
        {
            // Actualizează Label-ul cu procentajul selectat
            labelStatusPercentage.Text = $"Status: {trackBarStatus.Value}";
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            ProjectName = textBoxProjectName.Text;
            Deadline = textBoxDeadline.Text;
            Status = $"{trackBarStatus.Value}";


            if (int.TryParse(textBoxEstimatedPages.Text, out int estimatedPages))
            {
                EstimatedPages = estimatedPages;
            }
            else
            {
                MessageBox.Show("Enter a valid number of pages");
                return; 
            }

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