namespace project.Forms
{
    partial class StudentDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlSideBar = new System.Windows.Forms.Panel();
            this.btnEvent = new System.Windows.Forms.Button();
            this.btnManageEvent = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnNotification = new System.Windows.Forms.Button();
            this.btnTeam = new System.Windows.Forms.Button();
            this.btnJoinSociety = new System.Windows.Forms.Button();
            this.btnMySocieties = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.process1 = new System.Diagnostics.Process();
            this.pnlSideBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.BackColor = System.Drawing.Color.DarkSlateGray;
            this.pnlSideBar.Controls.Add(this.btnEvent);
            this.pnlSideBar.Controls.Add(this.btnManageEvent);
            this.pnlSideBar.Controls.Add(this.btnProfile);
            this.pnlSideBar.Controls.Add(this.btnNotification);
            this.pnlSideBar.Controls.Add(this.btnTeam);
            this.pnlSideBar.Controls.Add(this.btnJoinSociety);
            this.pnlSideBar.Controls.Add(this.btnMySocieties);
            this.pnlSideBar.Controls.Add(this.btnDashboard);
            this.pnlSideBar.Controls.Add(this.label3);
            this.pnlSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(284, 714);
            this.pnlSideBar.TabIndex = 11;
            // 
            // btnEvent
            // 
            this.btnEvent.AutoEllipsis = true;
            this.btnEvent.BackColor = System.Drawing.Color.White;
            this.btnEvent.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEvent.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEvent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEvent.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnEvent.Location = new System.Drawing.Point(0, 627);
            this.btnEvent.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.btnEvent.Name = "btnEvent";
            this.btnEvent.Size = new System.Drawing.Size(284, 76);
            this.btnEvent.TabIndex = 14;
            this.btnEvent.Text = "Event";
            this.btnEvent.UseVisualStyleBackColor = false;
            this.btnEvent.Click += new System.EventHandler(this.btnEvent_Click);
            // 
            // btnManageEvent
            // 
            this.btnManageEvent.AutoEllipsis = true;
            this.btnManageEvent.BackColor = System.Drawing.Color.White;
            this.btnManageEvent.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnManageEvent.Enabled = false;
            this.btnManageEvent.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnManageEvent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnManageEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageEvent.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnManageEvent.Location = new System.Drawing.Point(0, 551);
            this.btnManageEvent.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.btnManageEvent.Name = "btnManageEvent";
            this.btnManageEvent.Size = new System.Drawing.Size(284, 76);
            this.btnManageEvent.TabIndex = 13;
            this.btnManageEvent.Text = "Management";
            this.btnManageEvent.UseVisualStyleBackColor = false;
            this.btnManageEvent.Click += new System.EventHandler(this.btnManageEvent_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.AutoEllipsis = true;
            this.btnProfile.BackColor = System.Drawing.Color.White;
            this.btnProfile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProfile.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnProfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnProfile.Location = new System.Drawing.Point(0, 475);
            this.btnProfile.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(284, 76);
            this.btnProfile.TabIndex = 12;
            this.btnProfile.Text = "Profile";
            this.btnProfile.UseVisualStyleBackColor = false;
            this.btnProfile.Click += new System.EventHandler(this.button3_Click);
            this.btnProfile.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
            this.btnProfile.MouseHover += new System.EventHandler(this.button3_MouseHover_1);
            // 
            // btnNotification
            // 
            this.btnNotification.AutoEllipsis = true;
            this.btnNotification.BackColor = System.Drawing.Color.White;
            this.btnNotification.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNotification.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNotification.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnNotification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotification.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotification.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnNotification.Location = new System.Drawing.Point(0, 399);
            this.btnNotification.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.btnNotification.Name = "btnNotification";
            this.btnNotification.Size = new System.Drawing.Size(284, 76);
            this.btnNotification.TabIndex = 11;
            this.btnNotification.Text = "Notifications";
            this.btnNotification.UseVisualStyleBackColor = false;
            this.btnNotification.Click += new System.EventHandler(this.button1_Click);
            this.btnNotification.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.btnNotification.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // btnTeam
            // 
            this.btnTeam.AutoEllipsis = true;
            this.btnTeam.BackColor = System.Drawing.Color.White;
            this.btnTeam.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTeam.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTeam.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTeam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeam.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnTeam.Location = new System.Drawing.Point(0, 335);
            this.btnTeam.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.btnTeam.Name = "btnTeam";
            this.btnTeam.Size = new System.Drawing.Size(284, 64);
            this.btnTeam.TabIndex = 10;
            this.btnTeam.Text = "Teams ";
            this.btnTeam.UseVisualStyleBackColor = false;
            this.btnTeam.Click += new System.EventHandler(this.button6_Click);
            this.btnTeam.MouseLeave += new System.EventHandler(this.button6_MouseLeave);
            this.btnTeam.MouseHover += new System.EventHandler(this.button6_MouseHover);
            // 
            // btnJoinSociety
            // 
            this.btnJoinSociety.AutoEllipsis = true;
            this.btnJoinSociety.BackColor = System.Drawing.Color.White;
            this.btnJoinSociety.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnJoinSociety.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnJoinSociety.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnJoinSociety.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinSociety.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoinSociety.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnJoinSociety.Location = new System.Drawing.Point(0, 271);
            this.btnJoinSociety.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.btnJoinSociety.Name = "btnJoinSociety";
            this.btnJoinSociety.Size = new System.Drawing.Size(284, 64);
            this.btnJoinSociety.TabIndex = 8;
            this.btnJoinSociety.Text = "Join New Society";
            this.btnJoinSociety.UseVisualStyleBackColor = false;
            this.btnJoinSociety.Click += new System.EventHandler(this.button4_Click);
            this.btnJoinSociety.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
            this.btnJoinSociety.MouseHover += new System.EventHandler(this.button4_MouseHover);
            // 
            // btnMySocieties
            // 
            this.btnMySocieties.AutoEllipsis = true;
            this.btnMySocieties.BackColor = System.Drawing.Color.White;
            this.btnMySocieties.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMySocieties.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMySocieties.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMySocieties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMySocieties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMySocieties.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnMySocieties.Location = new System.Drawing.Point(0, 215);
            this.btnMySocieties.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.btnMySocieties.Name = "btnMySocieties";
            this.btnMySocieties.Size = new System.Drawing.Size(284, 56);
            this.btnMySocieties.TabIndex = 7;
            this.btnMySocieties.Text = "My Societies";
            this.btnMySocieties.UseVisualStyleBackColor = false;
            this.btnMySocieties.Click += new System.EventHandler(this.btnMySocieties_Click);
            this.btnMySocieties.MouseLeave += new System.EventHandler(this.btnMySocieties_MouseLeave);
            this.btnMySocieties.MouseHover += new System.EventHandler(this.btnMySocieties_MouseHover);
            // 
            // btnDashboard
            // 
            this.btnDashboard.AutoEllipsis = true;
            this.btnDashboard.BackColor = System.Drawing.Color.White;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnDashboard.Location = new System.Drawing.Point(0, 151);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(284, 64);
            this.btnDashboard.TabIndex = 6;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            this.btnDashboard.MouseLeave += new System.EventHandler(this.btnDashboard_MouseLeave);
            this.btnDashboard.MouseHover += new System.EventHandler(this.btnDashboard_MouseHover);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Cornsilk;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3, 19, 3, 3);
            this.label3.Size = new System.Drawing.Size(284, 151);
            this.label3.TabIndex = 0;
            this.label3.Text = "BIIT Society Management System";
            // 
            // guna2Elipse1
            // 
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(284, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1326, 714);
            this.pnlMain.TabIndex = 12;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // StudentDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1610, 714);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSideBar);
            this.MaximumSize = new System.Drawing.Size(1632, 770);
            this.MinimumSize = new System.Drawing.Size(1632, 770);
            this.Name = "StudentDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StudentDashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.StudentDashboard_Load);
            this.pnlSideBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Button btnMySocieties;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnNotification;
        private System.Windows.Forms.Button btnTeam;
        private System.Windows.Forms.Button btnJoinSociety;
        private System.Windows.Forms.Panel pnlMain;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.Button btnManageEvent;
        private System.Windows.Forms.Button btnEvent;
    }
}