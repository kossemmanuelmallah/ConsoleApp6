namespace ProjectWarship;

/// <summary>Цель: центр формы (сначала по горизонтали, затем по вертикали)</summary>
public class MoveToCenter : BaseTemplateMovement
{
    protected override bool IsTargetDestination()
    {
        var p = Parameters;
        if (p == null) return true;
        int s = Step;
        return Math.Abs(p.ObjectMiddleHorizontal - FieldWidth / 2) <= s &&
               Math.Abs(p.ObjectMiddleVertical - FieldHeight / 2) <= s;
    }

    protected override void MoveToTarget()
    {
        var p = Parameters;
        if (p == null) return;
        int s = Step;
        int dx = p.ObjectMiddleHorizontal - FieldWidth / 2;
        int dy = p.ObjectMiddleVertical - FieldHeight / 2;

        if (Math.Abs(dx) > s) { if (dx > 0) MoveLeft(); else MoveRight(); }
        else if (Math.Abs(dy) > s) { if (dy > 0) MoveUp(); else MoveDown(); }
    }
}
