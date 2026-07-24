using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace project.Theme
{
    /// <summary>
    /// Central theme manager for the BIIT Society Management System
    /// Defines a modern flat color palette and typography settings
    /// </summary>
    public static class ThemeManager
    {
        // Primary Colors - Modern Teal Palette
        public static readonly Color PrimaryColor = Color.FromArgb(26, 188, 156);      // Turquoise
        public static readonly Color PrimaryDark = Color.FromArgb(22, 160, 133);       // Darker Turquoise
        public static readonly Color PrimaryLight = Color.FromArgb(52, 211, 189);      // Light Turquoise
        
        // Secondary Colors
        public static readonly Color SecondaryColor = Color.FromArgb(52, 152, 219);    // Peter River Blue
        public static readonly Color AccentColor = Color.FromArgb(231, 76, 60);        // Alizarin Red
        public static readonly Color AccentHover = Color.FromArgb(192, 57, 43);        // Darker Red
        
        // Background Colors
        public static readonly Color BackgroundDark = Color.FromArgb(44, 62, 80);      // Midnight Blue
        public static readonly Color BackgroundLight = Color.FromArgb(245, 247, 250);  // Light Gray
        public static readonly Color BackgroundWhite = Color.FromArgb(255, 255, 255);  // Pure White
        public static readonly Color CardBackground = Color.FromArgb(255, 255, 255);   // White Cards
        public static readonly Color SidebarColor = Color.FromArgb(33, 47, 61);        // Dark Slate
        
        // Text Colors
        public static readonly Color TextPrimary = Color.FromArgb(44, 62, 80);         // Dark Blue Gray
        public static readonly Color TextSecondary = Color.FromArgb(127, 140, 141);    // Silver
        public static readonly Color TextLight = Color.FromArgb(255, 255, 255);        // White
        public static readonly Color TextMuted = Color.FromArgb(189, 195, 199);        // Light Gray
        
        // Status Colors
        public static readonly Color SuccessColor = Color.FromArgb(46, 204, 113);      // Nephritis Green
        public static readonly Color WarningColor = Color.FromArgb(241, 196, 15);      // Sun Flower Yellow
        public static readonly Color ErrorColor = Color.FromArgb(231, 76, 60);         // Alizarin Red
        public static readonly Color InfoColor = Color.FromArgb(52, 152, 219);         // Peter River
        
        // Border & Shadow
        public static readonly Color BorderColor = Color.FromArgb(230, 230, 230);      // Light Border
        public static readonly Color HoverColor = Color.FromArgb(22, 160, 133);        // Darker Turquoise
        public static readonly Color ShadowColor = Color.FromArgb(0, 0, 0, 40);        // Transparent Black
        
        // Typography
        public static readonly string FontFamilyPrimary = "Segoe UI";
        public static readonly string FontFamilySecondary = "Microsoft Sans Serif";
        
        // Font Sizes
        public static readonly float FontSizeSmall = 10F;
        public static readonly float FontSizeMedium = 12F;
        public static readonly float FontSizeLarge = 16F;
        public static readonly float FontSizeXLarge = 20F;
        public static readonly float FontSizeTitle = 28F;
        public static readonly float FontSizeHeading = 36F;

        // Spacing
        public static readonly int PaddingSmall = 8;
        public static readonly int PaddingMedium = 16;
        public static readonly int PaddingLarge = 24;
        public static readonly int PaddingXLarge = 32;
        public static readonly int MarginSmall = 8;
        public static readonly int MarginMedium = 16;
        public static readonly int MarginLarge = 24;
        
        // Control Dimensions
        public static readonly int ButtonHeight = 45;
        public static readonly int ButtonHeightSmall = 36;
        public static readonly int SidebarWidth = 280;
        public static readonly int TopBarHeight = 70;
        public static readonly int BorderRadius = 12;
        public static readonly int ButtonBorderRadius = 8;
        public static readonly int CardBorderRadius = 16;
        public static readonly int InputBorderRadius = 8;

        // Animation Timing (milliseconds)
        public static readonly int AnimationFast = 150;
        public static readonly int AnimationNormal = 300;
        public static readonly int AnimationSlow = 450;
        public static readonly int AnimationSlide = 500;

        // Helper method to get font
        public static Font GetFont(float size = FontSizeMedium, FontStyle style = FontStyle.Regular)
        {
            return new Font(FontFamilyPrimary, size, style, GraphicsUnit.Point);
        }

        // Helper method to get primary font bold
        public static Font GetFontBold(float size = FontSizeMedium)
        {
            return new Font(FontFamilyPrimary, size, FontStyle.Bold, GraphicsUnit.Point);
        }
        
        // Create rounded rectangle path
        public static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;
            
            if (diameter >= bounds.Width || diameter >= bounds.Height)
            {
                path.AddEllipse(bounds);
                return path;
            }
            
            var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);
            
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            
            path.CloseFigure();
            return path;
        }
        
        // Apply shadow effect
        public static void DrawShadow(Graphics g, Rectangle bounds, int blurRadius = 10, int opacity = 40)
        {
            var shadowColor = Color.FromArgb(opacity, 0, 0, 0);
            for (int i = 1; i <= blurRadius; i++)
            {
                var alpha = (int)(opacity * (1.0f - (float)i / blurRadius));
                var shadowRect = new Rectangle(
                    bounds.X + i, 
                    bounds.Y + i, 
                    bounds.Width, 
                    bounds.Height
                );
                using (var path = CreateRoundedRectangle(shadowRect, BorderRadius))
                using (var brush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillPath(brush, path);
                }
            }
        }
    }
}
