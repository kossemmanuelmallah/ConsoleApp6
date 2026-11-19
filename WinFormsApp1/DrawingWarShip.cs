using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace WarShipStorage;

/// <summary>
/// Класс-прорисовка: координаты, размеры, перемещение, рисование.
/// Корабль рисуется в локальных координатах 220 x 100 (нос справа).
/// </summary>
public class DrawingWarShip
{
    public const int ShipWidth = 220;
    public const int ShipHeight = 100;
    private const double PixelScale = 800;   // перевод Step в пиксели

    protected EntityWarShip _entity;
    protected int _x, _y;

    public EntityWarShip Entity => _entity;
    public int X => _x;
    public int Y => _y;
    public int Width => ShipWidth;
    public int Height => ShipHeight;

    /// <summary>Шаг в пикселях (из Step сущности)</summary>
    public int StepInPixels => Math.Clamp((int)Math.Round(_entity.Step * PixelScale), 3, 60);

    public DrawingWarShip(EntityWarShip entity)
    {
        _entity = entity;
    }

    /// <summary>Создаёт нужную прорисовку по типу сущности</summary>
    public static DrawingWarShip Create(EntityWarShip entity) =>
        entity is EntityAircraftCarrier carrier
            ? new DrawingAircraftCarrier(carrier)
            : new DrawingWarShip(entity);

    public void SetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void MoveLeft() { _x -= StepInPixels; }
    public void MoveRight() { _x += StepInPixels; }
    public void MoveUp() { _y -= StepInPixels; }
    public void MoveDown() { _y += StepInPixels; }

    // ===================== ОТРИСОВКА =====================

    public virtual void Draw(Graphics g)
    {
        var state = g.Save();
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.AntiAlias;
        g.TranslateTransform(_x, _y);
        DrawBody(g);
        g.Restore(state);
    }

    protected virtual void DrawBody(Graphics g)
    {
        DrawWaterShadow(g);
        DrawHull(g);
        DrawRailing(g);
        DrawSuperstructure(g);
        DrawMast(g);
        DrawGuns(g);
        DrawEnsign(g);
        DrawFoam(g);
    }

    protected virtual string HullNumber => "715";
    protected virtual Color SuperstructureColor => Color.FromArgb(142, 150, 158);

    // ---------- вспомогательные ----------
    protected static PointF P(float x, float y) => new(x, y);

    protected static Color Shade(Color c, float f) => f >= 0
        ? Color.FromArgb(c.A, (int)(c.R + (255 - c.R) * f), (int)(c.G + (255 - c.G) * f), (int)(c.B + (255 - c.B) * f))
        : Color.FromArgb(c.A, (int)(c.R * (1 + f)), (int)(c.G * (1 + f)), (int)(c.B * (1 + f)));

    protected static void Poly(Graphics g, PointF[] pts, Color top, Color bottom, float angle = 90f)
    {
        float minX = pts.Min(p => p.X), maxX = pts.Max(p => p.X);
        float minY = pts.Min(p => p.Y), maxY = pts.Max(p => p.Y);
        var r = new RectangleF(minX, minY, Math.Max(1, maxX - minX), Math.Max(1, maxY - minY));
        using var br = new LinearGradientBrush(r, top, bottom, angle);
        g.FillPolygon(br, pts);
        using var pen = new Pen(Shade(bottom, -0.4f), 0.8f);
        g.DrawPolygon(pen, pts);
    }

    protected static void Rect(Graphics g, RectangleF r, Color top, Color bottom, float angle = 90f)
    {
        if (r.Width < 1 || r.Height < 1) return;
        using var br = new LinearGradientBrush(r, top, bottom, angle);
        g.FillRectangle(br, r);
    }

