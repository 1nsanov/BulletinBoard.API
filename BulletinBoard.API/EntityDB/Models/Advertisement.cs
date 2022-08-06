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

        public Advertisement(string title, string description, string phoneNumber, decimal price, DateTime createdDate, string? imageUrl)
        {
            Title = title;
            Description = description;
            PhoneNumber = phoneNumber;
            Price = price;
            CreatedDate = createdDate;
            ImageUrl = imageUrl;
        }

        public int UserId { get; set; }
        public User? User { get; set; }

        public List<Category> Categories { get; set; } = new();
        public List<Town> Towns { get; set; } = new();
    }
}
