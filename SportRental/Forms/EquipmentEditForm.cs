using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SportRental.Entities;
using SportRental.Enums;
using SportRental.Repositories.Interfaces;

namespace SportRental.Forms
{
    public class EquipmentEditForm : Form
    {
        private readonly IEquipmentRepository _repository;
        private readonly IEquipmentTypeRepository _typeRepository;
        private readonly IEquipmentSizeRepository _sizeRepository;
        private readonly Equipment _existing;

        private readonly ComboBox _typeBox;
        private readonly ComboBox _sizeBox;
        private readonly TextBox _numberBox;
        private readonly CheckedListBox _conditionBox;

        public EquipmentEditForm(
            IEquipmentRepository repository,
            IEquipmentTypeRepository typeRepository,
            IEquipmentSizeRepository sizeRepository,
            Equipment existing)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
            _sizeRepository = sizeRepository ?? throw new ArgumentNullException(nameof(sizeRepository));
            _existing = existing;
            InitializeComponent();

            _typeBox = new ComboBox { Left = 150, Top = 20, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            _typeBox.DataSource = new List<EquipmentType>(_typeRepository.GetAll());
            _typeBox.DisplayMember = nameof(EquipmentType.Name);
            _typeBox.ValueMember = nameof(EquipmentType.Id);

            _sizeBox = new ComboBox { Left = 150, Top = 55, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            _sizeBox.DataSource = new List<EquipmentSize>(_sizeRepository.GetAll());
            _sizeBox.DisplayMember = nameof(EquipmentSize.Value);
            _sizeBox.ValueMember = nameof(EquipmentSize.Id);

            _numberBox = new TextBox { Left = 150, Top = 90, Width = 300 };

            _conditionBox = new CheckedListBox { Left = 150, Top = 125, Width = 300, Height = 100, CheckOnClick = true };
            foreach (EquipmentCondition value in Enum.GetValues(typeof(EquipmentCondition)))
            {
                if (value == EquipmentCondition.None) continue;
                _conditionBox.Items.Add(value);
            }

            Controls.Add(new Label { Text = "Вид:", Left = 20, Top = 23, Width = 120 });
            Controls.Add(new Label { Text = "Размер:", Left = 20, Top = 58, Width = 120 });
            Controls.Add(new Label { Text = "Инв. номер:", Left = 20, Top = 93, Width = 120 });
            Controls.Add(new Label { Text = "Состояние:", Left = 20, Top = 128, Width = 120 });
            Controls.Add(_typeBox);
            Controls.Add(_sizeBox);
            Controls.Add(_numberBox);
            Controls.Add(_conditionBox);

            Text = existing == null ? "Добавление инвентаря" : "Редактирование инвентаря";
            if (existing != null)
            {
                _typeBox.SelectedValue = existing.EquipmentTypeId;
                _sizeBox.SelectedValue = existing.EquipmentSizeId;
                _numberBox.Text = existing.InventoryNumber;
                for (var i = 0; i < _conditionBox.Items.Count; i++)
                {
                    var flag = (EquipmentCondition)_conditionBox.Items[i];
                    if (existing.Condition.HasFlag(flag))
                        _conditionBox.SetItemChecked(i, true);
                }
            }
        }

        private void InitializeComponent()
        {
            Width = 520;
            Height = 320;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var saveButton = new Button { Text = "Сохранить", Left = 150, Top = 240, Width = 100 };
            saveButton.Click += ButtonSave_Click;

            var cancelButton = new Button { Text = "Отмена", Left = 270, Top = 240, Width = 100 };
            cancelButton.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(saveButton);
            Controls.Add(cancelButton);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (_typeBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите вид инвентаря.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_sizeBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите размер.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(_numberBox.Text))
            {
                MessageBox.Show("Введите инвентарный номер.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var condition = EquipmentCondition.None;
            foreach (var item in _conditionBox.CheckedItems)
                condition |= (EquipmentCondition)item;

            if (condition == EquipmentCondition.None)
                condition = EquipmentCondition.Good;

            try
            {
                var typeId = ((EquipmentType)_typeBox.SelectedItem).Id;
                var sizeId = ((EquipmentSize)_sizeBox.SelectedItem).Id;

                if (_existing == null)
                {
                    var nextId = 1;
                    foreach (var item in _repository.GetAll())
                        if (item.Id >= nextId) nextId = item.Id + 1;
                    _repository.Add(Equipment.Create(nextId, typeId, sizeId, _numberBox.Text, condition));
                }
                else
                {
                    _repository.Update(Equipment.Create(_existing.Id, typeId, sizeId, _numberBox.Text, condition));
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
