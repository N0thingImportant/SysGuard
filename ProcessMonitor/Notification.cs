namespace ProcessMonitor
{
    static class Notification
    {
        public static void Send(string title, string text)
        {
            new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
                .AddAttributionText(text)
                .AddText(title)
                .Show();
        }
    }
}
