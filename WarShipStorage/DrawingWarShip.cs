using System.Drawing;

namespace WarShipStorage
{
    public class DrawingWarShip
    {
        public EntityWarShip Entity { get; }
        public double X { get; set; }       
        public double Y { get; set; }       
        public int Rotation { get; set; } = 0;

        public const int ShipWidth = 40;
        public const int ShipHeight = 30;

        public DrawingWarShip(EntityWarShip entity, int startX, int startY)
        {
            Entity = entity;
            X = startX;
            Y = startY;
        }

        public void SetPosition(double x, double y) { X = x; Y = y; }
        public void Rotate(int angle) { Rotation = (angle % 360 + 360) % 360; }
        public void RotateNext() { Rotation = (Rotation + 90) % 360; }

        public void MoveLeft(double step) { X -= step; }
        public void MoveRight(double step) { X += step; }
        public void MoveUp(double step) { Y -= step; }
        public void MoveDown(double step) { Y += step; }

        public virtual void Draw(Graphics g)
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

            g.Restore(state);
        }
    }
}