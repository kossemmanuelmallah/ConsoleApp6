using System.Drawing.Drawing2D;

namespace WarShipStorage;

/// <summary>Продвинутая прорисовка: авианосец (полётная палуба, самолёты, катапульта)</summary>
public class DrawingAircraftCarrier : DrawingWarShip
{
    private readonly EntityAircraftCarrier _carrier;
    private static readonly float[] PlaneX = { 16, 30, 44, 58, 72, 86, 100, 160, 176 };

    public DrawingAircraftCarrier(EntityAircraftCarrier carrier) : base(carrier)
    {
        _carrier = carrier;
    }

    protected override string HullNumber => "CV-12";

    protected override GraphicsPath CreateHullPath()
    {
        var p = new GraphicsPath();
        p.AddLine(0, 46, 208, 46);
        p.AddBezier(208, 46, 216, 47, 220, 52, 217, 60);
        p.AddBezier(217, 60, 214, 72, 205, 80, 192, 86);
        p.AddLine(192, 86, 14, 86);
        p.AddLine(14, 86, 4, 70);
        p.AddLine(4, 70, 0, 46);
        p.CloseFigure();
        return p;
    }

    protected override void DrawBody(Graphics g)
    {
        DrawWaterShadow(g);
        DrawHull(g);
        DrawFlightDeck(g);
        DrawIsland(g);
        DrawAircraft(g);
        DrawFoam(g);
    }

    private void DrawFlightDeck(Graphics g)
    {
        var deck = _carrier.DeckColor;

        // боковая кромка палубы
        Poly(g, new[] { P(0, 42), P(220, 42), P(214, 48), P(4, 48) },
             Color.FromArgb(120, 126, 132), Color.FromArgb(62, 66, 70));

        // верхняя поверхность (цвет палубы)
        Poly(g, new[] { P(0, 42), P(9, 35), P(211, 35), P(220, 42) },
             Shade(deck, 0.22f), Shade(deck, -0.28f));

        // разметка
        using (var edge = new Pen(Color.FromArgb(190, 245, 215, 70), 1f))
            g.DrawLine(edge, 3, 41, 217, 41);
        using (var center = new Pen(Color.FromArgb(210, 255, 255, 255), 1f) { DashStyle = DashStyle.Dash })
            g.DrawLine(center, 16, 38.5f, _carrier.HasCatapult ? 146 : 206, 38.5f);

        // катапульта
        if (_carrier.HasCatapult)
        {
            using var cat = new Pen(Color.FromArgb(200, 30, 32, 36), 1f);
            g.DrawLine(cat, 150, 37.2f, 210, 37.2f);
            g.DrawLine(cat, 150, 39.8f, 210, 39.8f);
            using var shuttle = new SolidBrush(Color.FromArgb(225, 230, 190, 40));
            g.FillRectangle(shuttle, 152, 36.6f, 5, 3.6f);
        }
    }

    private void DrawIsland(Graphics g)
    {
        var sup = SuperstructureColor;

        Poly(g, new[] { P(122, 38), P(125, 20), P(146, 20), P(150, 38) }, Shade(sup, 0.30f), Shade(sup, -0.30f));
        for (int i = 0; i < 4; i++)
            Rect(g, new RectangleF(129 + i * 4.6f, 24, 3.4f, 3.4f),
                 Color.FromArgb(150, 205, 240), Color.FromArgb(25, 60, 95));

        Poly(g, new[] { P(128, 20), P(130, 12), P(142, 12), P(144, 20) }, Shade(sup, 0.38f), Shade(sup, -0.20f));
        Rect(g, new RectangleF(131, 14.5f, 10, 2.5f), Color.FromArgb(150, 210, 242), Color.FromArgb(25, 60, 95));

        // мачта и радар
        using (var mast = new Pen(Color.FromArgb(62, 67, 72), 2f))
            g.DrawLine(mast, 135, 12, 135, 4);
        using (var yard = new Pen(Color.FromArgb(62, 67, 72), 1.3f))
            g.DrawLine(yard, 130, 8, 140, 8);
        Rect(g, new RectangleF(129, 4, 12, 2), Color.FromArgb(205, 210, 215), Color.FromArgb(90, 95, 100));
        using var rp = new Pen(Color.FromArgb(50, 55, 60), 1.3f);
        g.DrawArc(rp, 129, 0, 12, 8, 180, 180);
    }

    private void DrawAircraft(Graphics g)
    {
        int n = Math.Min(_carrier.AircraftCount, PlaneX.Length);
        for (int i = 0; i < n; i++)
            DrawPlane(g, PlaneX[i], 40f);
    }

    private static void DrawPlane(Graphics g, float x, float y)
    {
        using var body = new SolidBrush(Color.FromArgb(95, 105, 118));
        using var dark = new SolidBrush(Color.FromArgb(55, 62, 72));
        using var glass = new SolidBrush(Color.FromArgb(160, 190, 220));

        g.FillEllipse(body, x, y - 4.5f, 11, 3.2f);                                            // фюзеляж
        g.FillPolygon(dark, new[] { P(x + 0.5f, y - 3.5f), P(x - 1.5f, y - 9f), P(x + 2.5f, y - 3.5f) });   // киль
        g.FillPolygon(dark, new[] { P(x + 4, y - 3.2f), P(x + 8, y - 3.2f), P(x + 7, y - 0.5f), P(x + 3, y - 0.5f) }); // крыло
        g.FillEllipse(glass, x + 7.5f, y - 4.2f, 2.5f, 1.8f);                                  // кабина
    }
}