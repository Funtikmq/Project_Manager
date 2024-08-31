using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Diagnostics;

namespace Project_Manager
{
    public partial class Form1 : Form
    {
        private List<Control> newUserControls = new List<Control>();
        private List<Control> userMenuControls = new List<Control>();
        private string currentUserName = string.Empty;

        public class Project
        {
            public string Name { get; set; }
            public string ProjectName { get; set; }
            public string Status { get; set; }
            public string Deadline { get; set; }

            public Project(string name, string projectName, string status, string deadline)
            {
                Name = name;
                ProjectName = projectName;
                Status = status;
                Deadline = deadline;
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitializeButtons();
        }

// Butoanele din meniul utilizatorilor
        private void InitializeButtons()
        {
            // Calea fișierului din care citim
            string filePath = "C:\\Facultate\\BearingPoint\\RPA\\Project_Manager\\users.txt";

            try
            {
                // Citește toate liniile din fișier
                string[] lines = File.ReadAllLines(filePath);

                // Listă pentru nume
                var allNames = lines
                    .SelectMany(line => line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                    .ToList();

                // Creează butoane în funcție de nume
                int buttonCount = 0;
                int width = this.ClientSize.Width;
                int height = this.ClientSize.Height;
                foreach (var name in allNames)
                {
                    System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                    btn.Text = name.Trim();
                    btn.Top = height / 2 - 50;
                    btn.Left = width / 10 + buttonCount * 110;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.BackColor = Color.Beige;

                    // Adaugă handler-ul pentru click
                    btn.Click += UserButton_Click;

                    // Adaugă butonul în form
                    this.Controls.Add(btn);

                    // Incrementare pentru următorul buton
                    buttonCount++;
                }

                if (buttonCount < 5)
                {
                    System.Windows.Forms.Button newUser = new System.Windows.Forms.Button
                    {
                        Text = "New User",
                        Top = height / 2 - 50,
                        Left = width / 10 + buttonCount * 110,
                        Width = 100,
                        Height = 100,
                        BackColor = Color.Beige
                    };

                    newUser.Click += NewUserButton_Click;

                    this.Controls.Add(newUser);
                }

            }
            catch (Exception ex)
            {
                // Gestionarea eventualelor erori
                MessageBox.Show("A apărut o eroare la citirea fișierului: " + ex.Message);
            }
        }


        // Butoanele pentru manipularea utilizatorului
        private void UserButton_Click(object sender, EventArgs e)
        {
            // Ascunde toate controalele existente în loc să le elimini
            foreach (Control control in this.Controls)
            {
                control.Visible = false;
                control.Enabled = false;
            }

            // Obține butonul care a fost apăsat
            System.Windows.Forms.Button clickedButton = sender as System.Windows.Forms.Button;
            if (clickedButton != null)
            {
                currentUserName = clickedButton.Text;
            }

            // Creează și adaugă butoanele pentru opțiunile utilizatorului
            System.Windows.Forms.Button projects = new System.Windows.Forms.Button
            {
                Text = "Projects List",
                Top = this.ClientSize.Height / 2 - 50,
                Left = 100,
                Width = 100,
                Height = 100,
                BackColor = Color.Beige
            };

            projects.Click += projectsButton_Click;

            System.Windows.Forms.Button manage = new System.Windows.Forms.Button
            {
                Text = "Manage Your Projects",
                Top = this.ClientSize.Height / 2 - 50,
                Left = 300,
                Width = 100,
                Height = 100,
                BackColor = Color.Beige
            };

            System.Windows.Forms.Button change = new System.Windows.Forms.Button
            {
                Text = "Change User",
                Top = this.ClientSize.Height / 2 - 50,
                Left = 500,
                Width = 100,
                Height = 100,
                BackColor = Color.Beige
            };

            change.Click += ChangeUser_Click;

            // Adaugă noile butoane la formă și în `userMenuControls`
            this.Controls.Add(projects);
            this.Controls.Add(manage);
            this.Controls.Add(change);

            userMenuControls.Clear();
            userMenuControls.Add(projects);
            userMenuControls.Add(manage);
            userMenuControls.Add(change);
        }

        // Butonul pentru crearea noului utilizator
        private void NewUserButton_Click(object sender, EventArgs e)
        {
            // Ascunde toate butoanele existente
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.Button)
                {
                    control.Visible = false;
                }
            }

            // Elementele pentru adaugarea noului utilizator
            Label nameLabel = new Label
            {
                Text = "Nume utilizator:",
                Top = this.ClientSize.Height / 2 - 100,
                Left = this.ClientSize.Width / 10,
                AutoSize = true
            };

            System.Windows.Forms.TextBox nameTextBox = new System.Windows.Forms.TextBox
            {
                Top = this.ClientSize.Height / 2 - 70,
                Left = this.ClientSize.Width / 10,
                Width = 200
            };
            nameTextBox.KeyDown += NameTextBox_KeyDown;

            System.Windows.Forms.Button backButton = new System.Windows.Forms.Button
            {
                Text = "Back",
                Top = 200,
                Left = 220,
                Width = 50,
                Height = 50,
                BackColor = Color.Beige
            };

