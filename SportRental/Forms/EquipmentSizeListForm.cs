using System;
using System.Windows.Forms;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Forms
{
    public class EquipmentSizeListForm : Form
    {
        private readonly IEquipmentSizeRepository _repository;
        private readonly DataGridView _grid;

        public EquipmentSizeListForm(IEquipmentSizeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            InitializeComponent();
            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            Controls.Add(_grid);
            LoadData();
        }

        private void InitializeComponent()
        {
            Text = "Размеры";
            Width = 700;
            Height = 450;
            StartPosition = FormStartPosition.CenterParent;

            var buttonsPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 };

            var addButton = new Button { Text = "Добавить", Width = 100 };
            addButton.Click += ButtonAdd_Click;

            var editButton = new Button { Text = "Редактировать", Width = 120 };
            editButton.Click += ButtonEdit_Click;

            var deleteButton = new Button { Text = "Удалить", Width = 100 };
            deleteButton.Click += ButtonDelete_Click;

            var refreshButton = new Button { Text = "Обновить", Width = 100 };
            refreshButton.Click += ButtonRefresh_Click;

            buttonsPanel.Controls.Add(addButton);
            buttonsPanel.Controls.Add(editButton);
            buttonsPanel.Controls.Add(deleteButton);
            buttonsPanel.Controls.Add(refreshButton);

            Controls.Add(buttonsPanel);
        }

        private void LoadData()
        {
            _grid.DataSource = null;
            _grid.Columns.Clear();
            _grid.DataSource = new System.Collections.Generic.List<EquipmentSize>(_repository.GetAll());
            _grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _grid.ColumnHeadersHeight = 30;
            if (_grid.Rows.Count > 0)
            {
                _grid.FirstDisplayedScrollingRowIndex = 0;
                _grid.ClearSelection();
                _grid.Rows[0].Selected = true;
            }
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (var form = new EquipmentSizeEditForm(_repository, null))
            {
                if (form.ShowDialog(this) == DialogResult.OK) LoadData();
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (_grid.CurrentRow?.DataBoundItem is EquipmentSize selected)
            {
                using (var form = new EquipmentSizeEditForm(_repository, selected))
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
            if (_grid.CurrentRow?.DataBoundItem is EquipmentSize selected)
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