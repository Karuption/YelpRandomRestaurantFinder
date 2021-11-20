namespace YelpRandomRestaurantFinder.Data {
    public class Business {
        public string? Name { get; set; }
        public decimal Rating { get; set; }
        public double Distance { get; set; }
        public string? Price { get; set; }
        public IList<Category>? Categories { get; set; }
        public Uri? Url { get; set; }
        public Location Location { get; set; }
        //public string Alias { get; set; }
        //public bool is_Claimed { get; set; }
        //public bool is_Closed { get; set; }
        //public string Phone { get; set; }
        //public string Display_Phone { get; set; }
        //public int Review_Count { get; set; }
        public IList<string> Photos { get; set; }
        //public Hours Hours { get; set; }
        //public IList<Review> Reviews { get; set; }
        //public BusinessMessaging messaging { get; set; }
        //public IList<SpecialHours> Special_Hours { get; set; }
        //public BusinessAttributes Attributes { get; set; }
        //public string id { get; set; } = String.Empty;
    }

    //public class BusinessAttributes {
    //    public BusinessAttribute Wheelchair_Accessable { get; set; }
    //    public BusinessAttribute Open_To_All { get; set; }
    //    public BusinessAttribute Gender_Neutral_Restrooms { get; set; }
    //}

    //public class BusinessAttribute {
    //    public string value_Code { get; set; }
    //    public string Name_Code { get; set; }
    //}

    //public class BusinessMessaging {
    //    public Uri Url { get; set; }
    //    public string Use_Case_Text { get; set; }
    //}

    //public class SpecialHours {
    //    public bool is_Overnight { get; set; }
    //    public bool is_Closed { get; set; }
    //    public string End { get; set; }
    //    public string Start { get; set; }
    //    public DateOnly Date { get; set; }
    //}

    //public class Review {
    //    public PublicReviewResponse Public_Response { get; set; }
    //    public Uri Url { get; set; }
    //    public string Time_Created { get; set; }
    //    public string Text { get; set; }
    //    public User User { get; set; }
    //    public bool id { get; set; }
    //    public int Rating { get; set; }
    //}

    //public class PublicReviewResponse {
    //    public BusinessUser Business_User { get; set; }
    //    public DateTime Time_Created { get; set; }
    //    public string Text { get; set; }
    //}

    //public class BusinessUser {
    //    public Uri Phote_Url { get; set; }
    //    public string Role { get; set; }
    //    public string Name { get; set; }
    //}

    //public class User {
    //    public Uri Image_Url { get; set; }
    //    public string Name { get; set; }
    //    public string Profile_Url { get; set; }
    //    public string id { get; set; }
    //}

    //public class Hours {
    //    public string Hours_Type { get; set; }
    //    public IList<OpenHours> Open { get; set; }
    //    public bool is_Open_Now { get; set; }
    //}

    //public class OpenHours {
    //    public int Day { get; set; }
    //    public string Start { get; set; }
    //    public string End { get; set; }
    //    public bool is_Overnight { get; set; }
    //}

    public class Location {
        //    public String Address1 { get; set; }
        //    public string Address2 { get; set; }
        //    public string Address3 { get; set; }
        //    public string City { get; set; }
        //    public string State { get; set; }
        //    public string Postal_Code { get; set; }
        //    public string Country { get; set; }
        public string Formatted_Address { get; set; }
    }
}
