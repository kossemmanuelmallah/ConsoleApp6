namespace ProjectWarship;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        string log = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "warship_log.txt");

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) =>
        {
            File.AppendAllText(log, e.Exception + "\n\n");
            MessageBox.Show(e.Exception.ToString(), "Îøèáêà");
        };
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            File.AppendAllText(log, e.ExceptionObject + "\n\n");

        Application.Run(new FormWarship());   // TEST : on lance seulement le formulaire de test
    }
}