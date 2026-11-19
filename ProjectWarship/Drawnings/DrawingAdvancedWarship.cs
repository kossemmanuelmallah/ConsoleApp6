using System.Drawing.Drawing2D;

namespace ProjectWarship;

/// <summary>Продвинутый объект: боевой корабль (башни, вертолёт, радар, цветовая полоса)</summary>
public class DrawingAdvancedWarship : DrawingWarship
{
    public DrawingAdvancedWarship(int speed, double weight, Color bodyColor, Color additionalColor,
        bool guns, bool helipad, bool radar)
        : base(new EntityAdvancedWarship(speed, weight, bodyColor, additionalColor, guns, helipad, radar))
    {
    }

    protected override Color SuperstructureColor =>
        (Entity as EntityAdvancedWarship)?.AdditionalColor ?? base.SuperstructureColor;

    protected override void DrawShip(Graphics g)
    {
        base.DrawShip(g);
        if (Entity is not EntityAdvancedWarship e) return;

        // цветная полоса по борту
        using (var stripe = new Pen(Color.FromArgb(225, e.AdditionalColor), 2.5f))
            g.DrawLine(stripe, 6, 66, 200, 66);

        // вертолётная площадка и вертолёт
        if (e.Helipad)
        {
            using (var pad = new SolidBrush(Color.FromArgb(72, 77, 82)))
                g.FillEllipse(pad, 12, 45, 52, 9);
            using (var ring = new Pen(Color.FromArgb(230, 240, 240, 235), 1.2f))
                g.DrawEllipse(ring, 16, 46.5f, 44, 6);
            using (var f = new Font("Arial", 5.5f, FontStyle.Bold))
                g.DrawString("H", f, Brushes.WhiteSmoke, 33, 44.5f);

            var olive = Color.FromArgb(58, 66, 58);
            using (var hb = new SolidBrush(olive)) g.FillEllipse(hb, 26, 36, 18, 8);
            using (var gl = new SolidBrush(Color.FromArgb(160, 170, 210, 230))) g.FillEllipse(gl, 37, 37, 6, 5);
            using (var tail = new Pen(olive, 2f)) g.DrawLine(tail, 27, 39, 13, 35);
            using (var tr = new Pen(Color.FromArgb(150, 30, 30, 30), 1f)) g.DrawLine(tr, 13, 32, 13, 38);
            using (var rotor = new Pen(Color.FromArgb(170, 30, 30, 30), 1f))
            {
                g.DrawLine(rotor, 17, 34.5f, 53, 34.5f);
                g.DrawLine(rotor, 35, 34.5f, 35, 36);
            }
            using (var skid = new Pen(Color.FromArgb(40, 40, 40), 1f)) g.DrawLine(skid, 28, 46, 42, 46);
        }

        // артиллерия
        if (e.Guns)
        {
            DrawTurret(g, 158, 41.5f, 14, 8.5f, 12);
            DrawTurret(g, 176, 37.5f, 24, 12, 18);
        }

        // радар
        if (e.Radar)
        {
            Rect(g, new RectangleF(121, 5, 12, 2), Color.FromArgb(205, 210, 215), Color.FromArgb(90, 95, 100));
            using var rp = new Pen(Color.FromArgb(50, 55, 60), 1.4f);
            g.DrawArc(rp, 120, 0, 14, 10, 180, 180);
        }
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
}