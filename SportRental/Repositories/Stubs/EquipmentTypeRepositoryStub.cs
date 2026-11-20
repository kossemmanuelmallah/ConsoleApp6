using System.Collections.Generic;
using System.Linq;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Repositories.Stubs
{
    public class EquipmentTypeRepositoryStub : IEquipmentTypeRepository
    {
        private readonly List<EquipmentType> _items = new List<EquipmentType>
        {
            EquipmentType.Create(1, "Лыжи"),
            EquipmentType.Create(2, "Сноуборд"),
            EquipmentType.Create(3, "Велосипед")
        };

        public IEnumerable<EquipmentType> GetAll() => _items;

        public EquipmentType GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

        public void Add(EquipmentType entity) => _items.Add(entity);

        public void Update(EquipmentType entity)
        {
            var index = _items.FindIndex(x => x.Id == entity.Id);
            if (index >= 0) _items[index] = entity;
        }

        public void Delete(int id) => _items.RemoveAll(x => x.Id == id);
    }
}