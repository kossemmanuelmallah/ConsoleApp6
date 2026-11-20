using System.Drawing;

namespace WarShipStorage
{
    public class EntityWarShip
    {
        public int Speed { get; init; }
        public double Displacement { get; init; }
        public Color MainColor { get; init; }

        public double Step => Speed * 10.0 / Displacement;
    }
}