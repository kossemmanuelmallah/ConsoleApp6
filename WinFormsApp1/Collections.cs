using System.Drawing.Drawing2D;

namespace WarShipStorage;

/// <summary>Параметризованный интерфейс коллекции</summary>
public interface ICollection<T> where T : class
{
    int Capacity { get; }      // число мест
    int Count { get; }         // число занятых мест
    bool Add(T item);
    bool Remove(int index);
    T? Get(int index);
}

/// <summary>Коллекция на массиве фиксированного размера</summary>
public class ArrayCollection<T> : ICollection<T> where T : class
{
    private readonly T?[] _items;

    public ArrayCollection(int capacity = 10)
    {
        _items = new T?[capacity];
    }

    public int Capacity => _items.Length;

    public int Count
    {
        get
        {
            int n = 0;
            foreach (var item in _items)
                if (item != null) n++;
            return n;
        }
    }

    public bool Add(T item)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = item;
                return true;
            }
        }
        return false;
    }

    public bool Remove(int index)
    {
        if (index < 0 || index >= _items.Length || _items[index] == null) return false;
        _items[index] = null;
        return true;
    }

    public T? Get(int index) =>
        index >= 0 && index < _items.Length ? _items[index] : null;
}

/// <summary>Абстрактная эскадра (аналог компании)</summary>
public abstract class AbstractSquadron
{
    protected readonly ICollection<EntityWarShip> _ships;

    protected AbstractSquadron(ICollection<EntityWarShip> ships)
    {
        _ships = ships;
    }

    public int Capacity => _ships.Capacity;
    public int Count => _ships.Count;
    public EntityWarShip? Get(int index) => _ships.Get(index);

    /// <summary>Добавление корабля</summary>
    public static bool operator +(AbstractSquadron squadron, EntityWarShip ship) =>
        squadron._ships.Add(ship);

    /// <summary>Удаление корабля по индексу</summary>
    public static bool operator -(AbstractSquadron squadron, int index) =>
        squadron._ships.Remove(index);

    public abstract Bitmap Show(int width, int height);
}

/// <summary>Конкретная эскадра: отрисовка сетки у причалов</summary>
public class Squadron : AbstractSquadron
{
    private const int CellWidth = 250;
    private const int CellHeight = 110;
    private const int QuayOffset = 100;
    private const int QuayHeight = 10;

    public Squadron(int capacity = 10) : base(new ArrayCollection<EntityWarShip>(capacity))
    {
    }

    /// <summary>Суммарная скорость всех кораблей</summary>
    public int GetCombatPower()
    {
        int sum = 0;
        for (int i = 0; i < Capacity; i++)
            sum += Get(i)?.Speed ?? 0;
        return sum;
    }

    public override Bitmap Show(int width, int height)
    {
        var bmp = new Bitmap(width, height);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.AntiAlias;

        SeaScene.DrawSea(g, new Rectangle(0, 0, width, height), 11);

        int cols = Math.Max(1, width / CellWidth);
        int rows = (Capacity + cols - 1) / cols;
        using var font = new Font("Arial", 6f, FontStyle.Bold);

        // причалы
        for (int r = 0; r < rows; r++)
        {
            var quay = new Rectangle(0, r * CellHeight + QuayOffset, cols * CellWidth, QuayHeight);
            using (var concrete = new LinearGradientBrush(quay,
                   Color.FromArgb(178, 180, 184), Color.FromArgb(92, 95, 100), 90f))
                g.FillRectangle(concrete, quay);
            using (var edge = new Pen(Color.FromArgb(200, 235, 235, 235), 1f))
                g.DrawLine(edge, quay.X, quay.Y, quay.Right, quay.Y);
        }

        // корабли и номера мест
        for (int i = 0; i < Capacity; i++)
        {
            int col = i % cols, row = i / cols;
            g.DrawString(i.ToString(), font, Brushes.WhiteSmoke,
                col * CellWidth + 4, row * CellHeight + QuayOffset);

            var entity = Get(i);
            if (entity == null) continue;

            var drawing = DrawingWarShip.Create(entity);
            drawing.SetPosition(col * CellWidth + 15, row * CellHeight + 4);
            drawing.Draw(g);
        }

        return bmp;
    }
}