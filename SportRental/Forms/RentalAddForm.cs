using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Forms
{
    public class RentalAddForm : Form
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IRentalItemRepository _rentalItemRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        private readonly ComboBox _clientBox;
        private readonly ComboBox _tariffBox;
        private readonly DateTimePicker _issueDatePicker;
        private readonly CheckBox _returnedCheck;
        private readonly DateTimePicker _returnDatePicker;
        private readonly NumericUpDown _amountBox;
        private readonly CheckedListBox _equipmentBox;

        public RentalAddForm(
            IRentalRepository rentalRepository,
            IClientRepository clientRepository,
            ITariffRepository tariffRepository,
            IRentalItemRepository rentalItemRepository,
            IEquipmentRepository equipmentRepository)
        {
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _tariffRepository = tariffRepository ?? throw new ArgumentNullException(nameof(tariffRepository));
            _rentalItemRepository = rentalItemRepository ?? throw new ArgumentNullException(nameof(rentalItemRepository));
            _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));

            InitializeComponent();

            _clientBox = new ComboBox { Left = 160, Top = 20, Width = 320, DropDownStyle = ComboBoxStyle.DropDownList };
            _clientBox.DataSource = new List<Client>(_clientRepository.GetAll());
            _clientBox.DisplayMember = nameof(Client.FullName);
            _clientBox.ValueMember = nameof(Client.Id);

            _tariffBox = new ComboBox { Left = 160, Top = 55, Width = 320, DropDownStyle = ComboBoxStyle.DropDownList };
            _tariffBox.DataSource = new List<Tariff>(_tariffRepository.GetAll());
            _tariffBox.DisplayMember = nameof(Tariff.Name);
            _tariffBox.ValueMember = nameof(Tariff.Id);

            _issueDatePicker = new DateTimePicker { Left = 160, Top = 90, Width = 320, Format = DateTimePickerFormat.Short };

            _returnedCheck = new CheckBox { Left = 160, Top = 125, Width = 320, Text = "Возвращён" };
            _returnDatePicker = new DateTimePicker { Left = 160, Top = 155, Width = 320, Format = DateTimePickerFormat.Short, Enabled = false };
            _returnedCheck.CheckedChanged += (_, __) => _returnDatePicker.Enabled = _returnedCheck.Checked;

            _amountBox = new NumericUpDown { Left = 160, Top = 190, Width = 320, DecimalPlaces = 2, Maximum = 1000000, Minimum = 0 };

            _equipmentBox = new CheckedListBox { Left = 160, Top = 225, Width = 320, Height = 120, CheckOnClick = true };
            foreach (var eq in _equipmentRepository.GetAll())
                _equipmentBox.Items.Add(eq);

            Controls.Add(new Label { Text = "Клиент:", Left = 20, Top = 23, Width = 130 });
            Controls.Add(new Label { Text = "Тариф:", Left = 20, Top = 58, Width = 130 });
            Controls.Add(new Label { Text = "Дата выдачи:", Left = 20, Top = 93, Width = 130 });
            Controls.Add(new Label { Text = "Дата возврата:", Left = 20, Top = 158, Width = 130 });
            Controls.Add(new Label { Text = "Сумма:", Left = 20, Top = 193, Width = 130 });
            Controls.Add(new Label { Text = "Инвентарь:", Left = 20, Top = 228, Width = 130 });

            Controls.Add(_clientBox);
            Controls.Add(_tariffBox);
            Controls.Add(_issueDatePicker);
            Controls.Add(_returnedCheck);
            Controls.Add(_returnDatePicker);
            Controls.Add(_amountBox);
            Controls.Add(_equipmentBox);

            Text = "Добавление проката";
        }

        private void InitializeComponent()
        {
            Width = 540;
            Height = 430;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var saveButton = new Button { Text = "Сохранить", Left = 160, Top = 355, Width = 100 };
            saveButton.Click += ButtonSave_Click;

            var cancelButton = new Button { Text = "Отмена", Left = 280, Top = 355, Width = 100 };
            cancelButton.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(saveButton);
            Controls.Add(cancelButton);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (_clientBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_tariffBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите тариф.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_equipmentBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один инвентарь.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var client = (Client)_clientBox.SelectedItem;
                var tariff = (Tariff)_tariffBox.SelectedItem;

                var nextRentalId = 1;
                foreach (var item in _rentalRepository.GetAll())
                    if (item.Id >= nextRentalId) nextRentalId = item.Id + 1;

                var rental = Rental.Create(
                    nextRentalId,
                    client.Id,
                    tariff.Id,
                    _issueDatePicker.Value,
                    _returnedCheck.Checked ? _returnDatePicker.Value : (DateTime?)null,
                    _amountBox.Value);

                _rentalRepository.Add(rental);

                var nextItemId = 1;
                foreach (var eq in _equipmentBox.CheckedItems)
                {
                    var equipment = (Equipment)eq;
                    _rentalItemRepository.Add(
                        RentalItem.Create(nextItemId++, rental.Id, equipment.Id));
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