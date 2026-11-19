namespace ProjectWarship;

/// <summary>Основная форма: тест перемещения корабля</summary>
public class FormWarship : Form
{
    private readonly CanvasForWarship _canvas;
    private BaseTemplateMovement? _strategy;
    private readonly Random _rnd = new();

    private readonly PictureBox _pictureBox = new() { Left = 0, Top = 0, Width = 900, Height = 520 };
    private readonly ComboBox _comboGoal = new()
    {
        Left = 440,
        Top = 556,
        Width = 150,
        DropDownStyle = ComboBoxStyle.DropDownList
    };

    public FormWarship(DrawingWarship? ship = null)
    {
        Text = "Военный корабль — тест перемещения";
        ClientSize = new Size(900, 620);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        _canvas = new CanvasForWarship(_pictureBox.Width, _pictureBox.Height);
        _comboGoal.Items.AddRange(new object[] { "К центру", "К краю" });

        var bCreate = Btn("Создать", 10, 530, 170);
        var bCreateAdv = Btn("Создать боевой корабль", 190, 530, 230);
        var bCheck = Btn("Проверка границ", 10, 570, 170);
        var lbl = new Label { Text = "Выбор цели", Left = 440, Top = 532, Width = 150, Height = 20 };
        var bStep = Btn("Шаг", 600, 553, 90);

        var bUp = Btn("↑", 795, 525, 45);
        var bLeft = Btn("←", 745, 558, 45);
        var bDown = Btn("↓", 795, 558, 45);
        var bRight = Btn("→", 845, 558, 45);

        bCreate.Click += (_, _) => SetNewShip(ShipFactory.CreateSimple());
        bCreateAdv.Click += (_, _) => SetNewShip(ShipFactory.CreateAdvanced());
        bCheck.Click += (_, _) =>
        {
            // заведомо некорректные координаты: поле их скорректирует
            _canvas.SetPosition(_rnd.Next(-300, _pictureBox.Width + 300), _rnd.Next(-300, _pictureBox.Height + 300));
            Draw();
        };
        bStep.Click += (_, _) => MakeStep();
        bUp.Click += (_, _) => Move(DirectionType.Up);
        bDown.Click += (_, _) => Move(DirectionType.Down);
        bLeft.Click += (_, _) => Move(DirectionType.Left);
        bRight.Click += (_, _) => Move(DirectionType.Right);

        Controls.AddRange(new Control[]
        {
            _pictureBox, bCreate, bCreateAdv, bCheck, lbl, _comboGoal, bStep, bUp, bLeft, bDown, bRight
        });

        if (ship != null) SetNewShip(ship); else Draw();
    }

    private static Button Btn(string text, int x, int y, int w) =>
        new() { Text = text, Left = x, Top = y, Width = w, Height = 32 };

    private void SetNewShip(DrawingWarship ship)
    {
        _canvas.SetDrawing(ship);
        _canvas.SetPosition(20, 20);
        _strategy = null;
        _comboGoal.Enabled = true;
        Draw();
    }

    private void Move(DirectionType d)
    {
        _canvas.Move(d);
        Draw();
    }

    private void MakeStep()
    {
        if (_canvas.Drawing == null) return;

        if (_strategy == null)
        {
            if (_comboGoal.SelectedIndex < 0) return;
            _strategy = _comboGoal.SelectedIndex switch
            {
                0 => new MoveToCenter(),
                1 => new MoveToRightDownBorder(),
                _ => null
            };
            if (_strategy == null) return;
            _strategy.SetData(new MoveableAdapterWarship(_canvas), _pictureBox.Width, _pictureBox.Height);
            _comboGoal.Enabled = false;     // цель выбрана: список блокируется
        }

        _strategy.MakeStep();
        Draw();

        if (_strategy.Status == StrategyStatus.Finish)
        {
            _strategy = null;
            _comboGoal.Enabled = true;      // цель достигнута
        }
    }

    private void Draw()
    {
        var bmp = new Bitmap(_pictureBox.Width, _pictureBox.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            _canvas.Draw(g);
        }
        var old = _pictureBox.Image;
        _pictureBox.Image = bmp;
        old?.Dispose();
    }
}