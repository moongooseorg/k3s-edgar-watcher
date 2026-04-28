using EdgarWatcher.Configuration;
using EdgarWatcher.Features;
using EdgarWatcher.Features.SecApi;
using EdgarWatcher.Features.Webhook;
using Microsoft.Extensions.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EdgarWatcherSettings>(
    builder.Configuration.GetSection(EdgarWatcherSettings.SectionName));

builder.Services.PostConfigure<EdgarWatcherSettings>(settings =>
{
    string[] configuredTickers = builder.Configuration
        .GetSection(EdgarWatcherSettings.SectionName)
        .GetSection(nameof(EdgarWatcherSettings.Tickers))
        .GetChildren()
        .Select(child => child.Value ?? "")
        .Where(value => !string.IsNullOrWhiteSpace(value))
        .ToArray();

    if (configuredTickers.Length > 0)
        settings.Tickers = configuredTickers;
});

builder.Services.Configure<NotificationSettings>(
    builder.Configuration.GetSection(NotificationSettings.SectionName));

builder.Services.AddHttpClient<SecApiService>(client =>
{
    string userAgent = builder.Configuration
        .GetSection(EdgarWatcherSettings.SectionName)
        .GetValue<string>("UserAgent") ?? "";
    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", userAgent);
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
});

builder.Services.AddHttpClient<DiscordSecMessenger>();

builder.Services.AddHostedService<WatcherService>();

WebApplication app = builder.Build();

EdgarWatcherSettings startupSettings = app.Services
    .GetRequiredService<IOptions<EdgarWatcherSettings>>().Value;
ILogger<Program> startupLogger = app.Services.GetRequiredService<ILogger<Program>>();
startupLogger.LogInformation(
    "Configured to watch {Count} ticker(s): {Tickers}",
    startupSettings.Tickers.Length,
    string.Join(", ", startupSettings.Tickers));

app.Run();
