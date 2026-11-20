namespace WarShipStorage;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        WinFormsApp1.ApplicationConfiguration.Initialize();

        // En cas de plantage : l'erreur est écrite dans warship_log.txt sur le Bureau
        string log = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "warship_log.txt");

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) =>
        {
            File.AppendAllText(log, e.Exception + "\n\n");
            MessageBox.Show(e.Exception.ToString(), "Ошибка");
        };
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            File.AppendAllText(log, e.ExceptionObject + "\n\n");

        Application.Run(new FormSquadron());
    }
}