namespace BulletinBoard.API.Models.Advertisement
{
    public class AdvertisementItemDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImageUrl { get; set; }

        public AdvertisementItemDetailModel(int id, string title, string description, string phoneNumber, decimal price, DateTime createdDate, string? imageUrl)
        {
            Id = id;
            Title = title;
            Description = description;
            PhoneNumber = phoneNumber;
            Price = price;
            CreatedDate = createdDate;
            ImageUrl = imageUrl;
        }
    }
}
