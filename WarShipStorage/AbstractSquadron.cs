namespace WarShipStorage
{
    public abstract class AbstractSquadron
    {
        protected WarShipStorage.ICollection<EntityWarShip> _ships
            = new ArrayCollection<EntityWarShip>();

        public WarShipStorage.ICollection<EntityWarShip> Ships => _ships;

        public static AbstractSquadron operator +(AbstractSquadron s, EntityWarShip ship)
        {
            s._ships.Add(ship);
            return s;
        }

        public static AbstractSquadron operator -(AbstractSquadron s, int index)
        {
            s._ships.Remove(index);
            return s;
        }
    }
}