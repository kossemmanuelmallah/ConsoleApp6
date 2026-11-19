namespace WarShipStorage
{
    public class MoveableAdapterShip : IMoveable
    {
        private readonly DrawingWarShip _ship;

        public MoveableAdapterShip(DrawingWarShip ship) { _ship = ship; }

        public int GetX() => (int)_ship.X;
        public int GetY() => (int)_ship.Y;
        public void SetX(int x) => _ship.SetPosition(x, _ship.Y);
        public void SetY(int y) => _ship.SetPosition(_ship.X, y);
    }
}