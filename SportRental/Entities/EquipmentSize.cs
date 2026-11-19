using System;

namespace SportRental.Entities
{
    public class EquipmentSize
    {
        public int Id { get; private set; }
        public string Value { get; private set; }

        private EquipmentSize(int id, string value)
        {
            Id = id;
            Value = value;
        }

        public static EquipmentSize Create(int id, string value)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Идентификатор не может быть отрицательным.");
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Размер не может быть пустым.", nameof(value));

            return new EquipmentSize(id, value.Trim());
        }

        public override string ToString() => Value;
    }
}