using System.Text;
using YelpRestaurantFinderComponent.Models;

namespace YelpRestaurantFinderComponent.Extensions;

public static class LocationExtensions {

    public static bool hasChanged(this SearchLocation? x, SearchLocation? y) {
        if (x is null) {
            if (y is null)
                return false;
            return true;
        }
        return !x.Equals(y); //if x == y, then the position has not changed
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
