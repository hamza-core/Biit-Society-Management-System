using System;
using System.Drawing;
using System.Windows.Forms;

namespace project.Theme.Controls
{
    /// <summary>
    /// Draggable top bar panel for borderless forms
    /// </summary>
    public class TopBarPanel : Panel
    {
        private Form parentForm;
        
        public Color BarColor { get; set; } = Theme.ThemeManager.PrimaryColor;
        public Color TextColor { get; set; } = Theme.ThemeManager.TextLight;
        
        public string TitleText { get; set; } = "BIIT Society Management";
        
        public TopBarPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.OptimizedDoubleBuffer | 
                    ControlStyles.UserPaint, true);
            
            Dock = DockStyle.Top;
            Height = Theme.ThemeManager.TopBarHeight;
            BackColor = BarColor;
        }

        public void AttachToForm(Form form)
        {
            parentForm = form;
            
            // Add close button
            var closeButton = new Button
            {
                Text = "✕",
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = BarColor,
                ForeColor = TextColor,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
                Size = new Size(45, Height),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            
            closeButton.MouseEnter += (s, e) => closeButton.BackColor = Theme.ThemeManager.SecondaryColor;
            closeButton.MouseLeave += (s, e) => closeButton.BackColor = BarColor;
            closeButton.Click += (s, e) => parentForm?.Close();
            
            Controls.Add(closeButton);
            
            // Add minimize button
            var minimizeButton = new Button
            {
                Text = "−",
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = BarColor,
                ForeColor = TextColor,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
                Size = new Size(45, Height),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            
            minimizeButton.MouseEnter += (s, e) => minimizeButton.BackColor = Theme.ThemeManager.HoverColor;
            minimizeButton.MouseLeave += (s, e) => minimizeButton.BackColor = BarColor;
            minimizeButton.Click += (s, e) => parentForm?.WindowState = FormWindowState.Minimized;
            
            Controls.Add(minimizeButton);
            
            // Position buttons
            closeButton.Location = new Point(Width - closeButton.Width, 0);
            minimizeButton.Location = new Point(Width - closeButton.Width - minimizeButton.Width, 0);
            
            // Enable dragging
            MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left && parentForm != null)
                {
                    Capture = false;
                    var msg = Message.Create(parentForm.Handle, 0x112, (IntPtr)0xF012, IntPtr.Zero);
                    if (parentForm is Form f)
                    {
                        // Use reflection to call protected WndProc method
                        var method = typeof(Form).GetMethod("WndProc", 
                            System.Reflection.BindingFlags.Instance | 
                            System.Reflection.BindingFlags.NonPublic);
                        method?.Invoke(f, new object[] { msg });
                    }
                }
            };
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            
            Graphics g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            // Draw title
            using (var font = Theme.ThemeManager.GetFontBold(Theme.ThemeManager.FontSizeLarge))
            using (var brush = new SolidBrush(TextColor))
            {
                g.DrawString(TitleText, font, brush, Theme.ThemeManager.PaddingLarge, (Height - font.Height) / 2);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            // Reposition buttons on resize
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button btn)
                {
                    if (btn.Text == "✕")
                        btn.Location = new Point(Width - btn.Width, 0);
                    else if (btn.Text == "−")
                        btn.Location = new Point(Width - btn.Width - 45, 0);
                    
                    btn.Height = Height;
                }
            }
            
            Invalidate();
        }
    }
}
