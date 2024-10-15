using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace MockForCurrency;

public class MockServer
{
    private WireMockServer _mockServer;

    public void Start()
    {
        _mockServer = WireMockServer.Start(new WireMockServerSettings
        {
            Port = 3000,
        });

        var exchangeRates = new Dictionary<string, double>
        {
            { "AUD", 60.72 },
            { "AZN", 53.52 },
            { "BYN", 28.29 },
            { "USD", 90.99 },
            { "EUR", 98.78 }
        };

        _mockServer
            .Given(
                Request.Create()
                    .WithPath("/all")
                    .UsingGet()
            )
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyAsJson(exchangeRates)
            );

        foreach (var rate in exchangeRates)
        {
            _mockServer
                .Given(
                    Request.Create()
                        .WithPath($"/{rate.Key}")
                        .UsingGet()
                )
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", "application/json")
                        .WithBodyAsJson(new { Currency = rate.Key, Rate = rate.Value })
                );
        }

        Console.WriteLine("Server running at http://localhost:3000/");
    }

    public void Stop()
    {
        _mockServer.Stop();
        Console.WriteLine("Server stopped.");
    }
}