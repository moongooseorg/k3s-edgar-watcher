{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "EdgarWatcher": {
    "HealthCheckWebhook": "url",
    "DiscordWebhook": "url",
    "IntervalInMilliseconds": 300000,
    "MaxServiceCallsInARow": 10,
    "ServiceCallsResetInMilliseconds": 1000,
    "ServiceName": "edgar-watcher",
    "UserAgent": "sec required user agent",
    "Tickers": ["TSLA"]
  }
}
