namespace EdgarWatcher.Configuration;

public class EdgarWatcherSettings
{
    public const string SectionName = "EdgarWatcher";

    public string HealthCheckWebhook { get; set; } = "";
    public string DiscordWebhook { get; set; } = "";
    public int IntervalInMilliseconds { get; set; } = 300_000; // 5 minutes
    public int MaxServiceCallsInARow { get; set; } = 10;
    public int ServiceCallsResetInMilliseconds { get; set; } = 1000;
    public string ServiceName { get; set; } = "edgar-watcher";
    public string UserAgent { get; set; } = "";
    public List<string> Tickers { get; set; } = ["AMC", "GME"];
}
