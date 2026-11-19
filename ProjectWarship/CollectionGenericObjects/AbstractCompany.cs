namespace ProjectWarship;

/// <summary>Абстрактная компания: коллекция + отрисовка + перегрузка операторов</summary>
public abstract class AbstractCompany
{
    protected readonly int _pictureWidth;
    protected readonly int _pictureHeight;
    protected readonly int _placeSizeWidth = DrawingWarship.DrawingWidth + 20;    // 240
    protected readonly int _placeSizeHeight = 120;
    protected readonly ICollectionGenericObjects<DrawingWarship> _collection;

    private int GetMaxCount => (_pictureWidth / _placeSizeWidth) * (_pictureHeight / _placeSizeHeight);

    public AbstractCompany(int pictureWidth, int pictureHeight, ICollectionGenericObjects<DrawingWarship> collection)
    {
        _pictureWidth = pictureWidth;
        _pictureHeight = pictureHeight;
        _collection = collection;
        _collection.SetMaxCount = GetMaxCount;
    }

    /// <summary>Добавление корабля в коллекцию компании</summary>
    public static bool operator +(AbstractCompany company, DrawingWarship ship) =>
        company._collection.Insert(ship) >= 0;

    /// <summary>Удаление корабля с позиции</summary>
    public static DrawingWarship? operator -(AbstractCompany company, int position) =>
        company._collection.Remove(position);

    public DrawingWarship? GetRandomObject()
    {
        var items = new List<DrawingWarship>();
        for (int i = 0; i < _collection.Count; i++)
        {
            var o = _collection.Get(i);
            if (o != null) items.Add(o);
        }
        return items.Count == 0 ? null : items[Random.Shared.Next(items.Count)];
    }

    public Bitmap Show()
    {
        var bitmap = new Bitmap(_pictureWidth, _pictureHeight);
        using var g = Graphics.FromImage(bitmap);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        DrawBackground(g);
        SetObjectsPosition();
        for (int i = 0; i < _collection.Count; i++)
            _collection.Get(i)?.DrawTransport(g);
        return bitmap;
    }

    protected abstract void DrawBackground(Graphics g);
    protected abstract void SetObjectsPosition();
}