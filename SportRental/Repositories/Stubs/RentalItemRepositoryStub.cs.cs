using System.Collections.Generic;
using System.Linq;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Repositories.Stubs
{
    public class RentalItemRepositoryStub : IRentalItemRepository
    {
        private readonly List<RentalItem> _items = new List<RentalItem>();

        public IEnumerable<RentalItem> GetByRentalId(int rentalId) =>
            _items.Where(x => x.RentalId == rentalId).ToList();

        public void Add(RentalItem item) => _items.Add(item);
    }
}
