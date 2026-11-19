using System;
using System.Windows.Forms;
using Unity;

namespace SportRental.Forms
{
    public class MainForm : Form
    {
        private readonly IUnityContainer _container;

        public MainForm(IUnityContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Пункт проката спортинвентаря";
            Width = 900;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;

            var menu = new MenuStrip();

            var directoriesMenu = new ToolStripMenuItem("Справочники");
            directoriesMenu.DropDownItems.Add("Виды инвентаря", null, (_, __) => OpenForm<EquipmentTypeListForm>());
            directoriesMenu.DropDownItems.Add("Размеры", null, (_, __) => OpenForm<EquipmentSizeListForm>());
            directoriesMenu.DropDownItems.Add("Тарифы", null, (_, __) => OpenForm<TariffListForm>());
            directoriesMenu.DropDownItems.Add("Клиенты", null, (_, __) => OpenForm<ClientListForm>());
            directoriesMenu.DropDownItems.Add("Инвентарь", null, (_, __) => OpenForm<EquipmentListForm>());

            var operationsMenu = new ToolStripMenuItem("Операции");
            operationsMenu.DropDownItems.Add("Прокат", null, (_, __) => OpenForm<RentalListForm>());

            var reportsMenu = new ToolStripMenuItem("Отчёты");

            menu.Items.Add(directoriesMenu);
            menu.Items.Add(operationsMenu);
            menu.Items.Add(reportsMenu);

            MainMenuStrip = menu;
            Controls.Add(menu);
        }

        private void OpenForm<T>() where T : Form
        {
            var form = _container.Resolve<T>();
            form.ShowDialog();
        }
    }
}