using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using project.Helpers;
using project.Models;
using project.Services;
using project.Theme.Controls;

namespace project.Views
{
    public class EventManagementView : UserControl
    {
        private EventService _eventService;
        private Panel _mainPanel;
        private FlowLayoutPanel _eventsPanel;
        private EventCheckInView _checkInView;
        private int _selectedEventId;
        private string _selectedEventName;

        public EventManagementView()
        {
            _eventService = new EventService();
            InitializeComponent();
            LoadSampleEvents();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 248, 250);

            _mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30),
                BackColor = Color.Transparent
            };

            // Title
            var lblTitle = new Label
            {
                Text = "Event Management",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            _mainPanel.Controls.Add(lblTitle);

            // Events List Section
            var lblEventsTitle = new Label
            {
                Text = "Upcoming Events",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                AutoSize = true,
                Location = new Point(30, 80)
            };
            _mainPanel.Controls.Add(lblEventsTitle);

            _eventsPanel = new FlowLayoutPanel
            {
                Location = new Point(30, 120),
                Size = new Size(850, 350),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            _mainPanel.Controls.Add(_eventsPanel);

            // Check-in Section (initially hidden)
            _checkInView = new EventCheckInView
            {
                Location = new Point(30, 120),
                Size = new Size(850, 600),
                Visible = false
            };
            _mainPanel.Controls.Add(_checkInView);

            // Back button for check-in view
            var btnBack = new FlatButton
            {
                Text = "← Back to Events",
                Location = new Point(30, 70),
                Size = new Size(180, 40),
                BackgroundColor = Color.FromArgb(149, 165, 166),
                HoverColor = Color.FromArgb(127, 140, 141),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Visible = false
            };
            btnBack.Click += (s, e) => {
                _checkInView.Visible = false;
                _eventsPanel.Visible = true;
                btnBack.Visible = false;
            };
            _mainPanel.Controls.Add(btnBack);

            this.Controls.Add(_mainPanel);
        }

        private void LoadSampleEvents()
        {
            _eventsPanel.Controls.Clear();

            var sampleEvents = new List<(int Id, string Name, string Date, string Desc)>
            {
                (1, "Annual Tech Fest", "Dec 15, 2024", "Technology exhibition and competitions"),
                (2, "Sports Gala", "Dec 20, 2024", "Annual sports competition"),
                (3, "Art & Culture Night", "Dec 25, 2024", "Cultural performances and art display"),
                (4, "Coding Hackathon", "Jan 5, 2025", "24-hour coding challenge")
            };

            foreach (var ev in sampleEvents)
            {
                var eventCard = CreateEventCard(ev.Id, ev.Name, ev.Date, ev.Desc);
                _eventsPanel.Controls.Add(eventCard);
            }
        }

        private CardPanel CreateEventCard(int eventId, string eventName, string eventDate, string description)
        {
            var card = new CardPanel
            {
                Size = new Size(820, 100),
                Margin = new Padding(0, 0, 0, 15),
                ShadowIntensity = 0.1f
            };

            // Event Icon
            var iconLabel = new Label
            {
                Text = "📅",
                Font = new Font("Segoe UI", 28),
                AutoSize = true,
                Location = new Point(20, 25)
            };
            card.Controls.Add(iconLabel);

            // Event Name
            var lblName = new Label
            {
                Text = eventName,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(80, 20)
            };
            card.Controls.Add(lblName);

            // Event Date
            var lblDate = new Label
            {
                Text = eventDate,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(80, 48)
            };
            card.Controls.Add(lblDate);

            // Description
            var lblDesc = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(149, 165, 166),
                AutoSize = true,
                Location = new Point(80, 70)
            };
            card.Controls.Add(lblDesc);

            // Manage Button
            var btnManage = new FlatButton
            {
                Text = "⚙ Manage",
                Location = new Point(650, 30),
                Size = new Size(140, 40),
                BackgroundColor = Color.FromArgb(52, 152, 219),
                HoverColor = Color.FromArgb(41, 128, 185),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnManage.Click += (s, e) => OpenEventManagement(eventId, eventName);
            card.Controls.Add(btnManage);

            return card;
        }

        private void OpenEventManagement(int eventId, string eventName)
        {
            _selectedEventId = eventId;
            _selectedEventName = eventName;
            
            _eventsPanel.Visible = false;
            _checkInView.Visible = true;
            _checkInView.SetCurrentEvent(eventId, eventName);
            
            // Find and show back button
            foreach (Control ctrl in _mainPanel.Controls)
            {
                if (ctrl is FlatButton btn && btn.Text.Contains("Back"))
                {
                    btn.Visible = true;
                    break;
                }
            }
        }

        public void RegisterParticipant(int eventId, string aridNo, string name)
        {
            _eventService.RegisterParticipant(eventId, aridNo, name);
        }
    }
}
