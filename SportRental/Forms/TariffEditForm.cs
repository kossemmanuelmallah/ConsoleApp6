using System;
using System.Windows.Forms;
using SportRental.Entities;
using SportRental.Enums;
using SportRental.Repositories.Interfaces;

namespace SportRental.Forms
{
    public class TariffEditForm : Form
    {
        private readonly ITariffRepository _repository;
        private readonly Tariff _existing;
        private readonly TextBox _nameBox;
        private readonly ComboBox _periodBox;
        private readonly NumericUpDown _priceBox;

        public TariffEditForm(ITariffRepository repository, Tariff existing)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _existing = existing;
            InitializeComponent();

            _nameBox = new TextBox { Left = 150, Top = 20, Width = 300 };
            _periodBox = new ComboBox { Left = 150, Top = 55, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            _periodBox.Items.Add(RentalPeriod.Hour);
            _periodBox.Items.Add(RentalPeriod.Day);
            _priceBox = new NumericUpDown { Left = 150, Top = 90, Width = 300, DecimalPlaces = 2, Maximum = 1000000, Minimum = 0 };

            Controls.Add(new Label { Text = "Название:", Left = 20, Top = 23, Width = 120 });
            Controls.Add(new Label { Text = "Период:", Left = 20, Top = 58, Width = 120 });
            Controls.Add(new Label { Text = "Цена:", Left = 20, Top = 93, Width = 120 });
            Controls.Add(_nameBox);
            Controls.Add(_periodBox);
            Controls.Add(_priceBox);

            Text = existing == null ? "Добавление тарифа" : "Редактирование тарифа";
            if (existing != null)
            {
                _nameBox.Text = existing.Name;
                _periodBox.SelectedItem = existing.Period;
                _priceBox.Value = existing.Price;
            }
            else
            {
                _periodBox.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            Width = 520;
            Height = 220;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var saveButton = new Button { Text = "Сохранить", Left = 150, Top = 135, Width = 100 };
            saveButton.Click += ButtonSave_Click;

            var cancelButton = new Button { Text = "Отмена", Left = 270, Top = 135, Width = 100 };
            cancelButton.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(saveButton);
            Controls.Add(cancelButton);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_nameBox.Text))
            {
                MessageBox.Show("Введите название тарифа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_periodBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите период.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_priceBox.Value <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var period = (RentalPeriod)_periodBox.SelectedItem;

                if (_existing == null)
                {
                    var nextId = 1;
                    foreach (var item in _repository.GetAll())
                        if (item.Id >= nextId) nextId = item.Id + 1;
                    _repository.Add(Tariff.Create(nextId, _nameBox.Text, period, _priceBox.Value));
                }
                else
                {
                    _repository.Update(Tariff.Create(_existing.Id, _nameBox.Text, period, _priceBox.Value));
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
