using System.Text;

using YelpRandomRestaurantFinder.Models;

namespace YelpRandomRestaurantFinder.Data;

public static class LocationExtensions {
    //public static bool hasChanged(this GeolocationResult? x, GeolocationResult? y) {
    //    if (x?.Error is not null && y?.Location is null) return false;
    //    if (x == null && y == null) return false;
    //    if (x == null || y == null) return true;

    //    if (x.Location == null && y.Location == null) return false;
    //    if (x == null || y == null) return true;

    //    return x!.Location!.Coords.Latitude != y!.Location!.Coords.Latitude
    //           && x!.Location!.Coords.Longitude != y!.Location!.Coords.Longitude;
    //}

    public static bool hasChanged(this SearchLocation? x, SearchLocation? y) {
        if (x == null) {
            if (y == null)
                return false;
            return true;
        }
        return !x.Equals(y);
    }

    public static string? getCurrentLocation(this SearchLocation x) {
        if(x.isOverridden) return x.OverridenLocation;
        
        StringBuilder loc = new();
        if (x?.Coords is not null) {
            loc.Append(x.Coords.Latitude.ToString());
            loc.Append(", ");
            loc.Append(x?.Coords.Longitude.ToString());
            return loc.ToString();
        }

        return null;
    }
}
