using System;
using System.Windows.Forms;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Forms
{
    public class EquipmentListForm : Form
    {
        private readonly IEquipmentRepository _repository;
        private readonly IEquipmentTypeRepository _typeRepository;
        private readonly IEquipmentSizeRepository _sizeRepository;
        private readonly DataGridView _grid;

        public EquipmentListForm(
            IEquipmentRepository repository,
            IEquipmentTypeRepository typeRepository,
            IEquipmentSizeRepository sizeRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
            _sizeRepository = sizeRepository ?? throw new ArgumentNullException(nameof(sizeRepository));

            InitializeComponent();
            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            Controls.Add(_grid);
            LoadData();
        }

        private void InitializeComponent()
        {
            Text = "Инвентарь";
            Width = 800;
            Height = 450;
            StartPosition = FormStartPosition.CenterParent;

            var buttonsPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 };

            var addButton = new Button { Text = "Добавить", Width = 100 };
            addButton.Click += ButtonAdd_Click;

            var editButton = new Button { Text = "Редактировать", Width = 120 };
            editButton.Click += ButtonEdit_Click;

            var deleteButton = new Button { Text = "Удалить", Width = 100 };
            deleteButton.Click += ButtonDelete_Click;

            buttonsPanel.Controls.Add(addButton);
            buttonsPanel.Controls.Add(editButton);
            buttonsPanel.Controls.Add(deleteButton);

            Controls.Add(buttonsPanel);
        }

        private void LoadData()
        {
            _grid.DataSource = null;
            _grid.Refresh();
            _grid.DataSource = new System.Collections.Generic.List<Equipment>(_repository.GetAll());
            _grid.Refresh();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (var form = new EquipmentEditForm(
                _repository, _typeRepository, _sizeRepository, null))
            {
                if (form.ShowDialog(this) == DialogResult.OK) LoadData();
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (_grid.CurrentRow?.DataBoundItem is Equipment selected)
            {
                using (var form = new EquipmentEditForm(
                    _repository, _typeRepository, _sizeRepository, selected))
                {
                    if (form.ShowDialog(this) == DialogResult.OK) LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (_grid.CurrentRow?.DataBoundItem is Equipment selected)
            {
                _repository.Delete(selected.Id);
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}