namespace ProjectWarship;

/// <summary>Создание кораблей (выбор цвета + случайные характеристики)</summary>
public static class ShipFactory
{
    private static readonly Random _rnd = new();

    /// <summary>ColorDialog; при отмене - случайный цвет</summary>
    public static Color PickColor()
    {
        using var dlg = new ColorDialog();
        return dlg.ShowDialog() == DialogResult.OK
            ? dlg.Color
            : Color.FromArgb(_rnd.Next(40, 220), _rnd.Next(40, 220), _rnd.Next(40, 220));
    }

    public static DrawingWarship CreateSimple() =>
        new(_rnd.Next(120, 300), _rnd.Next(1000, 3000), PickColor());

    public static DrawingWarship CreateAdvanced()
    {
        var body = PickColor();
        var additional = PickColor();
        return new DrawingAdvancedWarship(_rnd.Next(120, 300), _rnd.Next(1000, 3000),
            body, additional,
            guns: _rnd.Next(4) != 0, helipad: _rnd.Next(2) == 0, radar: _rnd.Next(4) != 0);
    }
}