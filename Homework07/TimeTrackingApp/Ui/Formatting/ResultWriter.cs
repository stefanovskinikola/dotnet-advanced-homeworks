namespace TimeTrackingApp.Ui.Formatting;

public static class ResultWriter
{
    public static void Write(bool isSuccess, string message)
    {
        if (isSuccess)
        {
            ConsoleStyler.WriteSuccess(message);
            return;
        }

        ConsoleStyler.WriteError(message);
    }
}
