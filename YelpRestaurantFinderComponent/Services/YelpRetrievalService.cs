using GraphQL.Client.Abstractions;

using Microsoft.Extensions.Logging;

using YelpRestaurantFinderComponent.Extensions;
using YelpRestaurantFinderComponent.Models;

namespace YelpRestaurantFinderComponent.Services;
public interface IYelpRetrievalService {
    Task<IList<Business>> GetYelpData(string? currentLocation, float radiusMiles);
}

public class YelpRetrievalService : IYelpRetrievalService {
    private readonly ILogger<YelpResponse> _logger;
    private readonly IGraphQLClient gclient;

    public YelpRetrievalService(ILogger<YelpResponse> logger, IGraphQLClient gclient) {
        _logger = logger;
        this.gclient = gclient;
    }
    public async Task<IList<Business>> GetYelpData(string? currentLocation, float radiusMiles) {
        GraphQL.GraphQLRequest? request = Query.GetAllCategory(currentLocation, ShopExtensions.toMeters(radiusMiles));
        GraphQL.GraphQLResponse<YelpResponse>? response = await gclient.SendQueryAsync<YelpResponse>(request);

        if (response.Errors is not null) {
            _logger.LogError($"GraphQL Response Error: {response.Errors[0].Message} for Location {request.Variables}");
            return new List<Business>();
        }

        return response?.Data?.Search?.Business;
    }
}