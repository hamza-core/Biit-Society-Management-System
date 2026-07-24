using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using project.Theme;

namespace project.Theme.Controls
{
    /// <summary>
    /// Modern sidebar navigation button with smooth hover animations and accent line
    /// </summary>
    public class SidebarButton : Button
    {
        private bool _isHovered = false;
        private bool _isSelected = false;
        private int _animationFrame = 0;
        private Timer _animationTimer;
        private int _slideOffset = 0;
        
        public Color BaseColor { get; set; } = ThemeManager.SidebarColor;
        public Color HoverColor { get; set; } = Color.FromArgb(45, 60, 75);
        public Color SelectedColor { get; set; } = ThemeManager.PrimaryColor;
        public Color TextColor { get; set; } = ThemeManager.TextLight;
        public Color TextMutedColor { get; set; } = ThemeManager.TextMuted;
        
        public Icon Icon { get; set; } = null;
        public int IconSize { get; set; } = 20;
        
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                Invalidate();
            }
        }
        
        public SidebarButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.OptimizedDoubleBuffer | 
                    ControlStyles.UserPaint |
                    ControlStyles.SupportsTransparentBackColor, true);
            
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            Cursor = Cursors.Hand;
            Font = ThemeManager.GetFont(ThemeManager.FontSizeMedium, FontStyle.Regular);
            ForeColor = TextColor;
            BackColor = BaseColor;
            Height = 55;
            Dock = DockStyle.Top;
            TextAlign = ContentAlignment.MiddleLeft;
            Padding = new Padding(ThemeManager.PaddingLarge, 0, 0, 0);
            
            SetupAnimation();
        }
        
        private void SetupAnimation()
        {
            _animationTimer = new Timer
            {
                Interval = 16 // ~60 FPS
            };
            _animationTimer.Tick += (s, e) =>
            {
                if (_isHovered && _animationFrame < 10)
                    _animationFrame++;
                else if (!_isHovered && _animationFrame > 0)
                    _animationFrame--;
                
                // Slide animation for selection
                if (_isSelected && _slideOffset < 4)
                    _slideOffset++;
                else if (!_isSelected && _slideOffset > 0)
                    _slideOffset--;
                
                if ((_isHovered && _animationFrame == 10) || 
                    (!_isHovered && _animationFrame == 0))
                    _animationTimer.Stop();
                
                Invalidate();
            };
        }
        
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            _animationTimer.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _animationTimer.Start();
        }
        
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            
            Rectangle rect = ClientRectangle;
            
            // Determine background color
            Color bgColor;
            if (_isSelected)
                bgColor = Color.FromArgb(50, 255, 255, 255); // Semi-transparent white
            else if (_isHovered)
                bgColor = InterpolateColor(BaseColor, HoverColor, _animationFrame / 10.0f);
            else
                bgColor = BaseColor;
            
            // Draw background
            using (var brush = new SolidBrush(bgColor))
            {
                g.FillRectangle(brush, rect);
            }
            
            // Draw accent line on left (animated slide)
            int accentWidth = _isSelected ? 4 : 0;
            if (_isHovered && !_isSelected)
                accentWidth = 2 + (_animationFrame / 5);
            
            if (accentWidth > 0)
            {
                var accentColor = _isSelected ? SelectedColor : 
                                 InterpolateColor(SelectedColor, Color.Transparent, 1.0f - (_animationFrame / 10.0f));
                
                using (var brush = new SolidBrush(accentColor))
                {
                    var accentRect = new Rectangle(
                        rect.X + _slideOffset, 
                        rect.Y + ThemeManager.PaddingSmall, 
                        accentWidth, 
                        rect.Height - ThemeManager.PaddingSmall * 2);
                    g.FillRectangle(brush, accentRect);
                }
            }
            
            // Draw icon if present
            int iconX = ThemeManager.PaddingLarge + _slideOffset;
            int iconY = (rect.Height - IconSize) / 2;
            
            if (Icon != null)
            {
                var iconRect = new Rectangle(iconX, iconY, IconSize, IconSize);
                g.DrawIcon(Icon, iconRect);
            }
            
            // Draw text
            int textX = iconX + IconSize + ThemeManager.PaddingMedium;
            int textY = (rect.Height - TextRenderer.MeasureText(Text, Font).Height) / 2;
            
            Color textColor = Enabled ? (_isSelected ? SelectedColor : TextColor) : TextMutedColor;
            
            // Slight text shift on hover/selection
            if (_isHovered || _isSelected)
                textX += 2;
            
            TextRenderer.DrawText(g, Text, Font, 
                                 new Rectangle(textX, textY, rect.Width - textX - ThemeManager.PaddingMedium, rect.Height), 
                                 textColor, TextFormatFlags.VerticalCenter);
        }
        
        private Color InterpolateColor(Color from, Color to, float factor)
        {
            int r = (int)(from.R + (to.R - from.R) * factor);
            int g = (int)(from.G + (to.G - from.G) * factor);
            int b = (int)(from.B + (to.B - from.B) * factor);
            return Color.FromArgb(255, Math.Max(0, Math.Min(255, r)), 
                                  Math.Max(0, Math.Min(255, g)), 
                                  Math.Max(0, Math.Min(255, b)));
        }
    }
}
