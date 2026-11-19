namespace ProjectWarship;

public interface ICollectionGenericObjects<T> where T : class
{
    /// <summary>Количество мест в коллекции</summary>
    int Count { get; }

    /// <summary>Установка максимального количества мест</summary>
    int SetMaxCount { set; }

    /// <summary>Добавление на первое свободное место. -1, если нет места</summary>
    int Insert(T obj);

    /// <summary>Добавление на позицию (или на ближайшую свободную). -1, если не вышло</summary>
    int Insert(T obj, int position);

    /// <summary>Удаление с позиции. null, если позиция неверна или пуста</summary>
    T? Remove(int position);

    T? Get(int position);
}