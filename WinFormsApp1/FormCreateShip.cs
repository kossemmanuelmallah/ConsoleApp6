namespace WarShipStorage;

/// <summary>Диалог создания корабля</summary>
public class FormCreateShip : Form
{
    private readonly ComboBox _type = new() { Left = 190, Top = 14, Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly Button _btnMain = MakeColorButton(55, Color.SteelBlue);
    private readonly Button _btnDeck = MakeColorButton(95, Color.DimGray);
    private readonly NumericUpDown _speed = new() { Left = 190, Top = 135, Width = 170, Minimum = 5, Maximum = 60, Value = 30 };
    private readonly NumericUpDown _displacement = new() { Left = 190, Top = 175, Width = 170, Minimum = 500, Maximum = 100000, Increment = 500, Value = 8000 };
    private readonly NumericUpDown _aircraft = new() { Left = 190, Top = 215, Width = 170, Minimum = 0, Maximum = 9, Value = 6 };
    private readonly CheckBox _catapult = new() { Left = 190, Top = 255, Width = 170, Text = "Есть", Checked = true };

    public EntityWarShip? Result { get; private set; }

    public FormCreateShip()
    {
        Text = "Создание корабля";
        ClientSize = new Size(380, 345);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        _type.Items.AddRange(new object[] { "Обычный военный корабль", "Авианосец" });
        _type.SelectedIndex = 0;

        var ok = new Button { Text = "ОК", Left = 190, Top = 295, Width = 80, Height = 30, DialogResult = DialogResult.OK };
        var cancel = new Button { Text = "Отмена", Left = 280, Top = 295, Width = 80, Height = 30, DialogResult = DialogResult.Cancel };
        AcceptButton = ok;
        CancelButton = cancel;

        _btnMain.Click += (_, _) => PickColor(_btnMain);
        _btnDeck.Click += (_, _) => PickColor(_btnDeck);
        _type.SelectedIndexChanged += (_, _) => UpdateEnabled();
        ok.Click += (_, _) => BuildResult();

        Controls.AddRange(new Control[]
        {
            Lbl("Тип корабля", 18), _type,
            Lbl("Основной цвет корпуса", 58), _btnMain,
            Lbl("Цвет палубы (авианосец)", 98), _btnDeck,
            Lbl("Скорость (узлы)", 138), _speed,
            Lbl("Водоизмещение (тонны)", 178), _displacement,
            Lbl("Самолётов (0–9)", 218), _aircraft,
            Lbl("Катапульта", 258), _catapult,
            ok, cancel
        });

        UpdateEnabled();
    }

    private static Label Lbl(string text, int top) =>
        new() { Text = text, Left = 15, Top = top, Width = 175, Height = 20 };

    private static Button MakeColorButton(int top, Color color) =>
        new()
        {
            Left = 190,
            Top = top,
            Width = 170,
            Height = 26,
            Text = "Выбрать цвет…",
            BackColor = color,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };

    private void PickColor(Button button)
    {
        using var dlg = new ColorDialog { Color = button.BackColor };
        if (dlg.ShowDialog(this) == DialogResult.OK)
            button.BackColor = dlg.Color;
    }

    private void UpdateEnabled()
    {
        bool carrier = _type.SelectedIndex == 1;
        _btnDeck.Enabled = carrier;
        _aircraft.Enabled = carrier;
        _catapult.Enabled = carrier;
    }

    private void BuildResult()
    {
        if (_type.SelectedIndex == 1)
        {
            Result = new EntityAircraftCarrier
            {
                Speed = (int)_speed.Value,
                Displacement = (double)_displacement.Value,
                MainColor = _btnMain.BackColor,
                DeckColor = _btnDeck.BackColor,
                AircraftCount = (int)_aircraft.Value,
                HasCatapult = _catapult.Checked
            };
        }
        else
        {
            Result = new EntityWarShip
            {
                Speed = (int)_speed.Value,
                Displacement = (double)_displacement.Value,
                MainColor = _btnMain.BackColor
            };
        }
    }
}