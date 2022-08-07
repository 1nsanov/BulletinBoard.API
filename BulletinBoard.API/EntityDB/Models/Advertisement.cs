namespace BulletinBoard.API.EntityDB.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImageUrl { get; set; }

        public Advertisement(string title, string description, string phoneNumber, decimal price, DateTime createdDate, string? imageUrl, int userId, int categoryId, int townId, int subCategoryId)
        {
            Title = title;
            Description = description;
            PhoneNumber = phoneNumber;
            Price = price;
            CreatedDate = createdDate;
            ImageUrl = imageUrl;
            UserId = userId;
            CategoryId = categoryId;
            TownId = townId;
            SubCategoryId = subCategoryId;
        }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }

        public int TownId { get; set; }
        public Town? Town { get; set; }
    }
}
