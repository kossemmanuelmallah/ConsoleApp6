using System.Drawing.Drawing2D;

namespace ProjectWarship;

/// <summary>Военно-морская база: корабли у причальных стенок</summary>
public class NavalBase : AbstractCompany
{
    private const int QuayTop = 104;   // смещение причала внутри ячейки
    private const int QuayHeight = 16;

    public NavalBase(int pictureWidth, int pictureHeight, ICollectionGenericObjects<DrawingWarship> collection)
        : base(pictureWidth, pictureHeight, collection) { }

    protected override void DrawBackground(Graphics g)
    {
        SeaScene.DrawSea(g, new Rectangle(0, 0, _pictureWidth, _pictureHeight), 11);

        int cols = _pictureWidth / _placeSizeWidth;
        int rows = _pictureHeight / _placeSizeHeight;
        using var font = new Font("Arial", 8f, FontStyle.Bold);

        for (int r = 0; r < rows; r++)
        {
            int y = r * _placeSizeHeight + QuayTop;
            var rect = new Rectangle(0, y, cols * _placeSizeWidth, QuayHeight);

            using (var shadow = new SolidBrush(Color.FromArgb(70, 0, 10, 25)))
                g.FillRectangle(shadow, rect.X, rect.Bottom - 2, rect.Width, 4);
            using (var concrete = new LinearGradientBrush(rect, Color.FromArgb(178, 180, 184), Color.FromArgb(92, 95, 100), 90f))
                g.FillRectangle(concrete, rect);
            using (var edge = new Pen(Color.FromArgb(200, 235, 235, 235), 1f))
                g.DrawLine(edge, rect.X, rect.Y, rect.Right, rect.Y);

            for (int c = 0; c < cols; c++)
            {
                int idx = r * cols + c;
                // швартовная тумба
                using (var bollard = new SolidBrush(Color.FromArgb(40, 42, 46)))
                    g.FillEllipse(bollard, c * _placeSizeWidth + 150, y + 3, 9, 6);
                // номер места
                g.DrawString(idx.ToString(), font, Brushes.WhiteSmoke, c * _placeSizeWidth + 6, y + 1);
            }
        }
    }

    protected override void SetObjectsPosition()
    {
        int cols = _pictureWidth / _placeSizeWidth;
        if (cols == 0) return;
        for (int i = 0; i < _collection.Count; i++)
        {
            int col = i % cols, row = i / cols;
            _collection.Get(i)?.SetPosition(col * _placeSizeWidth + 10, row * _placeSizeHeight + 4);
        }
    }
}