    // ---------- корпус ----------
    protected virtual GraphicsPath CreateHullPath()
    {
        var p = new GraphicsPath();
        p.AddLine(0, 50, 180, 50);
        p.AddBezier(180, 50, 198, 50, 210, 46, 218, 38);
        p.AddBezier(218, 38, 213, 60, 204, 76, 192, 86);
        p.AddLine(192, 86, 14, 86);
        p.AddLine(14, 86, 4, 70);
        p.AddLine(4, 70, 0, 50);
        p.CloseFigure();
        return p;
    }

    protected void DrawWaterShadow(Graphics g)
    {
        using var sh = new SolidBrush(Color.FromArgb(90, 0, 14, 28));
        g.FillEllipse(sh, 6, 80, 204, 14);
        using var rf = new SolidBrush(Color.FromArgb(45, _entity.MainColor));
        g.FillEllipse(rf, 22, 88, 172, 8);
    }

    protected void DrawHull(Graphics g)
    {
        var body = _entity.MainColor;
        using var hull = CreateHullPath();
        var hb = hull.GetBounds();
        using (var br = new LinearGradientBrush(hb, Shade(body, 0.30f), Shade(body, -0.35f), 90f))
            g.FillPath(br, hull);

        g.SetClip(hull);

        // подводная часть
        Rect(g, new RectangleF(0, 74, 220, 14), Color.FromArgb(165, 42, 36), Color.FromArgb(88, 16, 16));
        using (var bt = new Pen(Color.FromArgb(210, 235, 235, 235), 1.6f))
            g.DrawLine(bt, 0, 73.5f, 220, 73.5f);
        using (var hi = new Pen(Color.FromArgb(120, 255, 255, 255), 2f))
            g.DrawLine(hi, 0, 52, 190, 52);

        // иллюминаторы
        using (var pb = new SolidBrush(Color.FromArgb(18, 24, 34)))
        using (var pp = new Pen(Color.FromArgb(160, 225, 225, 225), 0.9f))
            for (int i = 0; i < 12; i++)
            {
                float px = 20 + i * 11;
                g.FillEllipse(pb, px, 57, 4.2f, 4.2f);
                g.DrawEllipse(pp, px, 57, 4.2f, 4.2f);
            }

        // бортовой номер
        using (var f = new Font("Arial", 7.5f, FontStyle.Bold))
            g.DrawString(HullNumber, f, Brushes.WhiteSmoke, 158, 55);

        // вода у борта
        using (var wb = new LinearGradientBrush(new Rectangle(0, 78, 220, 12),
               Color.FromArgb(70, 40, 100, 140), Color.FromArgb(175, 8, 40, 70), 90f))
            g.FillRectangle(wb, 0, 78, 220, 12);

        g.ResetClip();

        using var outline = new Pen(Shade(body, -0.65f), 1.2f);
        g.DrawPath(outline, hull);
    }

    private static void DrawRailing(Graphics g)
    {
        using var pen = new Pen(Color.FromArgb(150, 205, 210, 215), 0.8f);
        for (float x = 8; x <= 174; x += 7) g.DrawLine(pen, x, 50, x, 45);
        g.DrawLine(pen, 8, 45, 174, 45);
    }

    private void DrawSuperstructure(Graphics g)
    {
        var sup = SuperstructureColor;

        // труба
        Poly(g, new[] { P(66, 50), P(70, 24), P(90, 24), P(94, 50) }, Shade(sup, 0.10f), Shade(sup, -0.45f), 0f);
        using (var dark = new SolidBrush(Color.FromArgb(32, 32, 36)))
            g.FillEllipse(dark, 70, 21, 20, 6);
        Rect(g, new RectangleF(68, 36, 24, 4), Color.FromArgb(120, 25, 25, 28), Color.FromArgb(120, 25, 25, 28));

        // надстройка
        Poly(g, new[] { P(98, 50), P(102, 28), P(144, 28), P(154, 50) }, Shade(sup, 0.25f), Shade(sup, -0.30f));
        for (int i = 0; i < 6; i++)
            Rect(g, new RectangleF(108 + i * 6.2f, 32, 4.6f, 4),
                 Color.FromArgb(135, 200, 238), Color.FromArgb(20, 50, 80));

        // ходовой мостик
        Poly(g, new[] { P(110, 28), P(114, 15), P(140, 15), P(144, 28) }, Shade(sup, 0.35f), Shade(sup, -0.20f));
        for (int i = 0; i < 4; i++)
            Rect(g, new RectangleF(116 + i * 6.2f, 19, 4.6f, 3.5f),
                 Color.FromArgb(150, 210, 242), Color.FromArgb(25, 60, 95));
        Rect(g, new RectangleF(112, 12.5f, 30, 2.5f), Color.FromArgb(110, 115, 120), Color.FromArgb(55, 58, 62));
    }

