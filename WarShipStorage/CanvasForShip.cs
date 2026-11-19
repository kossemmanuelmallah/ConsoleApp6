namespace WarShipStorage
{
    public class CanvasForShip
    {
        public int Width { get; }
        public int Height { get; }

        public CanvasForShip(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void ClampPosition(ref double x, ref double y)
        {
            double maxX = Width - DrawingWarShip.ShipWidth;
            double maxY = Height - DrawingWarShip.ShipHeight;

            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > maxX) x = maxX;
            if (y > maxY) y = maxY;
        }
    }
}