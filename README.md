# .NET 6 DAPR Microservices with HTTP Invoke

This project use:

* DAPR
* .NET6 with minimal API
* Docker

## Pre-requisites

* All this proyect can run with Visual Studio 2022, I recomend the use of this environment.
* [Install DAPR](https://docs.dapr.io/getting-started/install-dapr-cli/)

## Run steps

In **apione** you need to create a production image

```bash
docker build -t apione -f apione\Dockerfile .
```

Run **apitwo** with this steps:

1. Generate cert for **apione** (go to HTTP section before this).
2. Select docker-compose.
3. Set as startup project.
4. Run "Docker Compose".

This automatic launch directly loads the minimal api that invokes Dapr's http client.

```c#
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
```

## HTTPS

This [exampe use HTTPS](https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0) for **apione**

```bash
version: '3.4'

services:
  apione:
    environment:
      - ASPNETCORE_Kestrel__Certificates__Default__Password=12345
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

    ports:
      - "80"
      - "443"
    volumes:
      - ~/.aspnet/https:/https:ro
```

You need to run this commands in order to generate a pfx.

Clean you certs.

```bash
dotnet dev-certs https --clean
```

And create new one:

```bash
dotnet dev-certs https --trust -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p 12345
```

[![GitKraken shield](https://img.shields.io/badge/GitKraken-Legendary%20Git%20Tools-teal?style=plastic&logo=gitkraken)](https://gitkraken.com/invite/sUviHf86)

[![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg?)](https://github.com/tterb/atomic-design-ui/blob/master/LICENSEs)
