using BrowserInterop.Geolocation;

namespace YelpRestaurantFinderComponent.Models;

public class SearchLocation : IEquatable<SearchLocation> {
    public SearchLocation() {

    }
    public bool isOverridden { get; set; } = false;
    public string? OverridenLocation { get; set; }
    public coord? Coords { get; set; }

    public string? Error { get; set; }

    public static explicit operator SearchLocation(GeolocationResult? v) {
        if (v?.Error != null)
            return new() { Error = v.Error.Message };
        if (v?.Location?.Coords is not null)
            return new() { Coords = new(v.Location.Coords.Latitude, v.Location.Coords.Longitude) };
        return new SearchLocation() { Error = "Error searching for location" };
    }

    public bool Equals(SearchLocation? other) {
        if (other == null)
            return false;
        if (isOverridden && other.isOverridden)
            return OverridenLocation?.Equals(other.OverridenLocation)??false;
        if (Coords is not null && other?.Coords is not null)
            Coords.Equals(other.Coords);
        return false;
    }
}

public class coord : IEquatable<coord> {
    public coord() {

    }
    public coord(double latitude, double longitude) {
        Latitude = latitude;
        Longitude = longitude;
    }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public bool Equals(coord? other) =>
        (Latitude == other?.Latitude)
        &&
        (Longitude == other?.Longitude);
}