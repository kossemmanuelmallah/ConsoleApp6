namespace WarShipStorage
{
    public interface ICarRepository
    {
        void Add(Car car);
        void RemoveAt(int index);
        void UpdateAt(int index, Car car);
        void Clear();
        Car GetAt(int index);
        int Count();
    }
}
