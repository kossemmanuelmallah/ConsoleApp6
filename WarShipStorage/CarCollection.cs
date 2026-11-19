using System.Collections.Generic;

namespace WarShipStorage
{
    public class CarCollection
    {
        private List<Car> cars;

        public CarCollection()
        {
            cars = new List<Car>();
        }

        public void Add(Car car)
        {
            cars.Add(car);
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < cars.Count)
                cars.RemoveAt(index);
        }

        public void UpdateAt(int index, Car car)
        {
            if (index >= 0 && index < cars.Count)
                cars[index] = car;
        }

        public void Clear()
        {
            cars.Clear();
        }

        public Car GetAt(int index)
        {
            if (index >= 0 && index < cars.Count)
                return cars[index];
            return null;
        }

        public int Count()
        {
            return cars.Count;
        }
    }
}
