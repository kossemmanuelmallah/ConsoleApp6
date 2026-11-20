using System;
using System.Linq;
using System.Windows.Forms;
using SportRental.Entities;
using SportRental.Repositories.Interfaces;

namespace SportRental.Forms
{
    public class RentalListForm : Form
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IRentalItemRepository _rentalItemRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        private readonly DataGridView _grid;

        public RentalListForm(
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
            Text = "Прокат";
            Width = 900;
            Height = 500;
            StartPosition = FormStartPosition.CenterParent;

            var buttonsPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 };

            var addButton = new Button { Text = "Добавить", Width = 100 };
            addButton.Click += ButtonAdd_Click;

            var refreshButton = new Button { Text = "Обновить", Width = 100 };
            refreshButton.Click += ButtonRefresh_Click;

            buttonsPanel.Controls.Add(addButton);
            buttonsPanel.Controls.Add(refreshButton);

            Controls.Add(buttonsPanel);
        }

        private void LoadData()
        {
            var clients = _clientRepository.GetAll().ToDictionary(x => x.Id, x => x.FullName);
            var tariffs = _tariffRepository.GetAll().ToDictionary(x => x.Id, x => x.Name);

            var rows = _rentalRepository.GetAll()
                .Select(r => new
                {
                    r.Id,
                    Client = clients.TryGetValue(r.ClientId, out var c) ? c : $"#{r.ClientId}",
                    Tariff = tariffs.TryGetValue(r.TariffId, out var t) ? t : $"#{r.TariffId}",
                    IssueDate = r.IssueDate.ToString("dd.MM.yyyy"),
                    ReturnDate = r.ReturnDate?.ToString("dd.MM.yyyy") ?? "не возвращён",
                    TotalAmount = r.TotalAmount
                })
                .ToList();

            _grid.DataSource = null;
            _grid.Columns.Clear();
            _grid.DataSource = rows;
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
            using (var form = new RentalAddForm(
                _rentalRepository,
                _clientRepository,
                _tariffRepository,
                _rentalItemRepository,
                _equipmentRepository))
            {
                if (form.ShowDialog(this) == DialogResult.OK) LoadData();
            }
        }
    }
}