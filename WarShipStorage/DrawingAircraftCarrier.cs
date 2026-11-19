using System.Drawing;

namespace WarShipStorage
{
    public class DrawingAircraftCarrier : DrawingWarShip
    {
        public DrawingAircraftCarrier(EntityAircraftCarrier entity, int startX, int startY)
            : base(entity, startX, startY) { }

        public override void Draw(Graphics g)
        {
            var state = g.Save();
            int cx = (int)X + ShipWidth / 2;
            int cy = (int)Y + ShipHeight / 2;
            g.TranslateTransform(cx, cy);
            g.RotateTransform(Rotation);
            g.TranslateTransform(-cx, -cy);

            using var brush = new SolidBrush(Entity.MainColor);
            g.FillRectangle(brush, (int)X, (int)Y, ShipWidth, ShipHeight);
            g.FillEllipse(Brushes.Black, (int)X + 5, (int)Y + 5, 5, 5);
            g.FillEllipse(Brushes.Black, (int)X + 30, (int)Y + 5, 5, 5);

            var carrier = (EntityAircraftCarrier)Entity;
            using var pen = new Pen(carrier.DeckColor, 2);
            g.DrawLine(pen, (int)X, (int)Y + ShipHeight / 2,
                            (int)X + ShipWidth, (int)Y + ShipHeight / 2);
            g.FillRectangle(Brushes.Gray, (int)X + 10, (int)Y + ShipHeight / 2 + 3, 5, 5);
            g.FillRectangle(Brushes.Gray, (int)X + 25, (int)Y + ShipHeight / 2 + 3, 5, 5);

            g.Restore(state);
        }
    }
}