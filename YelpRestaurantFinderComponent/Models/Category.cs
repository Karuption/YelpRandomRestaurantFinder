using System.Collections;

namespace YelpRestaurantFinderComponent.Models;

public class Category : IEqualityComparer {
    public Category() {
        Parent_Categories = new List<Category>();
    }
    public string? Title { get; set; }
    public string? alias { get; set; }
    public IList<Category> Parent_Categories { get; set; }

    public new bool Equals(object? x, object? y) {
        if (x is Category categoryX && y is Category categoryY)
            return Equals(categoryX, categoryY);
        return false;
    }

    public int GetHashCode(object obj) {
        return string.GetHashCode(Title ?? "" + alias ?? "");
    }

    public bool Equals(Category? x, Category? y) =>
        (x?.Title == y?.Title) && (x?.alias == y?.alias);
}
