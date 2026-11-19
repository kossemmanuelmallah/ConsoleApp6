using System;
using System.Windows.Forms;
using SportRental.Forms;
using Unity;

namespace SportRental
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = ContainerFactory.Build();
            var mainForm = container.Resolve<MainForm>();
            Application.Run(mainForm);
        }
    }
}