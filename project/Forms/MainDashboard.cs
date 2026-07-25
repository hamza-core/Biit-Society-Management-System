using System;
using System.Drawing;
using System.Windows.Forms;
using project.Models;
using project.Theme.Controls;
using project.Views;

namespace project.Forms
{
    /// <summary>
    /// Main dashboard with modern SPA layout
    /// Borderless window with custom draggable title bar, sidebar navigation, and content viewport
    /// </summary>
    public partial class MainDashboard : Form
    {
        private Student currentStudent;
        private SidebarPanel sidebarPanel;
        private ViewportPanel viewportPanel;
        private TopBarPanel topBarPanel;
        
        // Navigation buttons
        private SidebarButton btnDashboard;
        private SidebarButton btnMySocieties;
        private SidebarButton btnJoinSociety;
        private SidebarButton btnTeams;
        private SidebarButton btnEvents;
        private SidebarButton btnNotifications;
        private SidebarButton btnProfile;
        private SidebarButton btnManageEvents;
        
        // Current views
        private EventManagementView _eventManagementView;
        
        public MainDashboard()
        {
            InitializeComponent();
        }
        
        public MainDashboard(Student student)
        {
            currentStudent = student;
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            // Configure main form
            this.Text = "BIIT Society Management - Dashboard";
            this.Size = new Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Theme.ThemeManager.BackgroundLight;
            
            // Create top bar
            topBarPanel = new TopBarPanel
            {
                TitleText = "BIIT Society Management System"
            };
            topBarPanel.AttachToForm(this);
            
            // Create sidebar
            sidebarPanel = new SidebarPanel();
            
            // Add logo/title area to sidebar
            var sidebarTitle = new Label
            {
                Text = "BIIT SMS",
                Font = Theme.ThemeManager.GetFontBold(Theme.ThemeManager.FontSizeXLarge),
                ForeColor = Theme.ThemeManager.LegacyCornsilk,
                AutoSize = false,
                Height = 80,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            sidebarPanel.Controls.Add(sidebarTitle);
            
            // Create navigation buttons
            btnDashboard = CreateSidebarButton("🏠 Dashboard");
            btnMySocieties = CreateSidebarButton("👥 My Societies");
            btnJoinSociety = CreateSidebarButton("➕ Join Society");
            btnTeams = CreateSidebarButton("🏆 Teams");
            btnEvents = CreateSidebarButton("📅 Events");
            btnNotifications = CreateSidebarButton("🔔 Notifications");
            btnProfile = CreateSidebarButton("👤 Profile");
            btnManageEvents = CreateSidebarButton("⚙ Management");
            
            // Add buttons to sidebar (in reverse order since Dock=Top)
            sidebarPanel.Controls.Add(btnManageEvents);
            sidebarPanel.Controls.Add(btnProfile);
            sidebarPanel.Controls.Add(btnNotifications);
            sidebarPanel.Controls.Add(btnEvents);
            sidebarPanel.Controls.Add(btnTeams);
            sidebarPanel.Controls.Add(btnJoinSociety);
            sidebarPanel.Controls.Add(btnMySocieties);
            sidebarPanel.Controls.Add(btnDashboard);
            
            // Add user info section at bottom of sidebar
            var userInfoPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = Theme.ThemeManager.PrimaryDark
            };
            
            // User avatar placeholder
            var avatarLabel = new Label
            {
                Text = "👤",
                Font = new Font("Segoe UI", 24),
                ForeColor = Theme.ThemeManager.LegacyCornsilk,
                AutoSize = true,
                Location = new Point(60, 15)
            };
            userInfoPanel.Controls.Add(avatarLabel);
            
            var userLabel = new Label
            {
                Text = currentStudent?.Name ?? "User",
                Font = Theme.ThemeManager.GetFontBold(Theme.ThemeManager.FontSizeMedium),
                ForeColor = Theme.ThemeManager.TextLight,
                AutoSize = false,
                Location = new Point(15, 55),
                Size = new Size(170, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            var aridLabel = new Label
            {
                Text = currentStudent?.AridNo ?? "",
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeSmall),
                ForeColor = Theme.ThemeManager.TextMuted,
                AutoSize = false,
                Location = new Point(15, 80),
                Size = new Size(170, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            userInfoPanel.Controls.Add(userLabel);
            userInfoPanel.Controls.Add(aridLabel);
            sidebarPanel.Controls.Add(userInfoPanel);
            
            // Create viewport panel
            viewportPanel = new ViewportPanel();
            
            // Add controls to form
            this.Controls.Add(viewportPanel);
            this.Controls.Add(sidebarPanel);
            this.Controls.Add(topBarPanel);
            
            // Wire up navigation events
            btnDashboard.Click += BtnDashboard_Click;
            btnMySocieties.Click += BtnMySocieties_Click;
            btnJoinSociety.Click += BtnJoinSociety_Click;
            btnTeams.Click += BtnTeams_Click;
            btnEvents.Click += BtnEvents_Click;
            btnNotifications.Click += BtnNotifications_Click;
            btnProfile.Click += BtnProfile_Click;
            btnManageEvents.Click += BtnManageEvents_Click;
            
            // Load default view
            LoadDashboardView();
        }
        
        private SidebarButton CreateSidebarButton(string text)
        {
            return new SidebarButton
            {
                Text = text,
                Height = 60
            };
        }
        
        private void LoadDashboardView()
        {
            ResetAllButtons();
            btnDashboard.IsSelected = true;
            
            viewportPanel.Controls.Clear();
            
            // Welcome gradient card
            var welcomeCard = new CardPanel
            {
                Size = new Size(1000, 180),
                Location = new Point(30, 30),
                //ShadowIntensity = 0.15f
            };
            
            using (var g = welcomeCard.CreateGraphics())
            {
                using (var gradient = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Point(0, 0),
                    new Point(welcomeCard.Width, welcomeCard.Height),
                    Color.FromArgb(52, 152, 219),
                    Color.FromArgb(41, 128, 185)))
                {
                    g.FillRectangle(gradient, 0, 0, welcomeCard.Width, welcomeCard.Height);
                }
            }
            
            var lblWelcome = new Label
            {
                Text = $"Welcome back, {currentStudent?.Name ?? "Student"}!",
                Font = Theme.ThemeManager.GetFontBold(Theme.ThemeManager.FontSizeXLarge),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 30)
            };
            welcomeCard.Controls.Add(lblWelcome);
            
            var lblSubtitle = new Label
            {
                Text = "Manage your society activities, events, and profile all in one place.",
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeMedium),
                ForeColor = Color.FromArgb(236, 240, 241),
                AutoSize = true,
                Location = new Point(40, 75)
            };
            welcomeCard.Controls.Add(lblSubtitle);
            
            // Stats cards
            CreateStatCard(welcomeCard, 30, 120, "📊", "3", "Active Societies");
            CreateStatCard(welcomeCard, 220, 120, "🏆", "5", "Events Joined");
            CreateStatCard(welcomeCard, 410, 120, "🎯", "2", "Upcoming Events");
            
            viewportPanel.Controls.Add(welcomeCard);
        }
        
