namespace ProjectWarship;

/// <summary>Класс-сущность: хранит только характеристики корабля</summary>
public class EntityWarship
{
    public int Speed { get; init; }
    public double Weight { get; init; }
    public Color BodyColor { get; init; }

    /// <summary>Шаг перемещения (вычисляемое свойство)</summary>
    public double Step => Speed * 100.0 / Weight;

    public EntityWarship(int speed, double weight, Color bodyColor)
    {
        Speed = speed;
        Weight = weight;
        BodyColor = bodyColor;
    }
}
