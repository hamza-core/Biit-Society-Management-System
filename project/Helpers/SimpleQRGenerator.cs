using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace project.Helpers
{
    /// <summary>
    /// Simple QR Code Generator using pattern-based drawing
    /// Note: For production, use QRCoder library. This is a simplified version for demonstration.
    /// </summary>
    public static class SimpleQRGenerator
    {
        private static readonly int[,] PositionPattern = new int[7, 7]
        {
            {1,1,1,1,1,1,1},
            {1,0,0,0,0,0,1},
            {1,0,1,1,1,0,1},
            {1,0,1,1,1,0,1},
            {1,0,1,1,1,0,1},
            {1,0,0,0,0,0,1},
            {1,1,1,1,1,1,1}
        };

        public static Image GenerateQRCode(string data, int size = 200)
        {
            // Create a simple pattern-based QR-like code
            // In production, replace with actual QRCoder library
            var bitmap = new Bitmap(size, size);
            var moduleSize = size / 25; // 25x25 grid
            
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
                var brush = new SolidBrush(Color.Black);
                
                // Draw position patterns (three corners)
                DrawPositionPattern(g, brush, 0, 0, moduleSize);
                DrawPositionPattern(g, brush, 18, 0, moduleSize);
                DrawPositionPattern(g, brush, 0, 18, moduleSize);
                
                // Generate pseudo-random pattern based on data hash
                var hash = data.GetHashCode();
                var random = new Random(Math.Abs(hash));
                
                for (int row = 0; row < 25; row++)
                {
                    for (int col = 0; col < 25; col++)
                    {
                        // Skip position pattern areas
                        if ((row < 7 && col < 7) || 
                            (row < 7 && col > 17) || 
                            (row > 17 && col < 7))
                            continue;
                        
                        // Use data hash to determine module state
                        if (((hash >> ((row + col) % 32)) & 1) == 1)
                        {
                            g.FillRectangle(brush, col * moduleSize, row * moduleSize, moduleSize - 1, moduleSize - 1);
                        }
                    }
                }
                
                // Add timing patterns
                for (int i = 8; i < 17; i++)
                {
                    if (i % 2 == 0)
                    {
                        g.FillRectangle(brush, i * moduleSize, 6 * moduleSize, moduleSize - 1, moduleSize - 1);
                        g.FillRectangle(brush, 6 * moduleSize, i * moduleSize, moduleSize - 1, moduleSize - 1);
                    }
                }
            }
            
            return bitmap;
        }
        
        private static void DrawPositionPattern(Graphics g, Brush brush, int startX, int startY, int moduleSize)
        {
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (PositionPattern[row, col] == 1)
                    {
                        g.FillRectangle(brush, 
                            (startX + col) * moduleSize, 
                            (startY + row) * moduleSize, 
                            moduleSize - 1, 
                            moduleSize - 1);
                    }
                }
            }
        }
        
        public static Image GenerateEventCard(string participantName, string aridNo, string eventName, int participantNumber)
        {
            int width = 400;
            int height = 250;
            var card = new Bitmap(width, height);
            
            using (var g = Graphics.FromImage(card))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                
                // Background gradient
                using (var gradient = new LinearGradientBrush(
                    new Point(0, 0), 
                    new Point(width, height),
                    Color.FromArgb(255, 255, 255),
                    Color.FromArgb(245, 248, 250)))
                {
                    g.FillRectangle(gradient, 0, 0, width, height);
                }
                
                // Border
                using (var pen = new Pen(Color.FromArgb(52, 152, 219), 3))
                {
                    g.DrawRectangle(pen, 2, 2, width - 5, height - 5);
                }
                
                // Header
                using (var headerBrush = new SolidBrush(Color.FromArgb(52, 152, 219)))
                {
                    g.FillRectangle(headerBrush, 3, 3, width - 6, 50);
                }
                
                // Event name
                using (var font = new Font("Segoe UI", 14, FontStyle.Bold))
                {
                    g.DrawString(eventName, font, Brushes.White, new PointF(15, 15));
                }
                
                // Participant Number Badge
                using (var badgeBrush = new SolidBrush(Color.FromArgb(231, 76, 60)))
                {
                    g.FillEllipse(badgeBrush, width - 70, 10, 50, 50);
                }
                using (var font = new Font("Segoe UI", 10, FontStyle.Bold))
                {
                    g.DrawString($"#{participantNumber:D3}", font, Brushes.White, 
                        new PointF(width - 55, 25));
                }
                
                // QR Code
                var qrData = $"{aridNo}|{eventName}|{participantNumber}";
                var qrImage = GenerateQRCode(qrData, 120);
                g.DrawImage(qrImage, width - 140, 65, 120, 120);
                qrImage.Dispose();
                
                // Participant Info
                using (var fontBold = new Font("Segoe UI", 12, FontStyle.Bold))
                using (var fontRegular = new Font("Segoe UI", 11, FontStyle.Regular))
                {
                    g.DrawString("Participant:", fontRegular, Brushes.Gray, new PointF(20, 70));
                    g.DrawString(participantName, fontBold, Brushes.Black, new PointF(20, 95));
                    
                    g.DrawString("ARID No:", fontRegular, Brushes.Gray, new PointF(20, 130));
                    g.DrawString(aridNo.ToUpper(), fontBold, Brushes.Black, new PointF(20, 155));
                }
                
                // Footer
                using (var fontSmall = new Font("Segoe UI", 8, FontStyle.Italic))
                {
                    g.DrawString("Scan QR code for attendance", fontSmall, 
                        new SolidBrush(Color.Gray), new PointF(20, 210));
                }
                
                // Decorative line
                using (var linePen = new Pen(Color.FromArgb(52, 152, 219), 2))
                {
                    g.DrawLine(linePen, 20, 190, width - 150, 190);
                }
            }
            
            return card;
        }
        
        public static void PrintCard(Image card)
        {
            // In a real application, this would send to printer
            // For now, save to file as demonstration
            var outputPath = Path.Combine(Path.GetTempPath(), $"EventCard_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            card.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
            System.Diagnostics.Process.Start(outputPath);
        }
    }
}
