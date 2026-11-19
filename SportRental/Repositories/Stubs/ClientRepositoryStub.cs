using System.Collections.Generic;
using System.Linq;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Repositories.Stubs
{
    public class ClientRepositoryStub : IClientRepository
    {
        private readonly List<Client> _items = new List<Client>
        {
            Client.Create(1, "Иванов Иван Иванович", "+7-900-000-00-01"),
            Client.Create(2, "Петрова Анна Сергеевна", "+7-900-000-00-02")
        };

        public IEnumerable<Client> GetAll() => _items;

        public Client GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

        public void Add(Client entity) => _items.Add(entity);

        public void Update(Client entity)
        {
            var index = _items.FindIndex(x => x.Id == entity.Id);
            if (index >= 0) _items[index] = entity;
        }

        public void Delete(int id) => _items.RemoveAll(x => x.Id == id);
    }
}