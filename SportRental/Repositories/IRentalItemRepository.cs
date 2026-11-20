using System.Collections.Generic;
using SportRental.Entities;

namespace SportRental.Repositories.Interfaces
{
    public interface IRentalItemRepository
    {
        IEnumerable<RentalItem> GetByRentalId(int rentalId);
        void Add(RentalItem item);
    }
}