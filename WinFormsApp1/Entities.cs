namespace WarShipStorage;

/// <summary>Класс-сущность: характеристики корабля</summary>
public class EntityWarShip
{
    public int Speed { get; init; }                 // скорость (узлы)
    public double Displacement { get; init; }       // водоизмещение (тонны)
    public Color MainColor { get; init; }           // основной цвет корпуса
    public double Step => Speed * 10.0 / Displacement;
}

/// <summary>Продвинутая сущность: авианосец</summary>
public class EntityAircraftCarrier : EntityWarShip
{
    public Color DeckColor { get; init; }
    public int AircraftCount { get; init; }
    public bool HasCatapult { get; init; }
}
