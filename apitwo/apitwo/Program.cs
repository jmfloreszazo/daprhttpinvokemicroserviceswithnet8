using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/test", async () =>
{
    var httpClient = DaprClient.CreateInvokeHttpClient("apione");
    return await httpClient.GetAsync("weatherforecast");

});

app.Run();