using System;
using SportRental.Enums;

namespace SportRental.Entities
{
    public class Equipment
    {
        public int Id { get; private set; }
        public int EquipmentTypeId { get; private set; }
        public int EquipmentSizeId { get; private set; }
        public string InventoryNumber { get; private set; }
        public EquipmentCondition Condition { get; private set; }

        private Equipment(int id, int equipmentTypeId, int equipmentSizeId,
                          string inventoryNumber, EquipmentCondition condition)
        {
            Id = id;
            EquipmentTypeId = equipmentTypeId;
            EquipmentSizeId = equipmentSizeId;
            InventoryNumber = inventoryNumber;
            Condition = condition;
        }

        public static Equipment Create(int id, int equipmentTypeId, int equipmentSizeId,
                                       string inventoryNumber, EquipmentCondition condition)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Идентификатор не может быть отрицательным.");
            if (equipmentTypeId <= 0)
                throw new ArgumentOutOfRangeException(nameof(equipmentTypeId), "Выберите вид инвентаря.");
            if (equipmentSizeId <= 0)
                throw new ArgumentOutOfRangeException(nameof(equipmentSizeId), "Выберите размер.");
            if (string.IsNullOrWhiteSpace(inventoryNumber))
                throw new ArgumentException("Инвентарный номер не может быть пустым.", nameof(inventoryNumber));

            return new Equipment(id, equipmentTypeId, equipmentSizeId,
                                 inventoryNumber.Trim(), condition);
        }

        public override string ToString() => InventoryNumber;
    }
}