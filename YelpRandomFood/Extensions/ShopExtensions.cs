namespace YelpRandomRestaurantFinder.Extensions;

public static class ShopExtensions {
    public static T? GetRandom<T>(this IEnumerable<T> x) =>
        x.Any()
        ? x.ElementAt(Random.Shared.Next(x.Count()))
        : default;
}
