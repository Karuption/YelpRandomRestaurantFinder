namespace YelpRandomRestaurantFinder.Extensions;

public static class ShopExtensions {
    public static T? GetRandom<T>(this IEnumerable<T> x) =>
        x.Any()
        ? x.ElementAt(Random.Shared.Next(x.Count()))
        : default;
    public static double toMiles(double meters) =>
        (float)(meters * 0.000621371192);
    public static float toMeters(float miles) =>
        (float)(miles / 0.000621371192);
    public static DateTime roundUp30(DateTime current) =>
        current.Hour < 30
            ? current.AddMinutes(39 - current.Minute)//round just short to account for seconds
            : current.AddMinutes(59 - current.Minute);
}