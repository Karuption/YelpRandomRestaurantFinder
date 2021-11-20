using BrowserInterop.Geolocation;
using BrowserInterop.Extensions;
using Microsoft.JSInterop;

namespace YelpRandomRestaurantFinder.Data {
    public interface ILocationService {
        Task<(GeolocationResult?, bool)> GetLocation(GeolocationResult? currentPosition);
    }

    public class LocationService : ILocationService {
        private IJSRuntime _jsRuntime;
        public LocationService(IJSRuntime JSRuntime) {
            _jsRuntime = JSRuntime;
        }

        /// <summary>
        /// This method finds the current location using the location services provided. 
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns>GeolocationResult?, location changed</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(GeolocationResult?, bool)> GetLocation(GeolocationResult? currentPosition) {
            var window = await _jsRuntime.Window();
            var navigator = await window.Navigator();
            var newPos = await navigator.Geolocation.GetCurrentPosition();

            if (newPos.hasChanged(currentPosition))
                return (newPos, true);

            if (currentPosition?.Location is null) {
                throw new NotImplementedException();
            }

            return (currentPosition, false);
        }
    }

}
