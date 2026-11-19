using System.Collections.Generic;
using System.Linq;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Repositories.Stubs
{
    public class RentalRepositoryStub : IRentalRepository
    {
        private readonly List<Rental> _items = new List<Rental>();

        public IEnumerable<Rental> GetAll() => _items;

        public Rental GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

        public void Add(Rental rental) => _items.Add(rental);
    }
}
