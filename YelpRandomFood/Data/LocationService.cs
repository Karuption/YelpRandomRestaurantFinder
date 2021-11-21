using BrowserInterop.Extensions;
using BrowserInterop.Geolocation;

using Microsoft.JSInterop;

namespace YelpRandomRestaurantFinder.Data;

public interface ILocationService {
    Task<(GeolocationResult?, bool)> GetLocation(GeolocationResult? currentPosition);
}

public class LocationService : ILocationService {
    private IJSRuntime _jsRuntime;
    private readonly ILogger logger;
    private readonly HttpContext httpContext;

    public LocationService(IJSRuntime JSRuntime, ILogger<LocationService> logger, IHttpContextAccessor contexAccessor) {
        _jsRuntime = JSRuntime;
        this.logger = logger;
        httpContext = contexAccessor.HttpContext;
    }

    /// <summary>
    /// This method finds the current location using the location services provided. 
    /// </summary>
    /// <param name="currentPosition"></param>
    /// <returns>GeolocationResult?, location changed</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<(GeolocationResult?, bool)> GetLocation(GeolocationResult? currentPosition) {
        BrowserInterop.WindowInterop? window = await _jsRuntime.Window();
        BrowserInterop.WindowNavigator? navigator = await window.Navigator();
        GeolocationResult? newPos = await navigator.Geolocation.GetCurrentPosition();

        if (newPos.hasChanged(currentPosition))
            return (newPos, true);

        if (currentPosition?.Location is null) {
            logger.LogInformation($"Unable to resolve location for IP {httpContext.Connection.RemoteIpAddress}");
        }

        return (currentPosition, false);
    }
}
