using System;

namespace SportRental.Enums
{
    [Flags]
    public enum EquipmentCondition
    {
        None = 0,
        Good = 1,
        NeedsRepair = 2,
        Broken = 4,
        WrittenOff = 8
    }
}
