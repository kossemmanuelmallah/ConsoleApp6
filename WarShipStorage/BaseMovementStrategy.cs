namespace WarShipStorage
{
    public abstract class BaseMovementStrategy
    {
        public abstract void Step(IMoveable ship, CanvasForShip canvas, double stepSize);
    }

    public class MoveToCenter : BaseMovementStrategy
    {
        public override void Step(IMoveable ship, CanvasForShip canvas, double stepSize)
        {
            double x = ship.GetX();
            double y = ship.GetY();
            double targetX = canvas.Width / 2.0;
            double targetY = canvas.Height / 2.0;

            if (x < targetX) x += stepSize;
            else if (x > targetX) x -= stepSize;

            if (y < targetY) y += stepSize;
            else if (y > targetY) y -= stepSize;

            canvas.ClampPosition(ref x, ref y);
            ship.SetX((int)x);
            ship.SetY((int)y);
        }
    }

    public class MoveToRightBottom : BaseMovementStrategy
    {
        public override void Step(IMoveable ship, CanvasForShip canvas, double stepSize)
        {
            double x = ship.GetX();
            double y = ship.GetY();
            double targetX = canvas.Width - DrawingWarShip.ShipWidth;
            double targetY = canvas.Height - DrawingWarShip.ShipHeight;

            if (x < targetX) x += stepSize;
            else if (x > targetX) x -= stepSize;

            if (y < targetY) y += stepSize;
            else if (y > targetY) y -= stepSize;

            canvas.ClampPosition(ref x, ref y);
            ship.SetX((int)x);
            ship.SetY((int)y);
        }
    }
}