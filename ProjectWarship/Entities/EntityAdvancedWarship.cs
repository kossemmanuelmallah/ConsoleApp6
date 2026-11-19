namespace ProjectWarship;

/// <summary>Класс-сущность продвинутого объекта (боевой корабль)</summary>
public class EntityAdvancedWarship : EntityWarship
{
    public Color AdditionalColor { get; init; }
    public bool Guns { get; init; }      // артиллерийские башни
    public bool Helipad { get; init; }   // вертолётная площадка + вертолёт
    public bool Radar { get; init; }     // радар на мачте

    public EntityAdvancedWarship(int speed, double weight, Color bodyColor,
        Color additionalColor, bool guns, bool helipad, bool radar)
        : base(speed, weight, bodyColor)
    {
        AdditionalColor = additionalColor;
        Guns = guns;
        Helipad = helipad;
        Radar = radar;
    }
}