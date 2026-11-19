namespace WarShipStorage;

public enum StrategyStatus { NotInit, InProgress, Finish }

/// <summary>Абстрактная стратегия: вектор от текущей позиции к цели</summary>
public abstract class BaseMovementStrategy
{
    private IMoveable? _object;

    public StrategyStatus Status { get; private set; } = StrategyStatus.NotInit;
    protected int FieldWidth { get; private set; }
    protected int FieldHeight { get; private set; }

    public void SetData(IMoveable obj, int fieldWidth, int fieldHeight)
    {
        _object = obj;
        FieldWidth = fieldWidth;
        FieldHeight = fieldHeight;
        Status = StrategyStatus.InProgress;
    }

    /// <summary>Один шаг к цели (по диагонали, затем по прямой)</summary>
    public void MakeStep()
    {
        if (Status != StrategyStatus.InProgress || _object == null) return;

        GetTarget(_object, out int tx, out int ty);
        int dx = tx - _object.GetX();
        int dy = ty - _object.GetY();
        int s = _object.GetStep();

        if (Math.Abs(dx) <= s && Math.Abs(dy) <= s)
        {
            _object.SetX(tx);
            _object.SetY(ty);
            Status = StrategyStatus.Finish;
            return;
        }

        _object.SetX(_object.GetX() + Math.Clamp(dx, -s, s));
        _object.SetY(_object.GetY() + Math.Clamp(dy, -s, s));
    }

    /// <summary>Целевая позиция левого верхнего угла корабля</summary>
    protected abstract void GetTarget(IMoveable obj, out int x, out int y);
}

public class MoveToCenter : BaseMovementStrategy
{
    protected override void GetTarget(IMoveable obj, out int x, out int y)
    {
        x = (FieldWidth - obj.GetWidth()) / 2;
        y = (FieldHeight - obj.GetHeight()) / 2;
    }
}

public class MoveToRightBottom : BaseMovementStrategy
{
    protected override void GetTarget(IMoveable obj, out int x, out int y)
    {
        x = FieldWidth - obj.GetWidth();
        y = FieldHeight - obj.GetHeight();
    }
}