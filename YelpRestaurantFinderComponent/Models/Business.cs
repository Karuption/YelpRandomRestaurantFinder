namespace YelpRestaurantFinderComponent.Models;

public class Business {
    public string? Name { get; set; }
    public decimal Rating { get; set; }
    public double Distance { get; set; }
    public string? Price { get; set; }
    public IList<Category>? Categories { get; set; }
    public Uri? Url { get; set; }
    public IList<string> Photos { get; set; }
}