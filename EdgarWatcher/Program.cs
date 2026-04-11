using EdgarWatcher.Configuration;
using EdgarWatcher.Features;
using EdgarWatcher.Features.SecApi;
using EdgarWatcher.Features.Webhook;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EdgarWatcherSettings>(
    builder.Configuration.GetSection(EdgarWatcherSettings.SectionName));

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

app.Run();
