using BrowserInterop.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using YelpRestaurantFinderComponent.Extensions;
using YelpRestaurantFinderComponent.Models;

namespace YelpRestaurantFinderComponent.Services;

public interface ILocationService {
    Task<(SearchLocation?, bool)> GetLocation(SearchLocation? currentPosition, SearchLocation? overridenLocation);
}

public class LocationService : ILocationService {
    private IJSRuntime _jsRuntime;
    private readonly ILogger logger;
    private readonly HttpContext httpContext;

    public LocationService(IJSRuntime JSRuntime, ILogger<LocationService> logger, IHttpContextAccessor contexAccessor) {
        _jsRuntime = JSRuntime;
        this.logger = logger;
        httpContext = contexAccessor?.HttpContext;
    }

    /// <summary>
    /// This method finds the current location using the location services provided. 
    /// </summary>
    /// <param name="currentPosition"></param>
    /// <returns>SearchLocation?, location changed</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<(SearchLocation?, bool)> GetLocation(SearchLocation? currentPosition, SearchLocation? overridenPosition = null) {
        if (currentPosition is not null && overridenPosition is not null) {
            if (currentPosition.isOverridden)
                return (overridenPosition, currentPosition.OverridenLocation == overridenPosition.OverridenLocation);
            if(overridenPosition.isOverridden)
                return (overridenPosition, true);
        }


        BrowserInterop.WindowInterop? window = await _jsRuntime.Window();
        BrowserInterop.WindowNavigator? navigator = await window.Navigator();
        SearchLocation? newPos = (SearchLocation?) await navigator.Geolocation.GetCurrentPosition();

        if (newPos.hasChanged(currentPosition))
            return (newPos, true);

        if (currentPosition?.Coords is null) {
            logger.LogInformation($"Unable to resolve location for IP {httpContext.Connection.RemoteIpAddress}");
        }

        return ((SearchLocation) currentPosition, false);
    }
}
