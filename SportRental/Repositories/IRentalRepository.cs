using System.Collections.Generic;
using SportRental.Entities;

namespace SportRental.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetAll();
        Rental GetById(int id);
        void Add(Rental rental);
    }
}
