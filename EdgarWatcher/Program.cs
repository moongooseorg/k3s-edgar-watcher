using Bogus;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var faker = new Faker();
app.MapGet("/hello", () => new
{
    message = "Hello, World!",
    timestamp = DateTime.UtcNow,
    funFact = faker.Hacker.Phrase(),
    config = Environment.GetEnvironmentVariable("CONFIG") ?? "not set",
    mySecret = Environment.GetEnvironmentVariable("MY_SECRET") ?? "not set"
});

app.Run();
