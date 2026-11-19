using System.Drawing.Drawing2D;

namespace ProjectWarship;

/// <summary>Реалистичный фон: небо, солнце, облака, море, волны</summary>
public static class SeaScene
{
    public static void DrawFull(Graphics g, int w, int h)
    {
        int hor = Math.Max(20, h * 12 / 100);
        int sunX = w * 3 / 4;

        using (var sky = new LinearGradientBrush(new Rectangle(0, 0, w, hor + 1),
               Color.FromArgb(62, 122, 196), Color.FromArgb(222, 237, 250), 90f))
            g.FillRectangle(sky, 0, 0, w, hor + 1);

        var r = new Rectangle(sunX - 130, hor - 130, 260, 260);
        using (var path = new GraphicsPath())
        {
            path.AddEllipse(r);
            using var glow = new PathGradientBrush(path)
            {
                CenterColor = Color.FromArgb(235, 255, 250, 225),
                SurroundColors = new[] { Color.FromArgb(0, 255, 240, 200) }
            };
            g.FillEllipse(glow, r);
        }

        using (var cl = new SolidBrush(Color.FromArgb(85, 255, 255, 255)))
        {
            g.FillEllipse(cl, 60, hor / 4, 170, 16);
            g.FillEllipse(cl, 120, hor / 4 - 6, 110, 14);
            g.FillEllipse(cl, w / 2, hor / 3, 210, 15);
        }

        DrawSea(g, new Rectangle(0, hor, w, h - hor), 7, sunX);

        using var hp = new Pen(Color.FromArgb(110, 255, 255, 255), 1f);
        g.DrawLine(hp, 0, hor, w, hor);
    }

    public static void DrawSea(Graphics g, Rectangle area, int seed, int? sunX = null)
    {
        using (var sea = new LinearGradientBrush(new Rectangle(area.X, area.Y, area.Width, area.Height + 1),
               Color.FromArgb(78, 140, 172), Color.FromArgb(4, 30, 60), 90f))
            g.FillRectangle(sea, area);

        var rnd = new Random(seed);

        // блики солнца на воде
        if (sunX.HasValue)
        {
            using var sb = new SolidBrush(Color.White);
            for (int i = 0; i < 16; i++)
            {
                float y = area.Y + 3 + i * (area.Height / 22f);
                float ww = 14 + i * 9 + rnd.Next(10);
                int a = Math.Max(10, 90 - i * 5);
                sb.Color = Color.FromArgb(a, 255, 250, 225);
                g.FillEllipse(sb, sunX.Value - ww / 2 + rnd.Next(-12, 12), y, ww, 2.5f);
            }
        }

        // волны
        int n = Math.Max(40, area.Width * area.Height / 1800);
        using var p = new Pen(Color.White, 1f);
        for (int i = 0; i < n; i++)
        {
            float y = area.Y + 4 + (float)rnd.NextDouble() * (area.Height - 8);
            float x = (float)rnd.NextDouble() * area.Width;
            float k = (y - area.Y) / (area.Height + 1f);
            float len = 14 + (float)rnd.NextDouble() * 70 * k;
            p.Color = Color.FromArgb(20, 0, 20, 40);
            g.DrawLine(p, x, y + 1.5f, x + len, y + 1.5f);
            p.Color = Color.FromArgb(25 + rnd.Next(50), 255, 255, 255);
            g.DrawLine(p, x, y, x + len, y);
        }
    }
}