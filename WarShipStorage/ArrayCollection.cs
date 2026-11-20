using System;

namespace WarShipStorage
{
    public class ArrayCollection<T> : ICollection<T>
    {
        private readonly T[] _items = new T[10];
        private int _count = 0;

        public int Count => _count;

        public void Add(T item)
        {
            if (_count >= _items.Length)
                throw new InvalidOperationException("Эскадра переполнена");
            _items[_count++] = item;
        }

        public bool Remove(int index)
        {
            if (index < 0 || index >= _count) return false;
            for (int i = index; i < _count - 1; i++)
                _items[i] = _items[i + 1];
            _count--;
            return true;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Неверный индекс");
            return _items[index];
        }
    }
}