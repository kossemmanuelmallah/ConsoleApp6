namespace WarShipStorage;

public class FormShipField : Form
{
    private readonly CanvasForShip _canvas;
    private BaseMovementStrategy? _strategy;

    private readonly PictureBox _pictureBox = new() { Left = 0, Top = 0, Width = 900, Height = 520 };
    private readonly ComboBox _goal = new() { Left = 65, Top = 534, Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };

    public FormShipField(EntityWarShip entity)
    {
        Text = "Поле перемещения";
        ClientSize = new Size(900, 600);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        _canvas = new CanvasForShip(_pictureBox.Width, _pictureBox.Height);
        _canvas.SetShip(DrawingWarShip.Create(entity), 10, 10);   // левый верхний угол

        _goal.Items.AddRange(new object[] { "К центру", "К правому нижнему углу" });

        var lbl = new Label { Text = "Цель:", Left = 15, Top = 538, Width = 45, Height = 20 };
        var bStep = new Button { Text = "Шаг", Left = 310, Top = 530, Width = 90, Height = 32 };
        var bUp = new Button { Text = "↑", Left = 785, Top = 524, Width = 50, Height = 30 };
        var bLeft = new Button { Text = "←", Left = 735, Top = 556, Width = 50, Height = 30 };
        var bDown = new Button { Text = "↓", Left = 785, Top = 556, Width = 50, Height = 30 };
        var bRight = new Button { Text = "→", Left = 835, Top = 556, Width = 50, Height = 30 };

        bStep.Click += (_, _) => MakeStep();
        bUp.Click += (_, _) => Move(Direction.Up);
        bDown.Click += (_, _) => Move(Direction.Down);
        bLeft.Click += (_, _) => Move(Direction.Left);
        bRight.Click += (_, _) => Move(Direction.Right);

        Controls.AddRange(new Control[] { _pictureBox, lbl, _goal, bStep, bUp, bLeft, bDown, bRight });
        Draw();
    }

    private void Move(Direction d)
    {
        _canvas.Move(d);
        Draw();
    }

    private void MakeStep()
    {
        if (_strategy == null)
        {
            if (_goal.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите цель");
                return;
            }

            BaseMovementStrategy? strategy = _goal.SelectedIndex switch
            {
                0 => new MoveToCenter(),
                1 => new MoveToRightBottom(),
                _ => null
            };
            if (strategy == null) return;

            strategy.SetData(new MoveableAdapterShip(_canvas), _pictureBox.Width, _pictureBox.Height);
            _strategy = strategy;
            _goal.Enabled = false;          // цель выбрана: список заблокирован
        }

        _strategy.MakeStep();
        Draw();

        if (_strategy.Status == StrategyStatus.Finish)
        {
            _strategy = null;
            _goal.Enabled = true;           // цель достигнута: список разблокирован
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