namespace WarShipStorage
{
    public class Squadron : AbstractSquadron
    {
        public int GetCombatPower()
        {
            int total = 0;
            for (int i = 0; i < _ships.Count; i++)
                total += _ships.Get(i).Speed;
            return total;
        }
    }
}