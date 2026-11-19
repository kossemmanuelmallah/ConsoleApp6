namespace ProjectWarship;

public class ObjectParameters
{
    private readonly int _x, _y, _width, _height;

    public int LeftBorder => _x;
    public int TopBorder => _y;
    public int RightBorder => _x + _width;
    public int DownBorder => _y + _height;
    public int ObjectMiddleHorizontal => _x + _width / 2;
    public int ObjectMiddleVertical => _y + _height / 2;

    public ObjectParameters(int x, int y, int width, int height)
    {
        _x = x; _y = y; _width = width; _height = height;
    }
}