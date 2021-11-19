namespace YelpRandomRestaurantFinder.Data {
    public class Category {
        public Category() {

        }
        public string? Title { get; set; }
        public string alias { get; set; }
        public IList<Category> Parent_Categories { get; set; }
    }


}