            backButton.Click += BackButton_Click;

            // Adaugă noile controale la formă
            this.Controls.Add(nameLabel);
            this.Controls.Add(nameTextBox);
            this.Controls.Add(backButton);

            // Adaugă noile controale în lista de controale create pentru "New User"
            newUserControls.Clear();
            newUserControls.Add(nameLabel);
            newUserControls.Add(nameTextBox);
            newUserControls.Add(backButton);
        }

// Crearea noului utilizator
        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                var textBox = sender as System.Windows.Forms.TextBox;
                if (textBox != null)
                {
                    string userInput = textBox.Text.Trim();
                    if (!string.IsNullOrEmpty(userInput))
                    {
                        // Calea fișierului unde vom scrie datele
                        string filePath = "C:\\Facultate\\BearingPoint\\RPA\\Project_Manager\\users.txt";

                        try
                        {
                            using (StreamWriter writer = new StreamWriter(filePath, true))
                            {
                                writer.WriteLine(userInput + ";");  // Scrie textul pe un rând nou
                            }
                            textBox.Clear();

                            // Setează numele utilizatorului curent
                            currentUserName = userInput;

                            UserButton_Click(this, EventArgs.Empty);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("A apărut o eroare la scrierea în fișier: " + ex.Message);
                        }
                    }
                }
            }
        }


        // Butonul pentru lista de proiecte
        private void projectsButton_Click(object sender, EventArgs e)
        {
            // Ascunde toate controalele din meniul utilizatorului în loc să le elimini
            foreach (Control control in userMenuControls)
            {
                control.Visible = false;
                control.Enabled = false;
            }

            string filepath = "C:\\Facultate\\BearingPoint\\RPA\\Project_Manager\\projects.txt";

            try
            {
                // Citim toate liniile din fișier
                string[] lines = File.ReadAllLines(filepath);
                int counter = 0;
                int labelWidth = 590;  // Setează lățimea dorită pentru toate Label-urile

                // Parcurgem fiecare linie și creăm obiecte de tip Project
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length >= 4)
                        {
                            string name = parts[0].Trim();
                            string projectName = parts[1].Trim();
                            string status = parts[2].Trim();
                            string deadline = parts[3].Trim();

                            // Creăm un obiect de tip Project
                            Project project = new Project(name, projectName, status, deadline);

                            // Creăm un label pentru a afișa informațiile despre proiect
                            Label projectLabel = new Label
                            {
                                Text = $" {project.Name} | \t {project.ProjectName} | \t {project.Status} | \t {project.Deadline}",
                                BackColor = Color.Beige,
                                AutoSize = false,
                                Width = labelWidth,
                                Height = 45,
                                Left = 60,
                                Top = 20 + counter * 50
                            };

                            // Adaugă label-ul în form
                            this.Controls.Add(projectLabel);

                            counter++;
                        }
                    }
                }

                // Creăm butonul "Back" pentru a reveni la meniul principal
                System.Windows.Forms.Button returnButton = new System.Windows.Forms.Button
                {
                    Text = "Back",
                    Top = 17,
                    Left = 5,
                    Width = 50,
                    Height = 50,
                    BackColor = Color.White,
                    Name = "BackButton"
                };

                returnButton.Click += returnButton_Click;

                // Adaugă butonul "Back" în form
                this.Controls.Add(returnButton);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la citirea fișierului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Schimbarea utilizatorului
        private void ChangeUser_Click(object sender, EventArgs e)
        {
            // Elimină toate controalele din meniul utilizatorului
            foreach (Control control in userMenuControls)
            {
                this.Controls.Remove(control);
            }
            userMenuControls.Clear();

            // Elimină toate controalele existente,
            foreach (Control control in this.Controls.OfType<System.Windows.Forms.Button>())
            {
                this.Controls.Remove(control);
            }

            // Reîncarcă butoanele utilizatorilor
            InitializeButtons();
        }

// Butonul de return
        private void BackButton_Click(object sender, EventArgs e)
        {
            // Elimină toate controalele create pentru adăugarea unui nou utilizator
            foreach (Control control in newUserControls)
            {
                this.Controls.Remove(control);
            }

            newUserControls.Clear();

            // Afișează butoanele originale care nu sunt în lista newUserControls
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.Button)
                {
                    control.Visible = true;
                }
            }

        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            // Elimină toate controalele afișate (lista de proiecte și butonul "Back")
            List<Control> controlsToRemove = new List<Control>();
            foreach (Control control in this.Controls)
            {
                // Adaugă toate controalele de tip Label sau butonul "Back" într-o listă pentru a fi eliminate
                if (control.Name != null && control.Name == "BackButton" || control is Label)
                {
                    controlsToRemove.Add(control);
                }
            }

            // Elimină toate controalele adunate
            foreach (Control control in controlsToRemove)
            {
                this.Controls.Remove(control);
            }

            // Reafișează și reactivează butoanele pentru "Projects List", "Manage Your Projects" și "Change User"
            foreach (Control control in userMenuControls)
            {
                control.Visible = true;
                control.Enabled = true;
            }
        }
    }
}