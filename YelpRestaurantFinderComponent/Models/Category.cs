using System.Collections;

namespace YelpRestaurantFinderComponent.Models;

public class Category : IEqualityComparer {
    public Category() {

    }
    public string? Title { get; set; }
    public string alias { get; set; }
    public IList<Category> Parent_Categories { get; set; }

    public new bool Equals(object? x, object? y) {
        if(x is Category && y is Category)
            return Equals((Category)x, (Category)y);
        else
            return false;
    }
    public bool Equals(Category? x, Category? y) => 
        (x?.Title == y?.Title) && (x?.alias == y?.alias);

    public int GetHashCode(object obj) => 
        obj.GetHashCode();
}
