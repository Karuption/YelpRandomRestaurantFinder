namespace YelpRandomRestaurantFinder.Data;

public static class Query {
    public static GraphQL.GraphQLRequest GetAllCategory(string Location, string Category = "Food", float Range = 8000) =>
        new GraphQL.GraphQLRequest {
            Query = @"query($location: String!, $cat: String!, $range: Float){
                    search(location: $location, categories: $cat, radius: $range, open_now: true) {
                        business {
                        name
                        rating
                        distance
                        price
                        url
                        photos
                        categories{ title }
                        }
                    }
                }",
            Variables =
                new { location = $"{Location}", cat = Category, range = Range }
        };
}
