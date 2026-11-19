using System;
using System.Windows.Forms;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Forms
{
    public class EquipmentTypeEditForm : Form
    {
        private readonly IEquipmentTypeRepository _repository;
        private readonly EquipmentType _existing;
        private readonly TextBox _nameBox;

        public EquipmentTypeEditForm(IEquipmentTypeRepository repository, EquipmentType existing)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _existing = existing;
            InitializeComponent();
            _nameBox = new TextBox { Left = 150, Top = 20, Width = 300 };
            Controls.Add(_nameBox);
            Controls.Add(new Label { Text = "Название:", Left = 20, Top = 23, Width = 120 });

            Text = existing == null ? "Добавление вида инвентаря" : "Редактирование вида инвентаря";
            if (existing != null) _nameBox.Text = existing.Name;
        }

        private void InitializeComponent()
        {
            Width = 500;
            Height = 150;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var saveButton = new Button { Text = "Сохранить", Left = 150, Top = 70, Width = 100 };
            saveButton.Click += ButtonSave_Click;

            var cancelButton = new Button { Text = "Отмена", Left = 270, Top = 70, Width = 100 };
            cancelButton.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(saveButton);
            Controls.Add(cancelButton);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_nameBox.Text))
            {
                MessageBox.Show("Введите название вида инвентаря.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (_existing == null)
                {
                    var nextId = 1;
                    foreach (var item in _repository.GetAll())
                        if (item.Id >= nextId) nextId = item.Id + 1;
                    _repository.Add(EquipmentType.Create(nextId, _nameBox.Text));
                }
                else
                {
                    _repository.Update(EquipmentType.Create(_existing.Id, _nameBox.Text));
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