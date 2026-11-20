namespace WarShipStorage;

public enum Direction { Left, Right, Up, Down }

/// <summary>Интерфейс перемещаемого объекта</summary>
public interface IMoveable
{
    int GetX();
    int GetY();
    void SetX(int x);
    void SetY(int y);
    int GetWidth();
    int GetHeight();
    int GetStep();
}

/// <summary>Класс-поле: границы области и корректировка координат</summary>
public class CanvasForShip
{
    public int Width { get; }
    public int Height { get; }
    public DrawingWarShip? Ship { get; private set; }

    public CanvasForShip(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void SetShip(DrawingWarShip ship, int x, int y)
    {
        Ship = ship;
        SetPosition(x, y);
    }

    /// <summary>Корректировка координат: корабль не выходит за пределы поля</summary>
    public void ClampPosition(ref int x, ref int y)
    {
        if (Ship == null) return;
        x = Math.Clamp(x, 0, Math.Max(0, Width - Ship.Width));
        y = Math.Clamp(y, 0, Math.Max(0, Height - Ship.Height));
    }

    public void SetPosition(int x, int y)
    {
        if (Ship == null) return;
        ClampPosition(ref x, ref y);
        Ship.SetPosition(x, y);
    }

    public void Move(Direction direction)
    {
        if (Ship == null) return;
        switch (direction)
        {
            case Direction.Left: Ship.MoveLeft(); break;
            case Direction.Right: Ship.MoveRight(); break;
            case Direction.Up: Ship.MoveUp(); break;
            case Direction.Down: Ship.MoveDown(); break;
        }
        SetPosition(Ship.X, Ship.Y);
    }

    public void Draw(Graphics g)
    {
        SeaScene.DrawFull(g, Width, Height);
        Ship?.Draw(g);
    }
}

/// <summary>Адаптер: позволяет стратегиям работать с кораблём на поле</summary>
public class MoveableAdapterShip : IMoveable
{
    private readonly CanvasForShip _canvas;

    public MoveableAdapterShip(CanvasForShip canvas)
    {
        _canvas = canvas;
    }

    public int GetX() => _canvas.Ship?.X ?? 0;
    public int GetY() => _canvas.Ship?.Y ?? 0;
    public void SetX(int x) => _canvas.SetPosition(x, GetY());
    public void SetY(int y) => _canvas.SetPosition(GetX(), y);
    public int GetWidth() => _canvas.Ship?.Width ?? 0;
    public int GetHeight() => _canvas.Ship?.Height ?? 0;
    public int GetStep() => _canvas.Ship?.StepInPixels ?? 1;
}