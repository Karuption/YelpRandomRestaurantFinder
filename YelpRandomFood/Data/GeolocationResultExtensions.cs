using System.Text;

using BrowserInterop.Geolocation;

namespace YelpRandomRestaurantFinder.Data {
    public static class GeolocationResultExtensions {
        public static bool hasChanged(this GeolocationResult? x, GeolocationResult? y) {
            if (x == null && y == null) return false;
            if (x == null || y == null) return true;

            if (x.Location == null && y.Location == null) return false;
            if (x == null || y == null) return true;

            return x!.Location!.Coords.Latitude != y!.Location!.Coords.Latitude
                   && x!.Location!.Coords.Longitude != y!.Location!.Coords.Longitude;
        }

        public static string? getLocation(this GeolocationResult x) {
            StringBuilder loc = new();
            if (x?.Location?.Coords is not null) {
                loc.Append(x.Location.Coords.Latitude.ToString());
                loc.Append(", ");
                loc.Append(x.Location?.Coords.Longitude.ToString());
                return loc.ToString();
            }

            return null;
        }
    }
}
