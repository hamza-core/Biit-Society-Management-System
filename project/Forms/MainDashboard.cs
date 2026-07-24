using System;
using System.Drawing;
using System.Windows.Forms;
using project.Models;
using project.Theme.Controls;

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
            this.Size = new Size(1200, 800);
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
            btnDashboard = CreateSidebarButton("Dashboard");
            btnMySocieties = CreateSidebarButton("My Societies");
            btnJoinSociety = CreateSidebarButton("Join New Society");
            btnTeams = CreateSidebarButton("Teams");
            btnEvents = CreateSidebarButton("Events");
            btnNotifications = CreateSidebarButton("Notifications");
            btnProfile = CreateSidebarButton("Profile");
            btnManageEvents = CreateSidebarButton("Management");
            
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
                Height = 100,
                BackColor = Theme.ThemeManager.PrimaryDark
            };
            
            var userLabel = new Label
            {
                Text = currentStudent?.Name ?? "User",
                Font = Theme.ThemeManager.GetFontBold(Theme.ThemeManager.FontSizeMedium),
                ForeColor = Theme.ThemeManager.TextLight,
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter
            };
            
            var aridLabel = new Label
            {
                Text = currentStudent?.AridNo ?? "",
                Font = Theme.ThemeManager.GetFont(Theme.ThemeManager.FontSizeSmall),
                ForeColor = Theme.ThemeManager.TextMuted,
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter
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
            // Clear existing content
            viewportPanel.Controls.Clear();
            
            // Show welcome message for now (will be replaced with actual UserControl)
            var welcomeLabel = new Label
            {
                Text = $"Welcome, {currentStudent?.Name ?? "Student"}!",
                Font = Theme.ThemeManager.GetFontBold(Theme.ThemeManager.FontSizeTitle),
                ForeColor = Theme.ThemeManager.TextPrimary,
                AutoSize = true,
                Location = new Point(Theme.ThemeManager.PaddingLarge, Theme.ThemeManager.PaddingLarge)
            };
            
            viewportPanel.Controls.Add(welcomeLabel);
        }
        
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            LoadDashboardView();
        }
        
        private void BtnMySocieties_Click(object sender, EventArgs e)
        {
            // Will load MySocieties UserControl
            MessageBox.Show("My Societies view - to be implemented with UserControl");
        }
        
        private void BtnJoinSociety_Click(object sender, EventArgs e)
        {
            // Will load JoinSociety UserControl
            MessageBox.Show("Join Society view - to be implemented with UserControl");
        }
        
        private void BtnTeams_Click(object sender, EventArgs e)
        {
            // Will load Teams UserControl
            MessageBox.Show("Teams view - to be implemented with UserControl");
        }
        
        private void BtnEvents_Click(object sender, EventArgs e)
        {
            // Will load Events UserControl
            MessageBox.Show("Events view - to be implemented with UserControl");
        }
        
        private void BtnNotifications_Click(object sender, EventArgs e)
        {
            // Will load Notifications UserControl
            MessageBox.Show("Notifications view - to be implemented with UserControl");
        }
        
        private void BtnProfile_Click(object sender, EventArgs e)
        {
            // Will load Profile UserControl
            MessageBox.Show("Profile view - to be implemented with UserControl");
        }
        
        private void BtnManageEvents_Click(object sender, EventArgs e)
        {
            // Will load Event Management UserControl (only for society leaders)
            MessageBox.Show("Event Management view - to be implemented with UserControl");
        }
    }
}
