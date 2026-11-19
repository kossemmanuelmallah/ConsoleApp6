namespace ProjectWarship;

/// <summary>Цель: правый нижний угол формы</summary>
public class MoveToRightDownBorder : BaseTemplateMovement
{
    protected override bool IsTargetDestination()
    {
        var p = Parameters;
        return p == null || (p.RightBorder >= FieldWidth && p.DownBorder >= FieldHeight);
    }

    protected override void MoveToTarget()
    {
        var p = Parameters;
        if (p == null) return;
        if (p.RightBorder < FieldWidth) MoveRight();
        else if (p.DownBorder < FieldHeight) MoveDown();
    }
}
