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
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public int TownId { get; set; }
        public string TownName { get; set; }
        public int UserId { get; set; }

        public AdvertisementItemDetailModel(int id, string title, string description, string phoneNumber, decimal price, DateTime createdDate, string? imageUrl, int categoryId, string categoryName, int? subCategoryId, string? subCategoryName, int townId, string townName, int userId)
        {
            Id = id;
            Title = title;
            Description = description;
            PhoneNumber = phoneNumber;
            Price = price;
            CreatedDate = createdDate;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
            CategoryName = categoryName;
            SubCategoryId = subCategoryId;
            SubCategoryName = subCategoryName;
            TownId = townId;
            TownName = townName;
            UserId=userId;
        }
    }
}
