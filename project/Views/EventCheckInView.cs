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
    public class EventCheckInView : UserControl
    {
        private EventService _eventService;
        private Panel _mainPanel;
        private Panel _scannerPanel;
        private Panel _resultsPanel;
        private ModernTextBox _txtQRScanner;
        private FlatButton _btnScan;
        private FlatButton _btnPrintCard;
        private Label _lblStatus;
        private Label _lblParticipantInfo;
        private PictureBox _qrPreview;
        private int _currentEventId;
        private string _currentEventName;

        public EventCheckInView()
        {
            _eventService = new EventService();
            InitializeComponent();
            SetupAnimations();
        }

        public void SetCurrentEvent(int eventId, string eventName)
        {
            _currentEventId = eventId;
            _currentEventName = eventName;
            UpdateStats();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 248, 250);
            
            // Main Panel
            _mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30),
                BackColor = Color.Transparent
            };

            // Title
            var lblTitle = new Label
            {
                Text = "Event Check-In & Card Management",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            _mainPanel.Controls.Add(lblTitle);

            // Scanner Section
            _scannerPanel = new CardPanel
            {
                Location = new Point(30, 90),
                Size = new Size(700, 280),
                //ShadowIntensity = 0.15f
            };

            var lblScannerTitle = new Label
            {
                Text = "QR Code Scanner",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                AutoSize = true,
                Location = new Point(25, 20)
            };
            _scannerPanel.Controls.Add(lblScannerTitle);

            _txtQRScanner = new ModernTextBox
            {
                Location = new Point(25, 65),
                Size = new Size(500, 45),
                PlaceholderText = "Scan QR code or enter manually (Format: ARID|Event|Number)",
                Font = new Font("Segoe UI", 12)
            };
            _scannerPanel.Controls.Add(_txtQRScanner);

            _btnScan = new FlatButton
            {
                Text = "✓ Mark Attendance",
                Location = new Point(25, 125),
                Size = new Size(200, 45),
                BackColor = Color.FromArgb(46, 204, 113),
                HoverColor = Color.FromArgb(39, 174, 96),
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            _btnScan.Click += BtnScan_Click;
            _scannerPanel.Controls.Add(_btnScan);

            _btnPrintCard = new FlatButton
            {
                Text = "🖨 Print Card",
                Location = new Point(240, 125),
                Size = new Size(180, 45),
                BackColor = Color.FromArgb(52, 152, 219),
                HoverColor = Color.FromArgb(41, 128, 185),
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            _btnPrintCard.Click += BtnPrintCard_Click;
            _scannerPanel.Controls.Add(_btnPrintCard);

            _lblStatus = new Label
            {
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(25, 185),
                Text = "Ready to scan..."
            };
            _scannerPanel.Controls.Add(_lblStatus);

            _qrPreview = new PictureBox
            {
                Location = new Point(550, 65),
                Size = new Size(120, 120),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };
            _scannerPanel.Controls.Add(_qrPreview);

            _mainPanel.Controls.Add(_scannerPanel);

            // Results/Stats Section
            _resultsPanel = new CardPanel
            {
                Location = new Point(30, 390),
                Size = new Size(700, 200),
                //ShadowIntensity = 0.15f
            };

            var lblResultsTitle = new Label
            {
                Text = "Attendance Statistics",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                AutoSize = true,
                Location = new Point(25, 20)
            };
            _resultsPanel.Controls.Add(lblResultsTitle);

            _lblParticipantInfo = new Label
            {
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(25, 60),
                Text = "Loading statistics..."
            };
            _resultsPanel.Controls.Add(_lblParticipantInfo);

            _mainPanel.Controls.Add(_resultsPanel);

            this.Controls.Add(_mainPanel);
        }

        private void SetupAnimations()
        {
            // Add hover animations to buttons
            _btnScan.MouseEnter += (s, e) => {
                var btn = (FlatButton)s;
                btn.Top -= 2;
            };
            _btnScan.MouseLeave += (s, e) => {
                var btn = (FlatButton)s;
                btn.Top += 2;
            };

            _btnPrintCard.MouseEnter += (s, e) => {
                var btn = (FlatButton)s;
                btn.Top -= 2;
            };
            _btnPrintCard.MouseLeave += (s, e) => {
                var btn = (FlatButton)s;
                btn.Top += 2;
            };
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            string qrData = _txtQRScanner.Text.Trim();
            
            if (string.IsNullOrEmpty(qrData))
            {
                ShowStatus("Please scan or enter QR code data", Color.FromArgb(231, 76, 60));
                return;
            }

            try
            {
                _eventService.MarkAttendanceByQR(qrData);
                ShowStatus("✓ Attendance marked successfully!", Color.FromArgb(46, 204, 113));
                UpdateStats();
                
                // Parse and show participant info
                var parts = qrData.Split('|');
                if (parts.Length >= 2)
                {
                    _lblParticipantInfo.Text = $"Last scanned: {parts[0]} for Event #{parts[1]}";
                }
                
                _txtQRScanner.Clear();
                _txtQRScanner.Focus();
            }
            catch (Exception ex)
            {
                ShowStatus($"✗ Error: {ex.Message}", Color.FromArgb(231, 76, 60));
            }
        }

        private void BtnPrintCard_Click(object sender, EventArgs e)
        {
            if (_currentEventId == 0)
            {
                ShowStatus("Please select an event first", Color.FromArgb(231, 76, 60));
                return;
            }

            var participantsWithoutCards = _eventService.GetParticipantsWithoutCards(_currentEventId);
            
            if (participantsWithoutCards.Count == 0)
            {
                ShowStatus("All participants already have cards!", Color.FromArgb(46, 204, 113));
                return;
            }

            // Generate and print cards for all participants without cards
            int printedCount = 0;
            foreach (var participant in participantsWithoutCards)
            {
                try
                {
                    var cardImage = SimpleQRGenerator.GenerateEventCard(
                        participant.StudentName,
                        participant.AridNo,
                        _currentEventName,
                        participant.ParticipantNumber
                    );

                    // Save card to file
                    var outputPath = Path.Combine(Path.GetTempPath(), 
                        $"EventCard_{participant.AridNo}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                    cardImage.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                    cardImage.Dispose();

                    _eventService.GenerateCardForParticipant(_currentEventId, participant.AridNo);
                    printedCount++;
                }
                catch (Exception ex)
                {
                    ShowStatus($"Error printing card for {participant.StudentName}: {ex.Message}", 
                        Color.FromArgb(231, 76, 60));
                }
            }

            ShowStatus($"✓ Printed {printedCount} event card(s) successfully!", Color.FromArgb(46, 204, 113));
            UpdateStats();
        }

        private void ShowStatus(string message, Color color)
        {
            _lblStatus.Text = message;
            _lblStatus.ForeColor = color;
            
            // Simple animation effect
            _lblStatus.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            var timer = new Timer { Interval = 2000 };
            timer.Tick += (s, e) => {
                _lblStatus.Font = new Font("Segoe UI", 11);
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void UpdateStats()
        {
            if (_currentEventId == 0)
            {
                _lblParticipantInfo.Text = "No event selected";
                return;
            }

            var total = _eventService.GetTotalParticipants(_currentEventId);
            var present = _eventService.GetPresentCount(_currentEventId);
            var withoutCards = _eventService.GetParticipantsWithoutCards(_currentEventId).Count;

            _lblParticipantInfo.Text = $@"Event: {_currentEventName}
Total Registered: {total}
Present Today: {present}
Cards Pending: {withoutCards}";
        }

        public void SimulateQRScan(string aridNo, string eventName, int participantNumber)
        {
            _txtQRScanner.Text = $"{aridNo}|{eventName}|{participantNumber}";
            BtnScan_Click(null, EventArgs.Empty);
        }
    }
}
