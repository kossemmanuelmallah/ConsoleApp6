namespace WarShipStorage
{
    public interface ICollection<T>
    {
        void Add(T item);
        bool Remove(int index);
        T Get(int index);
        int Count { get; }
    }
}