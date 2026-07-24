using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using project.Theme;

namespace project.Theme.Controls
{
    /// <summary>
    /// Modern flat button with smooth hover animations and no 3D borders
    /// </summary>
    public class FlatButton : Button
    {
        private bool _isHovered = false;
        private bool _isPressed = false;
        private int _animationFrame = 0;
        private Timer _animationTimer;
        
        public Color BaseColor { get; set; } = ThemeManager.PrimaryColor;
        public Color HoverColor { get; set; } = ThemeManager.HoverColor;
        public Color PressedColor { get; set; } = ThemeManager.PrimaryDark;
        public Color TextColor { get; set; } = ThemeManager.TextLight;
        public Color DisabledColor { get; set; } = ThemeManager.TextMuted;
        
        public int BorderRadius { get; set; } = ThemeManager.ButtonBorderRadius;
        
        public FlatButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.OptimizedDoubleBuffer | 
                    ControlStyles.UserPaint |
                    ControlStyles.SupportsTransparentBackColor, true);
            
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            Cursor = Cursors.Hand;
            Font = ThemeManager.GetFont(ThemeManager.FontSizeMedium, FontStyle.Bold);
            ForeColor = TextColor;
            BackColor = BaseColor;
            Height = ThemeManager.ButtonHeight;
            
            SetupAnimation();
        }
        
        private void SetupAnimation()
        {
            _animationTimer = new Timer
            {
                Interval = 16 // ~60 FPS for smooth animation
            };
            _animationTimer.Tick += (s, e) =>
            {
                if (_isHovered && _animationFrame < 10)
                    _animationFrame++;
                else if (!_isHovered && _animationFrame > 0)
                    _animationFrame--;
                
                if ((_isHovered && _animationFrame == 10) || (!_isHovered && _animationFrame == 0))
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
            _isPressed = false;
            _animationTimer.Start();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            _isPressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _isPressed = false;
            Invalidate();
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
            
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            
            // Determine current color state with animation interpolation
            Color currentColor;
            if (!Enabled)
                currentColor = DisabledColor;
            else if (_isPressed)
                currentColor = PressedColor;
            else if (_isHovered)
            {
                // Smooth color transition on hover
                currentColor = InterpolateColor(BaseColor, HoverColor, _animationFrame / 10.0f);
            }
            else
                currentColor = BaseColor;
            
            // Draw shadow effect
            if (Enabled && !_isPressed)
            {
                var shadowOffset = _isHovered ? 4 : 2;
                var shadowOpacity = _isHovered ? 60 : 40;
                ThemeManager.DrawShadow(g, new Rectangle(shadowOffset, shadowOffset, rect.Width, rect.Height), 8, shadowOpacity);
            }
            
            // Draw rounded background
            using (var brush = new SolidBrush(currentColor))
            using (var path = ThemeManager.CreateRoundedRectangle(rect, BorderRadius))
            {
                g.FillPath(brush, path);
            }
            
            // Draw subtle highlight on top edge
            if (Enabled && !_isPressed)
            {
                using (var pen = new Pen(Color.FromArgb(40, 255, 255, 255), 1))
                {
                    var highlightRect = new Rectangle(rect.X + BorderRadius, rect.Y, 
                                                      rect.Width - BorderRadius * 2, 1);
                    g.DrawLine(pen, highlightRect.X, highlightRect.Y, highlightRect.Right, highlightRect.Y);
                }
            }
            
            // Draw text centered
            string text = Text;
            SizeF textSize = g.MeasureString(text, Font);
            float x = (Width - textSize.Width) / 2;
            float y = (Height - textSize.Height) / 2;
            
            // Slight text lift on hover
            if (_isHovered && Enabled)
                y -= 1;
            
            using (var brush = new SolidBrush(Enabled ? TextColor : DisabledColor))
            {
                g.DrawString(text, Font, brush, x, y);
            }
            
            // Draw focus rectangle if focused
            if (Focused && Enabled)
            {
                using (var pen = new Pen(Color.FromArgb(100, 255, 255, 255), 2))
                using (var path = ThemeManager.CreateRoundedRectangle(new Rectangle(2, 2, Width - 5, Height - 5), BorderRadius))
                {
                    g.DrawPath(pen, path);
                }
            }
        }
        
        private Color InterpolateColor(Color from, Color to, float factor)
        {
            int r = (int)(from.R + (to.R - from.R) * factor);
            int g = (int)(from.G + (to.G - from.G) * factor);
            int b = (int)(from.B + (to.B - from.B) * factor);
            return Color.FromArgb(255, r, g, b);
        }
    }
}
