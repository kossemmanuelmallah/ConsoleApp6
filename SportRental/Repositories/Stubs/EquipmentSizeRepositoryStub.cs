using System.Collections.Generic;
using System.Linq;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Repositories.Stubs
{
    public class EquipmentSizeRepositoryStub : IEquipmentSizeRepository
    {
        private readonly List<EquipmentSize> _items = new List<EquipmentSize>
        {
            EquipmentSize.Create(1, "S"),
            EquipmentSize.Create(2, "M"),
            EquipmentSize.Create(3, "L"),
            EquipmentSize.Create(4, "XL")
        };

        public IEnumerable<EquipmentSize> GetAll() => _items;

        public EquipmentSize GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

        public void Add(EquipmentSize entity) => _items.Add(entity);

        public void Update(EquipmentSize entity)
        {
            var index = _items.FindIndex(x => x.Id == entity.Id);
            if (index >= 0) _items[index] = entity;
        }

        public void Delete(int id) => _items.RemoveAll(x => x.Id == id);
    }
}