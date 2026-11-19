using System;

namespace SportRental.Entities
{
    public class RentalItem
    {
        public int Id { get; private set; }
        public int RentalId { get; private set; }
        public int EquipmentId { get; private set; }

        private RentalItem(int id, int rentalId, int equipmentId)
        {
            Id = id;
            RentalId = rentalId;
            EquipmentId = equipmentId;
        }

        public static RentalItem Create(int id, int rentalId, int equipmentId)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Идентификатор не может быть отрицательным.");
            if (rentalId <= 0)
                throw new ArgumentOutOfRangeException(nameof(rentalId), "Некорректный идентификатор проката.");
            if (equipmentId <= 0)
                throw new ArgumentOutOfRangeException(nameof(equipmentId), "Некорректный идентификатор инвентаря.");

            return new RentalItem(id, rentalId, equipmentId);
        }
    }
}