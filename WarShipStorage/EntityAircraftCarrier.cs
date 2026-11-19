using System.Drawing;

namespace WarShipStorage
{
    public class EntityAircraftCarrier : EntityWarShip
    {
        public Color DeckColor { get; init; }
        public int AircraftCount { get; init; }
        public bool HasCatapult { get; init; }
    }
}