    private static void DrawMast(Graphics g)
    {
        using (var mast = new Pen(Color.FromArgb(62, 67, 72), 2.4f))
            g.DrawLine(mast, 127, 13, 127, 5);
        using (var yard = new Pen(Color.FromArgb(62, 67, 72), 1.5f))
            g.DrawLine(yard, 121, 9, 133, 9);
        using (var stay = new Pen(Color.FromArgb(120, 100, 105, 110), 0.8f))
        {
            g.DrawLine(stay, 127, 5, 214, 41);
            g.DrawLine(stay, 127, 5, 80, 23);
        }
        // радар
        Rect(g, new RectangleF(121, 5, 12, 2), Color.FromArgb(205, 210, 215), Color.FromArgb(90, 95, 100));
        using var rp = new Pen(Color.FromArgb(50, 55, 60), 1.4f);
        g.DrawArc(rp, 120, 0, 14, 10, 180, 180);
    }

    // ---------- орудия (круги) ----------
    private static void DrawGuns(Graphics g)
    {
        DrawTurret(g, 158, 41.5f, 14, 8.5f, 12);
        DrawTurret(g, 176, 37.5f, 24, 12, 18);
    }

    private static void DrawTurret(Graphics g, float x, float y, float w, float h, float barrelLen)
    {
        Rect(g, new RectangleF(x + w * 0.55f, y + h * 0.28f, barrelLen, 2.6f),
             Color.FromArgb(165, 170, 175), Color.FromArgb(50, 52, 56));
        using (var baseB = new SolidBrush(Color.FromArgb(55, 58, 62)))
            g.FillEllipse(baseB, x - 1, y + h - 3.5f, w + 2, 6);
        var r = new RectangleF(x, y, w, h);
        using (var b = new LinearGradientBrush(r, Color.FromArgb(190, 195, 200), Color.FromArgb(60, 64, 68), 90f))
            g.FillEllipse(b, r);
        using var p = new Pen(Color.FromArgb(40, 42, 45), 1f);
        g.DrawEllipse(p, r);
    }

    private static void DrawEnsign(Graphics g)
    {
        using (var pole = new Pen(Color.FromArgb(85, 85, 85), 1.4f))
            g.DrawLine(pole, 5, 50, 5, 28);
        using var w = new SolidBrush(Color.White);
        using var b = new SolidBrush(Color.RoyalBlue);
        using var r = new SolidBrush(Color.Firebrick);
        g.FillRectangle(w, 5, 28, 14, 4);
        g.FillRectangle(b, 5, 32, 14, 4);
        g.FillRectangle(r, 5, 36, 14, 4);
    }

    protected static void DrawFoam(Graphics g)
    {
        using (var foam = new SolidBrush(Color.FromArgb(125, 255, 255, 255)))
        {
            g.FillEllipse(foam, 4, 75, 200, 7);
            g.FillEllipse(foam, 186, 70, 30, 12);
        }
        using (var spray = new SolidBrush(Color.FromArgb(70, 255, 255, 255)))
            g.FillEllipse(spray, 192, 64, 26, 16);
        using (var wake = new SolidBrush(Color.FromArgb(85, 255, 255, 255)))
        {
            g.FillEllipse(wake, -14, 76, 30, 8);
            g.FillEllipse(wake, -10, 82, 22, 5);
        }
    }
}