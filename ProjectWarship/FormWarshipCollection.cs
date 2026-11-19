namespace ProjectWarship;

/// <summary>Главная форма: работа с коллекцией кораблей</summary>
public class FormWarshipCollection : Form
{
    private readonly AbstractCompany _company;
    private readonly PictureBox _pictureBox = new() { Left = 0, Top = 0, Width = 960, Height = 600 };
    private readonly NumericUpDown _numPosition = new() { Left = 980, Top = 160, Width = 210, Minimum = 0, Maximum = 999 };

    public FormWarshipCollection()
    {
        Text = "Хранилище объектов — Военный корабль";
        ClientSize = new Size(1200, 600);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        _company = new NavalBase(_pictureBox.Width, _pictureBox.Height, new MassiveGenericObjects<DrawingWarship>());

        var bAdd = Btn("Добавить корабль", 20);
        var bAddAdv = Btn("Добавить боевой корабль", 64);
        var lbl = new Label { Text = "Позиция для удаления", Left = 980, Top = 135, Width = 210, Height = 20 };
        var bRemove = Btn("Удалить", 196);
        var bTransfer = Btn("Передать на тесты", 270);
        var bRefresh = Btn("Обновить", 314);

        bAdd.Click += (_, _) => AddShip(ShipFactory.CreateSimple());
        bAddAdv.Click += (_, _) => AddShip(ShipFactory.CreateAdvanced());
        bRemove.Click += (_, _) => RemoveShip();
        bTransfer.Click += (_, _) => TransferToTests();
        bRefresh.Click += (_, _) => Redraw();

        Controls.AddRange(new Control[] { _pictureBox, bAdd, bAddAdv, lbl, _numPosition, bRemove, bTransfer, bRefresh });
        Redraw();
    }

    private static Button Btn(string text, int y) =>
        new() { Text = text, Left = 980, Top = y, Width = 210, Height = 36 };

    private void AddShip(DrawingWarship ship)
    {
        if (_company + ship)
        {
            Redraw();                                   // on dessine d'abord
            MessageBox.Show("Объект добавлен");         // puis on affiche le message
        }
        else
        {
            MessageBox.Show("Не удалось добавить объект");
        }
    }

    private void RemoveShip()
    {
        if (MessageBox.Show("Удалить объект?", "Удаление",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        int pos = (int)_numPosition.Value;
        if ((_company - pos) != null)
        {
            MessageBox.Show("Объект удален");
            Redraw();
        }
        else
        {
            MessageBox.Show("Не удалось удалить объект");
        }
    }

    private void TransferToTests()
    {
        var ship = _company.GetRandomObject();
        if (ship == null)
        {
            MessageBox.Show("Не удалось получить объект");
            return;
        }
        using var form = new FormWarship(ship);
        form.ShowDialog();
        Redraw();   // объект остаётся в коллекции
    }

    private void Redraw()
    {
        try
        {
            var bmp = _company.Show();
            var old = _pictureBox.Image;
            _pictureBox.Image = bmp;
            old?.Dispose();
            _pictureBox.Refresh();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Ошибка отрисовки");
        }
    }
}