using System.Collections.Generic;
using System.Linq;
using SportRental.Entities;
using SportRental.Enums;
using SportRental.Repositories.Interfaces;

namespace SportRental.Repositories.Stubs
{
    public class EquipmentRepositoryStub : IEquipmentRepository
    {
        private readonly List<Equipment> _items = new List<Equipment>
        {
            Equipment.Create(1, 1, 2, "SKI-001", EquipmentCondition.Good),
            Equipment.Create(2, 2, 3, "SNB-001", EquipmentCondition.NeedsRepair),
            Equipment.Create(3, 3, 4, "BIK-001", EquipmentCondition.Good)
        };

        public IEnumerable<Equipment> GetAll() => _items;

        public Equipment GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

        public void Add(Equipment entity) => _items.Add(entity);

        public void Update(Equipment entity)
        {
            var index = _items.FindIndex(x => x.Id == entity.Id);
            if (index >= 0) _items[index] = entity;
        }

        public void Delete(int id) => _items.RemoveAll(x => x.Id == id);
    }
}