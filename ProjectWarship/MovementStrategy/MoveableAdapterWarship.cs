namespace ProjectWarship;

/// <summary>Адаптер: CanvasForWarship -> IMoveableObject</summary>
public class MoveableAdapterWarship : IMoveableObject
{
    private readonly CanvasForWarship _canvas;

    public MoveableAdapterWarship(CanvasForWarship canvas) => _canvas = canvas;

    public ObjectParameters? GetObjectPosition
    {
        get
        {
            var d = _canvas.Drawing;
            if (d == null || d.GetPosX == null || d.GetPosY == null) return null;
            return new ObjectParameters(d.GetPosX.Value, d.GetPosY.Value, d.Width, d.Height);
        }
    }

    public int GetStep => _canvas.Drawing?.GetStep ?? 1;

    public bool CheckCanMove(DirectionType direction) => _canvas.CanMove(direction);

    public void MoveObject(DirectionType direction) => _canvas.Move(direction);
}