        private void CreateStatCard(Control parent, int x, int y, string icon, string number, string label)
        {
            var statCard = new CardPanel
            {
                Size = new Size(160, 100),
                Location = new Point(x, y),
                //ShadowIntensity = 0.1f,
                BackColor = Color.White
            };
            
            var iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 24),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            statCard.Controls.Add(iconLabel);
            
            var numberLabel = new Label
            {
                Text = number,
                Font = Theme.ThemeManager.GetFontBold(28),
                ForeColor = Color.FromArgb(52, 152, 219),
                AutoSize = true,
                Location = new Point(70, 20)
            };
            statCard.Controls.Add(numberLabel);
            
            var textLabel = new Label
            {
                Text = label,
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeSmall),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(70, 55)
            };
            statCard.Controls.Add(textLabel);
            
            parent.Controls.Add(statCard);
        }
        
        private void ResetAllButtons()
        {
            btnDashboard.IsSelected = false;
            btnMySocieties.IsSelected = false;
            btnJoinSociety.IsSelected = false;
            btnTeams.IsSelected = false;
            btnEvents.IsSelected = false;
            btnNotifications.IsSelected = false;
            btnProfile.IsSelected = false;
            btnManageEvents.IsSelected = false;
        }
        
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ResetAllButtons();
            btnDashboard.IsSelected = true;
            LoadDashboardView();
        }
        
        private void BtnMySocieties_Click(object sender, EventArgs e)
        {
            ResetAllButtons();
            btnMySocieties.IsSelected = true;
            ShowComingSoonMessage("My Societies");
        }
        
        private void BtnJoinSociety_Click(object sender, EventArgs e)
        {
            ResetAllButtons();
            btnJoinSociety.IsSelected = true;
            ShowComingSoonMessage("Join Society");
        }
        
        private void BtnTeams_Click(object sender, EventArgs e)
        {
            ResetAllButtons();
            btnTeams.IsSelected = true;
            ShowComingSoonMessage("Teams");
        }
        
        private void BtnEvents_Click(object sender, EventArgs e)
        {
            ResetAllButtons();
            btnEvents.IsSelected = true;
            ShowComingSoonMessage("Events");
        }
        
        private void BtnNotifications_Click(object sender, EventArgs e)
        {
            ResetAllButtons();
            btnNotifications.IsSelected = true;
            ShowComingSoonMessage("Notifications");
        }
        
        private void BtnProfile_Click(object sender, EventArgs e)
        {
            ResetAllButtons();
            btnProfile.IsSelected = true;
            ShowComingSoonMessage("Profile");
        }
        
        private void BtnManageEvents_Click(object sender, EventArgs e)
        {
            ResetAllButtons();
            btnManageEvents.IsSelected = true;
            
            viewportPanel.Controls.Clear();
            
            if (_eventManagementView == null)
            {
                _eventManagementView = new EventManagementView();
                _eventManagementView.Dock = DockStyle.Fill;
                
                // Pre-register some sample participants for demo
                _eventManagementView.RegisterParticipant(1, "ARID-001", "John Doe");
                _eventManagementView.RegisterParticipant(1, "ARID-002", "Jane Smith");
            }
            
            viewportPanel.Controls.Add(_eventManagementView);
        }
        
        private void ShowComingSoonMessage(string feature)
        {
            viewportPanel.Controls.Clear();
            
            var comingSoonPanel = new CardPanel
            {
                Size = new Size(600, 300),
                Location = new Point((viewportPanel.Width - 600) / 2, (viewportPanel.Height - 300) / 2),
                //ShadowIntensity = 0.15f
            };
            
            var iconLabel = new Label
            {
                Text = "🚧",
                Font = new Font("Segoe UI", 48),
                AutoSize = true,
                Location = new Point(270, 40)
            };
            comingSoonPanel.Controls.Add(iconLabel);
            
            var titleLabel = new Label
            {
                Text = $"{feature}",
                Font = Theme.ThemeManager.GetFontBold(Theme.ThemeManager.FontSizeXLarge),
                ForeColor = Color.FromArgb(52, 152, 219),
                AutoSize = true,
                Location = new Point(200, 120)
            };
            comingSoonPanel.Controls.Add(titleLabel);
            
            var subtitleLabel = new Label
            {
                Text = "This feature is coming soon!",
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeMedium),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(210, 165)
            };
            comingSoonPanel.Controls.Add(subtitleLabel);
            
            viewportPanel.Controls.Add(comingSoonPanel);
        }
    }
}
