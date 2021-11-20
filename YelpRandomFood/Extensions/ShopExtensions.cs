

using System.Diagnostics.CodeAnalysis;

namespace YelpRandomRestaurantFinder.Extensions {
    public static class ShopExtensions {
        public static T? GetRandom<T>(this IEnumerable<T> x) =>
            x.Any() 
            ? x.ElementAt(new Random().Next(x.Count())) 
            : default;
    }
}
