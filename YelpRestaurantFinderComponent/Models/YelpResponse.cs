namespace YelpRestaurantFinderComponent.Models;

public class YelpResponse {
    public Search? Search { get; set; }
}

public class Search {
    public IList<Business>? Business { get; set; }
    public int? total { get; set; }
}
