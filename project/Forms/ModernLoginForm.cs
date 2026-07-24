using System;
using System.Drawing;
using System.Windows.Forms;
using project.Models;
using project.DataHandlers;
using project.Theme.Controls;
using project.Utilities;

namespace project.Forms
{
    /// <summary>
    /// Modern login form with flat design
    /// </summary>
    public partial class ModernLoginForm : Form
    {
        private ModernTextBox txtUsername;
        private ModernTextBox txtPassword;
        private FlatButton btnLogin;
        private FlatButton btnSignUp;
        private Label lblErrorUserName;
        private Label lblErrorPassword;
        private CheckBox chkShowPass;
        
        public ModernLoginForm()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            // Configure main form
            this.Text = "BIIT Society Management - Login";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Theme.ThemeManager.BackgroundWhite;
            
            // Create left panel (sidebar style)
            var leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 300,
                BackColor = Theme.ThemeManager.PrimaryDark
            };
            
            // Add title to left panel
            var titleLabel = new Label
            {
                Text = "BIIT Society\nManagement\nSystem",
                Font = Theme.ThemeManager.GetFontBold(Theme.ThemeManager.FontSizeXLarge),
                ForeColor = Theme.ThemeManager.LegacyCornsilk,
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 200,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10)
            };
            leftPanel.Controls.Add(titleLabel);
            
            // Add decorative bottom section
            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = Theme.ThemeManager.LegacyCornsilk
            };
            leftPanel.Controls.Add(bottomPanel);
            
            // Create right panel (main content)
            var rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Theme.ThemeManager.BackgroundWhite
            };
            
            // Add image/logo at top
            var pictureBox = new PictureBox
            {
                Dock = DockStyle.Top,
                Height = 250,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Properties.Resources._7559129 // Using existing resource
            };
            rightPanel.Controls.Add(pictureBox);
            
            // Username label
            var lblUsername = new Label
            {
                Text = "User Name",
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeMedium),
                ForeColor = Theme.ThemeManager.TextPrimary,
                AutoSize = true,
                Location = new Point(50, 280)
            };
            rightPanel.Controls.Add(lblUsername);
            
            // Username textbox
            txtUsername = new ModernTextBox
            {
                Location = new Point(50, 310),
                Width = 350,
                PlaceholderText = "Enter your username"
            };
            txtUsername.TextChanged += TxtUsername_TextChanged;
            rightPanel.Controls.Add(txtUsername);
            
            // Username error label
            lblErrorUserName = new Label
            {
                Text = "Username is required",
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeSmall),
                ForeColor = Theme.ThemeManager.ErrorColor,
                AutoSize = true,
                Location = new Point(50, 360),
                Visible = false
            };
            rightPanel.Controls.Add(lblErrorUserName);
            
            // Password label
            var lblPassword = new Label
            {
                Text = "Password",
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeMedium),
                ForeColor = Theme.ThemeManager.TextPrimary,
                AutoSize = true,
                Location = new Point(50, 390)
            };
            rightPanel.Controls.Add(lblPassword);
            
            // Password textbox
            txtPassword = new ModernTextBox
            {
                Location = new Point(50, 420),
                Width = 350,
                PasswordChar = '●',
                PlaceholderText = "Enter your password"
            };
            txtPassword.TextChanged += TxtPassword_TextChanged;
            rightPanel.Controls.Add(txtPassword);
            
            // Password error label
            lblErrorPassword = new Label
            {
                Text = "Password is required",
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeSmall),
                ForeColor = Theme.ThemeManager.ErrorColor,
                AutoSize = true,
                Location = new Point(50, 470),
                Visible = false
            };
            rightPanel.Controls.Add(lblErrorPassword);
            
            // Show password checkbox
            chkShowPass = new CheckBox
            {
                Text = "Show Password",
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeSmall),
                ForeColor = Theme.ThemeManager.TextSecondary,
                AutoSize = true,
                Location = new Point(220, 510),
                Cursor = Cursors.Hand
            };
            chkShowPass.CheckedChanged += ChkShowPass_CheckedChanged;
            rightPanel.Controls.Add(chkShowPass);
            
            // Login button
            btnLogin = new FlatButton
            {
                Text = "Login",
                Location = new Point(50, 550),
                Width = 350,
                BaseColor = Theme.ThemeManager.PrimaryDark,
                Enabled = false
            };
            btnLogin.Click += BtnLogin_Click;
            rightPanel.Controls.Add(btnLogin);
            
            // Sign up button
            btnSignUp = new FlatButton
            {
                Text = "Signup For Student",
                Location = new Point(50, 610),
                Width = 350,
                BaseColor = Theme.ThemeManager.BackgroundWhite,
                TextColor = Theme.ThemeManager.PrimaryDark,
                HoverColor = Theme.ThemeManager.PrimaryLight,
                HoverTextColor = Theme.ThemeManager.TextLight
            };
            btnSignUp.Click += BtnSignUp_Click;
            rightPanel.Controls.Add(btnSignUp);
            
            // Close button
            var btnClose = new Button
            {
                Text = "✕",
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Theme.ThemeManager.BackgroundWhite,
                ForeColor = Theme.ThemeManager.ErrorColor,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
                Size = new Size(50, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand,
                Location = new Point(rightPanel.Width - 60, 10)
            };
            btnClose.MouseEnter += (s, e) => btnClose.BackColor = Theme.ThemeManager.ErrorColor;
            btnClose.MouseLeave += (s, e) => btnClose.BackColor = Theme.ThemeManager.BackgroundWhite;
            btnClose.Click += (s, e) => Application.Exit();
            rightPanel.Controls.Add(btnClose);
            
            // Add panels to form
            this.Controls.Add(rightPanel);
            this.Controls.Add(leftPanel);
        }
        
        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblErrorUserName.Visible = true;
                btnLogin.Enabled = false;
            }
            else
            {
                lblErrorUserName.Visible = false;
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    btnLogin.Enabled = true;
            }
        }
        
        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblErrorPassword.Visible = true;
                btnLogin.Enabled = false;
            }
            else
            {
                lblErrorPassword.Visible = false;
                if (!string.IsNullOrWhiteSpace(txtUsername.Text))
                    btnLogin.Enabled = true;
            }
        }
        
        private void ChkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPass.Checked ? '\0' : '●';
        }
        
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            
            if (string.IsNullOrEmpty(userName))
            {
                lblErrorUserName.Text = "Username is required";
                lblErrorUserName.Visible = true;
                return;
            }
            
            if (string.IsNullOrEmpty(password))
            {
                lblErrorPassword.Text = "Password is required";
                lblErrorPassword.Visible = true;
                return;
            }
            
            User user = UserViewModel.Authenticate(userName, password);
            
            if (user != null)
            {
                if (user.Role == "Student")
                {
                    Student student = StudentViewModel.GetByAridNo(user.RelatedId);
                    MessageBoxHelper.ShowInfo("Student");
                    
                    // Open new MainDashboard instead of old StudentDashboard
                    MainDashboard dashboard = new MainDashboard(student);
                    dashboard.Show();
                    this.Hide();
                }
                else if (user.Role == "Teacher")
                {
                    MessageBoxHelper.ShowInfo("Teacher");
                    // TODO: Open Teacher Dashboard
                }
            }
            else
            {
                MessageBoxHelper.ShowError("INVALID CREDENTIALS!");
            }
        }
        
        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            StudentSignUpForm signUpForm = new StudentSignUpForm(this);
            signUpForm.Show();
            this.Hide();
        }
    }
}
