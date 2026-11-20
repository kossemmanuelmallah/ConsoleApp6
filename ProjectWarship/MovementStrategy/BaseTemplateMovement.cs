namespace ProjectWarship;

public enum StrategyStatus { NotInit, InProgress, Finish }

public abstract class BaseTemplateMovement
{
    private IMoveableObject? _moveableObject;

    public StrategyStatus Status { get; private set; } = StrategyStatus.NotInit;
    protected int FieldWidth { get; private set; }
    protected int FieldHeight { get; private set; }

    public void SetData(IMoveableObject moveableObject, int width, int height)
    {
        _moveableObject = moveableObject;
        FieldWidth = width;
        FieldHeight = height;
        Status = StrategyStatus.InProgress;
    }

    public void MakeStep()
    {
        if (Status != StrategyStatus.InProgress) return;
        if (IsTargetDestination()) { Status = StrategyStatus.Finish; return; }
        MoveToTarget();
        if (IsTargetDestination()) Status = StrategyStatus.Finish;
    }

    protected bool MoveLeft() => MoveTo(DirectionType.Left);
    protected bool MoveRight() => MoveTo(DirectionType.Right);
    protected bool MoveUp() => MoveTo(DirectionType.Up);
    protected bool MoveDown() => MoveTo(DirectionType.Down);

    protected ObjectParameters? Parameters => _moveableObject?.GetObjectPosition;
    protected int Step => _moveableObject?.GetStep ?? 1;

    private bool MoveTo(DirectionType direction)
    {
        if (_moveableObject == null || !_moveableObject.CheckCanMove(direction)) return false;
        _moveableObject.MoveObject(direction);
        return true;
    }

    protected abstract void MoveToTarget();
    protected abstract bool IsTargetDestination();
}