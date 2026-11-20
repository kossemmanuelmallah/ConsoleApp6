namespace WarShipStorage;

public class FormSquadron : Form
{
    private readonly Squadron _squadron = new(10);
    private readonly PictureBox _pictureBox = new() { Left = 0, Top = 0, Width = 500, Height = 550 };
    private readonly ListBox _list = new() { Left = 515, Top = 10, Width = 255, Height = 215, IntegralHeight = false };
    private readonly NumericUpDown _numIndex = new() { Left = 515, Top = 345, Width = 120, Minimum = 0, Maximum = 99 };
    private readonly Label _power = new() { Left = 515, Top = 400, Width = 255, Height = 40 };

    public FormSquadron()
    {
        Text = "Управление эскадрой";
        ClientSize = new Size(780, 550);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        var bCreate = new Button { Text = "Создать корабль", Left = 515, Top = 235, Width = 255, Height = 34 };
        var bTransfer = new Button { Text = "Передать на поле", Left = 515, Top = 275, Width = 255, Height = 34 };
        var lbl = new Label { Text = "Индекс для удаления", Left = 515, Top = 322, Width = 255, Height = 20 };
        var bDelete = new Button { Text = "Удалить", Left = 645, Top = 342, Width = 125, Height = 28 };

        bCreate.Click += (_, _) => CreateShip();
        bTransfer.Click += (_, _) => TransferToField();
        bDelete.Click += (_, _) => DeleteShip();

        Controls.AddRange(new Control[] { _pictureBox, _list, bCreate, bTransfer, lbl, _numIndex, bDelete, _power });
        Redraw();
    }

    private void CreateShip()
    {
        using var dlg = new FormCreateShip();
        if (dlg.ShowDialog(this) != DialogResult.OK) return;

        var ship = dlg.Result;
        if (ship == null) return;

        if (_squadron + ship) Redraw();
        else MessageBox.Show("Эскадра переполнена");
    }

    private void DeleteShip()
    {
        int index = (int)_numIndex.Value;
        if (_squadron - index) Redraw();
        else MessageBox.Show("Неверный индекс");
    }

    private void TransferToField()
    {
        if (_squadron.Count == 0)
        {
            MessageBox.Show("Нет кораблей для передачи");
            return;
        }

        var ship = _squadron.Get(_list.SelectedIndex);
        if (ship == null)
        {
            MessageBox.Show("Неверный индекс");
            return;
        }

        using var form = new FormShipField(ship);
        form.ShowDialog(this);
    }

    private void Redraw()
    {
        var old = _pictureBox.Image;
        _pictureBox.Image = _squadron.Show(_pictureBox.Width, _pictureBox.Height);
        old?.Dispose();

        int sel = _list.SelectedIndex;
        _list.Items.Clear();
        for (int i = 0; i < _squadron.Capacity; i++)
        {
            var s = _squadron.Get(i);
            _list.Items.Add(s == null ? $"{i}.  — свободно —" : $"{i}.  {Describe(s)}");
        }
        if (sel >= 0 && sel < _list.Items.Count) _list.SelectedIndex = sel;

        _power.Text = $"Кораблей: {_squadron.Count} из {_squadron.Capacity}\nСуммарная скорость: {_squadron.GetCombatPower()} уз";
    }

    private static string Describe(EntityWarShip s) =>
        s is EntityAircraftCarrier
            ? $"Авианосец, {s.Speed} уз, {s.Displacement:F0} т"
            : $"Военный корабль, {s.Speed} уз, {s.Displacement:F0} т";
}