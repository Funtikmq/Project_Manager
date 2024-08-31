using System;
using System.Windows.Forms;

namespace Project_Manager
{
    public partial class UpdateStatusForm : Form
    {
        public int NewStatus { get; private set; }

        public UpdateStatusForm()
        {
            InitializeComponent(); 

            this.ClientSize = new System.Drawing.Size(250, 200);


            TrackBar trackBar = new TrackBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                TickFrequency = 10,
                Top = 50,
                Left = 25,
                Width = 200 
            };

            Label valueLabel = new Label
            {
                Text = $"Status: {trackBar.Value}", 
                Top = 120,
                Left = 75,
                AutoSize = true
            };

            trackBar.Scroll += (sender, e) =>
            {
                valueLabel.Text = $"Status: {trackBar.Value}";
            };

            Button confirmButton = new Button
            {
                Text = "Confirm",
                Top = 150,
                Left = 75,
                Width = 100,
                Height = 30
            };

            confirmButton.Click += (sender, e) =>
            {
                NewStatus = trackBar.Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            this.Controls.Add(trackBar);
            this.Controls.Add(valueLabel);
            this.Controls.Add(confirmButton);
        }
    }
}