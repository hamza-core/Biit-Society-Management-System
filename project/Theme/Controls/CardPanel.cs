using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using project.Theme;

namespace project.Theme.Controls
{
    /// <summary>
    /// Modern card panel with shadow and rounded corners
    /// </summary>
    public class CardPanel : Panel
    {
        private bool _isHovered = false;
        private int _shadowIntensity = 10;
        private Timer _hoverTimer;
        
        public Color BackgroundColor { get; set; } = ThemeManager.CardBackground;
        public Color BorderColor { get; set; } = ThemeManager.BorderColor;
        public bool ShowShadow { get; set; } = true;
        public bool ShowBorder { get; set; } = false;
        public int CornerRadius { get; set; } = ThemeManager.CardBorderRadius;
        public int ShadowIntensity 
        { 
            get => _shadowIntensity;
            set
            {
                _shadowIntensity = value;
                Invalidate();
            }
        }
        
        public CardPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.OptimizedDoubleBuffer | 
                    ControlStyles.UserPaint |
                    ControlStyles.SupportsTransparentBackColor, true);
            
            BackColor = BackgroundColor;
            Padding = new Padding(ThemeManager.PaddingLarge);
            
            SetupHoverAnimation();
        }
        
        private void SetupHoverAnimation()
        {
            _hoverTimer = new Timer { Interval = 16 };
            _hoverTimer.Tick += (s, e) =>
            {
                if (_isHovered && ShadowIntensity < 20)
                    ShadowIntensity++;
                else if (!_isHovered && ShadowIntensity > 10)
                    ShadowIntensity--;
                
                if ((_isHovered && ShadowIntensity == 20) || 
                    (!_isHovered && ShadowIntensity == 10))
                    _hoverTimer.Stop();
            };
        }
        
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            _hoverTimer.Start();
            Cursor = Cursors.Hand;
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _hoverTimer.Start();
            Cursor = Cursors.Default;
        }
        
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            
            Rectangle rect = ClientRectangle;
            
            // Draw shadow if enabled
            if (ShowShadow)
            {
                var shadowOffset = _isHovered ? 8 : 4;
                var shadowRect = new Rectangle(shadowOffset, shadowOffset, 
                                               rect.Width - shadowOffset, 
                                               rect.Height - shadowOffset);
                ThemeManager.DrawShadow(g, shadowRect, ShadowIntensity, _isHovered ? 60 : 40);
            }
            
            // Draw rounded background
            using (var path = ThemeManager.CreateRoundedRectangle(rect, CornerRadius))
            using (var brush = new SolidBrush(BackgroundColor))
            {
                g.FillPath(brush, path);
            }
            
            // Draw border if enabled
            if (ShowBorder)
            {
                using (var path = ThemeManager.CreateRoundedRectangle(rect, CornerRadius))
                using (var pen = new Pen(BorderColor, 1))
                {
                    g.DrawPath(pen, path);
                }
            }
            
            // Draw subtle top highlight
            if (!_isHovered)
            {
                using (var brush = new LinearGradientBrush(
                    new Rectangle(rect.X, rect.Y, rect.Width, 2),
                    Color.FromArgb(30, 255, 255, 255),
                    Color.Transparent,
                    90F))
                {
                    g.FillRectangle(brush, new Rectangle(rect.X + CornerRadius, rect.Y, 
                                                         rect.Width - CornerRadius * 2, 1));
                }
            }
        }
    }
}
