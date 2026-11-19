using System;
using System.Drawing;
using System.Windows.Forms;

namespace WarShipStorage
{
    public partial class Form1 : Form
    {
        private Squadron _squadron = new Squadron();
        private DrawingWarShip _currentDrawingShip = null;
        private BaseMovementStrategy _currentStrategy = new MoveToCenter();

        public Form1()
        {
            InitializeComponent();
            pnlField.Paint += PnlField_Paint;
        }

        

        private void btnCreateShip_Click(object sender, EventArgs e)
        {
            using var dlg = new ShipCreationForm();
            dlg.Text = "Создание корабля";
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.CreatedShip != null)
            {
                try
                {
                    _squadron = (Squadron)(_squadron + dlg.CreatedShip);
                    UpdateList();
                    lblStatus.Text = "Корабль добавлен. Всего: " + _squadron.Ships.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnCreateCarrier_Click(object sender, EventArgs e)
        {
            using var dlg = new ShipCreationForm();
            dlg.Text = "Создание авианосца";
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.CreatedShip != null)
            {
                try
                {
                    _squadron = (Squadron)(_squadron + dlg.CreatedShip);
                    UpdateList();
                    lblStatus.Text = "Авианосец добавлен. Всего: " + _squadron.Ships.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        

        private void UpdateList()
        {
            lstShips.Items.Clear();
            for (int i = 0; i < _squadron.Ships.Count; i++)
            {
                var s = _squadron.Ships.Get(i);
                string type = s is EntityAircraftCarrier ? "Авианосец" : "Корабль";
                lstShips.Items.Add(
                    $"{i}: {type} (скорость: {s.Speed} уз, водоизмещение: {s.Displacement})");
            }
        }

        

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDeleteIndex.Text, out int idx))
            {
                MessageBox.Show("Неверный индекс", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idx < 0 || idx >= _squadron.Ships.Count)
            {
                MessageBox.Show("Неверный индекс", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _squadron = (Squadron)(_squadron - idx);
            UpdateList();
            lblStatus.Text = "Корабль удалён. Всего: " + _squadron.Ships.Count;

            if (_currentDrawingShip != null && _squadron.Ships.Count == 0)
            {
                _currentDrawingShip = null;
                pnlField.Invalidate();
            }
        }

        

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (_squadron.Ships.Count == 0)
            {
                MessageBox.Show("Нет кораблей для передачи", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lstShips.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите корабль из списка", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var entity = _squadron.Ships.Get(lstShips.SelectedIndex);

            if (entity is EntityAircraftCarrier carrier)
                _currentDrawingShip = new DrawingAircraftCarrier(carrier, 10, 10);
            else
                _currentDrawingShip = new DrawingWarShip(entity, 10, 10);

            pnlField.Invalidate();
            pnlField.Focus();
            lblStatus.Text = "Корабль передан на поле.";
        }

      

        private void btnStep_Click(object sender, EventArgs e)
        {
            if (_currentDrawingShip == null)
            {
                MessageBox.Show("Нет корабля на поле", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var adapter = new MoveableAdapterShip(_currentDrawingShip);
            var canvas = new CanvasForShip(pnlField.Width, pnlField.Height);

            double step = _currentDrawingShip.Entity.Step;
            if (step < 1) step = 1;

            _currentStrategy.Step(adapter, canvas, step);
            pnlField.Invalidate();

            lblStatus.Text =
                $"X={_currentDrawingShip.X:F1}, Y={_currentDrawingShip.Y:F1} " +
                $"(шаг: {step:F2})";
        }

        private void cmbStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentStrategy = cmbStrategy.SelectedIndex == 0
                ? new MoveToCenter()
                : (BaseMovementStrategy)new MoveToRightBottom();
        }

        

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (_currentDrawingShip == null)
            {
                MessageBox.Show("Нет корабля на поле", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _currentDrawingShip.RotateNext();
            pnlField.Invalidate();
            lblStatus.Text = $"Поворот: {_currentDrawingShip.Rotation}°";
        }

        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_currentDrawingShip == null) return;

            double step = _currentDrawingShip.Entity.Step;
            if (step < 1) step = 1;

            double newX = _currentDrawingShip.X;
            double newY = _currentDrawingShip.Y;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    newX -= step;
                    _currentDrawingShip.Rotate(180);
                    break;
                case Keys.Right:
                    newX += step;
                    _currentDrawingShip.Rotate(0);
                    break;
                case Keys.Up:
                    newY -= step;
                    _currentDrawingShip.Rotate(270);
                    break;
                case Keys.Down:
                    newY += step;
                    _currentDrawingShip.Rotate(90);
                    break;
                default:
                    return;
            }

            var canvas = new CanvasForShip(pnlField.Width, pnlField.Height);
            canvas.ClampPosition(ref newX, ref newY);

            _currentDrawingShip.SetPosition(newX, newY);
            pnlField.Invalidate();

            lblStatus.Text =
                $"X={_currentDrawingShip.X:F1}, Y={_currentDrawingShip.Y:F1} " +
                $"ориентация: {_currentDrawingShip.Rotation}°";

            e.Handled = true;
        }

        

        private void PnlField_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            using var pen = new Pen(Color.LightGray);
            for (int i = 0; i < pnlField.Width; i += 40)
                g.DrawLine(pen, i, 0, i, pnlField.Height);
            for (int j = 0; j < pnlField.Height; j += 40)
                g.DrawLine(pen, 0, j, pnlField.Width, j);

            _currentDrawingShip?.Draw(g);
        }
    }
}