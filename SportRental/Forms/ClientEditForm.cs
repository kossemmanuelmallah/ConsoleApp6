using System;
using System.Windows.Forms;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Forms
{
    public class ClientEditForm : Form
    {
        private readonly IClientRepository _repository;
        private readonly Client _existing;
        private readonly TextBox _fullNameBox;
        private readonly TextBox _phoneBox;

        public ClientEditForm(IClientRepository repository, Client existing)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _existing = existing;
            InitializeComponent();

            _fullNameBox = new TextBox { Left = 150, Top = 20, Width = 300 };
            _phoneBox = new TextBox { Left = 150, Top = 55, Width = 300 };

            Controls.Add(new Label { Text = "ФИО:", Left = 20, Top = 23, Width = 120 });
            Controls.Add(new Label { Text = "Телефон:", Left = 20, Top = 58, Width = 120 });
            Controls.Add(_fullNameBox);
            Controls.Add(_phoneBox);

            Text = existing == null ? "Добавление клиента" : "Редактирование клиента";
            if (existing != null)
            {
                _fullNameBox.Text = existing.FullName;
                _phoneBox.Text = existing.Phone;
            }
        }

        private void InitializeComponent()
        {
            Width = 520;
            Height = 200;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var saveButton = new Button { Text = "Сохранить", Left = 150, Top = 100, Width = 100 };
            saveButton.Click += ButtonSave_Click;

            var cancelButton = new Button { Text = "Отмена", Left = 270, Top = 100, Width = 100 };
            cancelButton.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(saveButton);
            Controls.Add(cancelButton);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_fullNameBox.Text))
            {
                MessageBox.Show("Введите ФИО клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(_phoneBox.Text))
            {
                MessageBox.Show("Введите телефон.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (_existing == null)
                {
                    var nextId = 1;
                    foreach (var item in _repository.GetAll())
                        if (item.Id >= nextId) nextId = item.Id + 1;
                    _repository.Add(Client.Create(nextId, _fullNameBox.Text, _phoneBox.Text));
                }
                else
                {
                    _repository.Update(Client.Create(_existing.Id, _fullNameBox.Text, _phoneBox.Text));
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