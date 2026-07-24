using System;
using System.Drawing;
using System.Windows.Forms;

namespace project.Theme.Controls
{
    /// <summary>
    /// Modern sidebar panel with consistent styling
    /// </summary>
    public class SidebarPanel : Panel
    {
        public Color SideBarColor { get; set; } = Theme.ThemeManager.PrimaryDark;
        
        public SidebarPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.OptimizedDoubleBuffer | 
                    ControlStyles.UserPaint, true);
            
            Dock = DockStyle.Left;
            Width = Theme.ThemeManager.SidebarWidth;
            BackColor = SideBarColor;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            
            Graphics g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            // Optional: Draw subtle gradient or accent line on right edge
            using (var pen = new Pen(Theme.ThemeManager.PrimaryLight, 2))
            {
                g.DrawLine(pen, Width - 1, 0, Width - 1, Height);
            }
        }
    }
}
