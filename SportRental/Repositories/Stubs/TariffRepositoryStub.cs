using System.Collections.Generic;
using System.Linq;
using SportRental.Entities;
using SportRental.Enums;
using SportRental.Repositories.Interfaces;

namespace SportRental.Repositories.Stubs
{
    public class TariffRepositoryStub : ITariffRepository
    {
        private readonly List<Tariff> _items = new List<Tariff>
        {
            Tariff.Create(1, "Час — стандарт", RentalPeriod.Hour, 300m),
            Tariff.Create(2, "Сутки — стандарт", RentalPeriod.Day, 1500m),
            Tariff.Create(3, "Сутки — выходной", RentalPeriod.Day, 2000m)
        };

        public IEnumerable<Tariff> GetAll() => _items;

        public Tariff GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

        public void Add(Tariff entity) => _items.Add(entity);

        public void Update(Tariff entity)
        {
            var index = _items.FindIndex(x => x.Id == entity.Id);
            if (index >= 0) _items[index] = entity;
        }

        public void Delete(int id) => _items.RemoveAll(x => x.Id == id);
    }
}