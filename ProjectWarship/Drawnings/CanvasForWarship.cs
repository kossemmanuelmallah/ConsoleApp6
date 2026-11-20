namespace ProjectWarship;

/// <summary>Класс-поле: размеры области, ссылка на прорисовку, проверка границ</summary>
public class CanvasForWarship
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public DrawingWarship? Drawing { get; private set; }

    public CanvasForWarship(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void SetDrawing(DrawingWarship drawing) => Drawing = drawing;

    /// <summary>Установка позиции с коррекцией координат (корабль не выходит за поле)</summary>
    public void SetPosition(int x, int y)
    {
        if (Drawing == null) return;
        x = Math.Clamp(x, 0, Math.Max(0, Width - Drawing.Width));
        y = Math.Clamp(y, 0, Math.Max(0, Height - Drawing.Height));
        Drawing.SetPosition(x, y);
    }

    public bool CanMove(DirectionType direction)
    {
        if (Drawing == null || Drawing.GetPosX == null || Drawing.GetPosY == null) return false;
        int x = Drawing.GetPosX.Value, y = Drawing.GetPosY.Value;
        return direction switch
        {
            DirectionType.Left => x > 0,
            DirectionType.Right => x + Drawing.Width < Width,
            DirectionType.Up => y > 0,
            DirectionType.Down => y + Drawing.Height < Height,
            _ => false
        };
    }

    public void Move(DirectionType direction)
    {
        if (Drawing == null || Drawing.GetPosX == null || Drawing.GetPosY == null) return;
        switch (direction)
        {
            case DirectionType.Left: Drawing.MoveLeft(); break;
            case DirectionType.Right: Drawing.MoveRight(); break;
            case DirectionType.Up: Drawing.MoveUp(); break;
            case DirectionType.Down: Drawing.MoveDown(); break;
        }
        SetPosition(Drawing.GetPosX!.Value, Drawing.GetPosY!.Value);   // коррекция границ
    }

    public void Draw(Graphics g)
    {
        SeaScene.DrawFull(g, Width, Height);
        Drawing?.DrawTransport(g);
    }
}
