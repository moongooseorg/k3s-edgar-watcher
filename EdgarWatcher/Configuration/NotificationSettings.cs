namespace EdgarWatcher.Configuration;

public class NotificationSettings
{
    public const string SectionName = "Notification";

    public string HealthCheckWebhook { get; set; } = "";
    public string DiscordWebhook { get; set; } = "";
}
