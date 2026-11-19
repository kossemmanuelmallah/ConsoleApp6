using System;
using System.Windows.Forms;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Forms
{
    public class EquipmentSizeEditForm : Form
    {
        private readonly IEquipmentSizeRepository _repository;
        private readonly EquipmentSize _existing;
        private readonly TextBox _valueBox;

        public EquipmentSizeEditForm(IEquipmentSizeRepository repository, EquipmentSize existing)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _existing = existing;
            InitializeComponent();
            _valueBox = new TextBox { Left = 150, Top = 20, Width = 300 };
            Controls.Add(_valueBox);
            Controls.Add(new Label { Text = "Размер:", Left = 20, Top = 23, Width = 120 });

            Text = existing == null ? "Добавление размера" : "Редактирование размера";
            if (existing != null) _valueBox.Text = existing.Value;
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
            if (string.IsNullOrWhiteSpace(_valueBox.Text))
            {
                MessageBox.Show("Введите размер.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (_existing == null)
                {
                    var nextId = 1;
                    foreach (var item in _repository.GetAll())
                        if (item.Id >= nextId) nextId = item.Id + 1;
                    _repository.Add(EquipmentSize.Create(nextId, _valueBox.Text));
                }
                else
                {
                    _repository.Update(EquipmentSize.Create(_existing.Id, _valueBox.Text));
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