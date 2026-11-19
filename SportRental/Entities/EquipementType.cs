using System;

namespace SportRental.Entities
{
    public class EquipmentType
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private EquipmentType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static EquipmentType Create(int id, string name)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Идентификатор не может быть отрицательным.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название вида инвентаря не может быть пустым.", nameof(name));

            return new EquipmentType(id, name.Trim());
        }

        public override string ToString() => Name;
    }
}