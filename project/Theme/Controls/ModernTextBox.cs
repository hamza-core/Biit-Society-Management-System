using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using project.Theme;

namespace project.Theme.Controls
{
    /// <summary>
    /// Modern text box with animated border and focus effects
    /// </summary>
    public class ModernTextBox : TextBox
    {
        private bool _isFocused = false;
        private bool _isHovered = false;
        private int _animationFrame = 0;
        private Timer _animationTimer;
        private string _placeholderText = "";
        
        public Color BorderColor { get; set; } = ThemeManager.BorderColor;
        public Color FocusColor { get; set; } = ThemeManager.PrimaryColor;
        public Color HoverColor { get; set; } = ThemeManager.SecondaryColor;
        public Color BackgroundColor { get; set; } = ThemeManager.BackgroundWhite;
        public Color TextColor { get; set; } = ThemeManager.TextPrimary;
        public Color PlaceholderColor { get; set; } = ThemeManager.TextMuted;
        
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value;
                Invalidate();
            }
        }
        
        public int BorderRadius { get; set; } = ThemeManager.InputBorderRadius;
        
        public ModernTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.OptimizedDoubleBuffer | 
                    ControlStyles.UserPaint, true);
            
            Font = ThemeManager.GetFont(ThemeManager.FontSizeMedium);
            ForeColor = TextColor;
            BackColor = BackgroundColor;
            BorderStyle = BorderStyle.None;
            Height = ThemeManager.ButtonHeight;
            Padding = new Padding(ThemeManager.PaddingMedium, ThemeManager.PaddingSmall, 
                                  ThemeManager.PaddingMedium, ThemeManager.PaddingSmall);
            
            SetupAnimation();
            
            GotFocus += (s, e) => { _isFocused = true; _animationTimer.Start(); };
            LostFocus += (s, e) => { _isFocused = false; _animationTimer.Start(); };
        }
        
        private void SetupAnimation()
        {
            _animationTimer = new Timer { Interval = 16 };
            _animationTimer.Tick += (s, e) =>
            {
                if ((_isFocused || _isHovered) && _animationFrame < 10)
                    _animationFrame++;
                else if (!_isFocused && !_isHovered && _animationFrame > 0)
                    _animationFrame--;
                
                if ((_isFocused && _animationFrame == 10) || 
                    (!_isFocused && !_isHovered && _animationFrame == 0))
                    _animationTimer.Stop();
                
                Invalidate();
            };
        }
        
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            if (!_isFocused) _animationTimer.Start();
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            if (!_isFocused) _animationTimer.Start();
        }
        
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            Rectangle rect = ClientRectangle;
            var borderRect = new Rectangle(0, 0, Width - 1, Height - 1);
            
            // Calculate border color with animation
            Color borderColor;
            if (_isFocused)
                borderColor = InterpolateColor(BorderColor, FocusColor, _animationFrame / 10.0f);
            else if (_isHovered)
                borderColor = InterpolateColor(BorderColor, HoverColor, _animationFrame / 10.0f);
            else
                borderColor = BorderColor;
            
            // Draw rounded border
            using (var path = ThemeManager.CreateRoundedRectangle(borderRect, BorderRadius))
            using (var pen = new Pen(borderColor, _isFocused ? 2 : 1))
            {
                g.DrawPath(pen, path);
            }
            
            // Draw bottom accent line when focused
            if (_isFocused)
            {
                using (var brush = new SolidBrush(FocusColor))
                {
                    var lineRect = new Rectangle(
                        ThemeManager.PaddingMedium, 
                        Height - 3, 
                        (int)(Width - ThemeManager.PaddingMedium * 2) * (_animationFrame / 10.0f), 
                        2);
                    g.FillRectangle(brush, lineRect);
                }
            }
            
            // Draw placeholder text
            if (string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(_placeholderText))
            {
                float alpha = _isFocused ? 0.5f : 1.0f;
                using (var brush = new SolidBrush(Color.FromArgb((int)(255 * alpha), PlaceholderColor)))
                {
                    var format = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit
                    };
                    
                    var textRect = new Rectangle(
                        ThemeManager.PaddingMedium + 2, 
                        0, 
                        Width - ThemeManager.PaddingMedium * 2 - 4, 
                        Height);
                    
                    g.DrawString(_placeholderText, Font, brush, textRect, format);
                }
            }
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
