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
using Xceed.Words.NET;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;

namespace Project_Manager
{
    public partial class projectManager : Form
    {
        private List<Control> newUserControls = new List<Control>();
        private List<Control> userMenuControls = new List<Control>();
        private string currentUserName = string.Empty;
        private Label selectedProjectLabel = null;

        public class Project
        {
            public string Name { get; set; }
            public string ProjectName { get; set; }
            public string Status { get; set; }
            public string Type { get; set; }
            public int EstimatedPages { get; set; }
            public int Pages { get; set; }
            public string Deadline { get; set; }


            public Project(string name, string projectName, string type, string status, string deadline)
            {
                Name = name;
                ProjectName = projectName;
                Status = status;
                Type = type;
                Deadline = deadline;
            }

            public Project(string name, string projectName, string type, int estimPages, int pages, string status, string deadline)
            {
                Name = name;
                ProjectName = projectName;
                Status = status;
                Type = type;
                Deadline = deadline;
                EstimatedPages = estimPages;
                Pages = pages;
            }
        }

        public projectManager()
        {
            InitializeComponent();
            InitializeButtons();
        }

        // Butoanele din meniul utilizatorilor
        private void InitializeButtons()
        {
            // Fișierul cu Utilizatori
            string filePath = "users.txt";

            try
            {
                
                if (!File.Exists(filePath))
                {
                    
                    File.Create(filePath).Dispose();
                }

                
                string[] lines = File.ReadAllLines(filePath);

                
                var allNames = lines
                    .SelectMany(line => line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                    .ToList();

                
                int buttonCount = 0;
                int width = this.ClientSize.Width;
                int height = this.ClientSize.Height;
                foreach (var name in allNames)
                {
                    System.Windows.Forms.Button btn = new System.Windows.Forms.Button
                    {
                        Text = name.Trim(),
                        Top = height / 2 - 50,
                        Left = width / 10 + buttonCount * 110,
                        Width = 100,
                        Height = 100,
                        BackColor = Color.Beige
                    };

                    btn.Click += UserButton_Click;
                    this.Controls.Add(btn);
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
                MessageBox.Show(ex.Message);
            }
        }

        // Butoanele pentru manipularea utilizatorului
        private void UserButton_Click(object sender, EventArgs e)
        {
            
            foreach (Control control in this.Controls)
            {
                control.Visible = false;
                control.Enabled = false;
            }

            
            System.Windows.Forms.Button clickedButton = sender as System.Windows.Forms.Button;
            if (clickedButton != null)
            {
                currentUserName = clickedButton.Text;
            }

            // Lista de Proiecte
            
            System.Windows.Forms.Button projects = new System.Windows.Forms.Button
            {
                Text = "Projects List",
                Top = this.ClientSize.Height / 2 - 50,
                Left = this.ClientSize.Width / 10,
                Width = 100,
                Height = 100,
                BackColor = Color.Beige
            };

            projects.Click += showProjectsButton_Click;

            System.Windows.Forms.Button manage = new System.Windows.Forms.Button
            {
                Text = "Manage Your Projects",
                Top = this.ClientSize.Height / 2 - 50,
                Left = this.ClientSize.Width - 410,
                Width = 100,
                Height = 100,
                BackColor = Color.Beige
            };

            // Manipularea Proiectelor

            manage.Click += manageButton_Click;

            System.Windows.Forms.Button change = new System.Windows.Forms.Button
            {
                Text = "Change User",
                Top = this.ClientSize.Height / 2 - 50,
                Left = this.ClientSize.Width - 172,
                Width = 100,
                Height = 100,
                BackColor = Color.Beige
            };

            // Schimbarea Utilizatorului

            change.Click += ChangeUser_Click;

            
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

            
            this.Controls.Add(nameLabel);
            this.Controls.Add(nameTextBox);
            this.Controls.Add(backButton);

           
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
                        string filePath = "users.txt";

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

        // Afișare proiectelor
        private void showProjectsButton_Click(object sender, EventArgs e)
        {
            string filePath = "projects.txt";

            if (!File.Exists(filePath))
            {

                File.Create(filePath).Dispose();
            }

            foreach (Control control in userMenuControls)
            {
                control.Visible = false;
                control.Enabled = false;
            }

            

            try
            {
                
                string[] lines = File.ReadAllLines(filePath);
                int counter = 0;
                int labelWidth = 590;  

                
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length >= 5)
                        {
                            string name = parts[0].Trim();
                            string projectName = parts[1].Trim();
                            string type = parts[2].Trim();
                            string status = parts[3].Trim();
                            string deadline = parts[4].Trim();

                            
                            Label projectLabel = new Label
                            {
                                Text = $"{name} | {projectName} |{type} | {status} | {deadline}", 
                                BackColor = Color.Beige,
                                AutoSize = false,
                                Width = labelWidth,
                                Height = 45,
                                Left = 60,
                                Top = 20 + counter * 50,
                                Font = new Font("Arial", 10, FontStyle.Regular),
                                TextAlign = ContentAlignment.MiddleCenter
                            };

                            this.Controls.Add(projectLabel);

                            counter++;
                        }
                    }
                }

                
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

                this.Controls.Add(returnButton);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la citirea fișierului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Manipularea proiectelor
        private void manageButton_Click(object sender, EventArgs e)
        {
            string filePath = "projects.txt";

            if (!File.Exists(filePath))
            {

                File.Create(filePath).Dispose();
            }

            foreach (Control control in userMenuControls)
            {
                control.Visible = false;
                control.Enabled = false;
            }

           

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                int counter = 0;
                int labelWidth = 590;
                bool projectFound = false;

                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length >= 5)
                        {
                            string name = parts[0].Trim();
                            string projectName = parts[1].Trim();
                            string type = parts[2].Trim();
                            string status = parts[3].Trim();
                            string deadline = parts[4].Trim();

                            // Filtrăm proiectele 
                            if (name == currentUserName)
                            {
                                Label projectLabel = new Label
                                {
                                    Text = $"{name} | {projectName} | {type} | {status} | {deadline}",
                                    BackColor = Color.Beige,
                                    AutoSize = false,
                                    Width = labelWidth,
                                    Height = 45,
                                    Left = 60,
                                    Top = 20 + counter * 50,
                                    Font = new Font("Arial", 10, FontStyle.Regular),
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Tag = line, 
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                
                                projectLabel.Click += (updateSender, args) =>
                                {
                                   
                                    if (selectedProjectLabel != null)
                                    {
                                        selectedProjectLabel.BackColor = Color.Beige;
                                    }

                                    
                                    selectedProjectLabel = projectLabel;
                                    selectedProjectLabel.BackColor = Color.LightGray;

                                    string[] projectDetails = ((string)projectLabel.Tag).Split(',');
                                    string currentStatus = projectDetails[3].Trim(); 

                                    // Deschide fereastra pentru actualizarea statusului
                                    using (UpdateStatusForm updateStatusForm = new UpdateStatusForm())
                                    {
                                        if (updateStatusForm.ShowDialog() == DialogResult.OK)
                                        {
                                            int newStatus = updateStatusForm.NewStatus;
                                            projectDetails[3] = newStatus.ToString(); 

                                            
                                            UpdateProjectInFile(filePath, (string)projectLabel.Tag, string.Join(",", projectDetails));

                                            
                                            projectLabel.Text = $"{projectDetails[0]} | {projectDetails[1]} | {projectDetails[2]} | {newStatus} | {projectDetails[4]}";
                                            projectLabel.Tag = string.Join(",", projectDetails); 
                                        }
                                    }
                                };

                                this.Controls.Add(projectLabel);
                                counter++;
                                projectFound = true;
                            }
                        }
                    }
                }

                if (!projectFound)
                {
                    Label noProjectsLabel = new Label
                    {
                        Text = "Nu există niciun proiect pentru utilizatorul curent.",
                        AutoSize = false,
                        Width = labelWidth,
                        Height = 100,
                        Left = this.ClientSize.Width / 9,
                        Top = this.ClientSize.Height / 5,
                        Font = new Font("Arial", 20, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    System.Windows.Forms.Button addProject = new System.Windows.Forms.Button
                    {
                        Text = "Add Project",
                        Top = this.ClientSize.Height - 110,
                        Left = this.ClientSize.Width / 6,
                        Width = 100,
                        Height = 100,
                        BackColor = Color.Beige,
                    };

                    addProject.Click += addProjectButton_Click;

                    this.Controls.Add(noProjectsLabel);
                    this.Controls.Add(addProject);
                    userMenuControls.Add(addProject);
                }
                else
                {
                    System.Windows.Forms.Button addProject = new System.Windows.Forms.Button
                    {
                        Text = "Add Project",
                        Top = this.ClientSize.Height - 110,
                        Left = this.ClientSize.Width / 6,
                        Width = 100,
                        Height = 100,
                        BackColor = Color.Beige,
                    };

                    addProject.Click += addProjectButton_Click;

                    System.Windows.Forms.Button deleteProject = new System.Windows.Forms.Button
                    {
                        Text = "Delete Project",
                        Top = this.ClientSize.Height - 110,
                        Left = this.ClientSize.Width / 6 + 300,
                        Width = 100,
                        Height = 100,
                        BackColor = Color.Beige,
                    };

                    deleteProject.Click += deleteProjectButton_Click;

                    this.Controls.Add(addProject);
                    this.Controls.Add(deleteProject);
                    userMenuControls.Add(addProject);
                    userMenuControls.Add(deleteProject);
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

        // Adăugarea Proiectului
        private void addProjectButton_Click(object sender, EventArgs e)
        {
            using (AddProjectForm addProjectForm = new AddProjectForm())
            {
                if (addProjectForm.ShowDialog() == DialogResult.OK)
                {
                    string projectName = addProjectForm.ProjectName;
                    string projectType = addProjectForm.ProjectType;
                    string projectStatus = addProjectForm.Status;
                    string deadline = addProjectForm.Deadline;
                    int estimPages = addProjectForm.EstimatedPages;
                    int pages = 0;


                    
                    Project newProject = new Project(currentUserName, projectName, projectType, estimPages, pages, projectStatus, deadline);

                    
                    foreach (Control control in this.Controls)
                    {
                        if (control is Label label && label.Text.Contains("Nu există niciun proiect"))
                        {
                            this.Controls.Remove(control);
                            control.Dispose();
                        }
                    }

                   
                    Label projectLabel = new Label
                    {
                        Text = $"{currentUserName} | {projectName} | {projectType} | {projectStatus} | {deadline}",
                        BackColor = Color.Beige,
                        AutoSize = false,
                        Width = 590,
                        Height = 45,
                        Left = 60,
                        Top = 20 + this.Controls.OfType<Label>().Count() * 50,
                        Font = new Font("Arial", 10, FontStyle.Regular),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    this.Controls.Add(projectLabel);

                    
                    string filepath = "projects.txt";

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filepath, true))
                        {
                            writer.WriteLine($"{currentUserName},{projectName},{projectType},{projectStatus},{deadline}"); // Corectare aici
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("A apărut o eroare la scrierea în fișier: " + ex.Message);
                    }
                }
            }
        }

        // Editarea Statusului Proiectului
        private void UpdateProjectInFile(string filepath, string oldLine, string newLine)
        {
            try
            {
                
                string[] lines = File.ReadAllLines(filepath);

                
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == oldLine)
                    {
                        lines[i] = newLine;
                        break;
                    }
                }

                
                File.WriteAllLines(filepath, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ștergerea Proiectului
        private void deleteProjectButton_Click(object sender, EventArgs e)
        {
            if (selectedProjectLabel != null)
            {
                string filepath = "projects.txt";
                string projectLine = (string)selectedProjectLabel.Tag;

                DeleteProjectFromFile(filepath, projectLine);

                this.Controls.Remove(selectedProjectLabel);
                selectedProjectLabel.Dispose();
                selectedProjectLabel = null;

                MessageBox.Show("Project was deleted", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Choose a project to delete.", "Atention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeleteProjectFromFile(string filepath, string projectLine)
        {
            try
            {
                var lines = File.ReadAllLines(filepath).ToList();
                lines.Remove(projectLine);
                File.WriteAllLines(filepath, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Schimbarea utilizatorului
        private void ChangeUser_Click(object sender, EventArgs e)
        {
            foreach (Control control in userMenuControls)
            {
                this.Controls.Remove(control);
            }
            userMenuControls.Clear();

            
            foreach (Control control in this.Controls.OfType<System.Windows.Forms.Button>())
            {
                this.Controls.Remove(control);
            }

            
            InitializeButtons();
        }

        // Butonul de return
        private void BackButton_Click(object sender, EventArgs e)
        {
            
            foreach (Control control in newUserControls)
            {
                this.Controls.Remove(control);
            }

            newUserControls.Clear();

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
             
            List<Control> controlsToRemove = new List<Control>();
            foreach (Control control in this.Controls)
            {
                if (control.Name != null && (control.Name == "BackButton" || control is Label))
                {
                    controlsToRemove.Add(control);
                }
            }

            
            foreach (Control control in controlsToRemove)
            {
                this.Controls.Remove(control);
            }

            
            foreach (Control control in userMenuControls)
            {
                control.Visible = true;
                control.Enabled = true;
            }

            
            List<Control> additionalControlsToRemove = new List<Control>();
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.Button button && (button.Text == "Add Project" || button.Text == "Delete Project"))
                {
                    additionalControlsToRemove.Add(control);
                }
            }

            foreach (Control control in additionalControlsToRemove)
            {
                this.Controls.Remove(control);
            }
        }
    }
}