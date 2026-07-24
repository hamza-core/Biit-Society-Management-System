using System;
using System.Drawing;
using System.Windows.Forms;

namespace project.Theme.Controls
{
    /// <summary>
    /// Main content viewport panel for SPA layout
    /// </summary>
    public class ViewportPanel : Panel
    {
        public Color ViewportColor { get; set; } = Theme.ThemeManager.BackgroundLight;
        
        public ViewportPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.OptimizedDoubleBuffer | 
                    ControlStyles.UserPaint, true);
            
            Dock = DockStyle.Fill;
            BackColor = ViewportColor;
            AutoScroll = true;
            Padding = new Padding(Theme.ThemeManager.PaddingLarge);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            
            Graphics g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            // Optional: Draw subtle background pattern or keep clean
        }
    }
}
