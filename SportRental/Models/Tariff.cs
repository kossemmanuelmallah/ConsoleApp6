using System;
using SportRental.Enums;

namespace SportRental.Entities
{
    public class Tariff
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public RentalPeriod Period { get; private set; }
        public decimal Price { get; private set; }

        private Tariff(int id, string name, RentalPeriod period, decimal price)
        {
            Id = id;
            Name = name;
            Period = period;
            Price = price;
        }

        public static Tariff Create(int id, string name, RentalPeriod period, decimal price)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Идентификатор не может быть отрицательным.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название тарифа не может быть пустым.", nameof(name));
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Цена должна быть больше нуля.");
            if (!Enum.IsDefined(typeof(RentalPeriod), period))
                throw new ArgumentException("Некорректный период тарифа.", nameof(period));

            return new Tariff(id, name.Trim(), period, price);
        }

        public override string ToString() => $"{Name} ({Price} руб.)";
    }
}