using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public class YelpApIConnectivityHealthCheck : IHealthCheck {
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private static readonly IEnumerable<HttpStatusCode> _badCodes = new List<HttpStatusCode>() { HttpStatusCode.BadGateway ,HttpStatusCode.NetworkAuthenticationRequired,HttpStatusCode.NotFound};

    public YelpApIConnectivityHealthCheck(HttpClient client, IConfiguration config) {
        _client = client;
        _config = config;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken()) {
        try {
            var response = await _client.PostAsync(new Uri(_config.GetSection("Yelp")["Url"]!), null,
                cancellationToken);

            return !_badCodes.Contains(response.StatusCode)
                ? new HealthCheckResult(HealthStatus.Healthy)
                : new HealthCheckResult(HealthStatus.Unhealthy);
        }
        catch (Exception e) {
            return new HealthCheckResult(HealthStatus.Unhealthy, "unable to get to the Yelp API", e);
        }
    }
}