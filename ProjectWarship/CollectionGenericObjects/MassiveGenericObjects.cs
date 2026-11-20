namespace ProjectWarship;

public class MassiveGenericObjects<T> : ICollectionGenericObjects<T> where T : class
{
    private T?[] _collection = Array.Empty<T?>();

    public int Count => _collection.Length;

    public int SetMaxCount
    {
        set
        {
            if (value <= 0) return;
            if (_collection.Length > 0) Array.Resize(ref _collection, value);
            else _collection = new T?[value];
        }
    }

    public int Insert(T obj)
    {
        for (int i = 0; i < _collection.Length; i++)
        {
            if (_collection[i] == null) { _collection[i] = obj; return i; }
        }
        return -1;
    }

    public int Insert(T obj, int position)
    {
        if (position < 0 || position >= _collection.Length) return -1;
        if (_collection[position] == null) { _collection[position] = obj; return position; }

        for (int i = position + 1; i < _collection.Length; i++)
            if (_collection[i] == null) { _collection[i] = obj; return i; }
        for (int i = position - 1; i >= 0; i--)
            if (_collection[i] == null) { _collection[i] = obj; return i; }
        return -1;
    }

    public T? Remove(int position)
    {
        if (position < 0 || position >= _collection.Length) return null;
        var obj = _collection[position];
        _collection[position] = null;
        return obj;
    }

    public T? Get(int position) =>
        position >= 0 && position < _collection.Length ? _collection[position] : null;
}
