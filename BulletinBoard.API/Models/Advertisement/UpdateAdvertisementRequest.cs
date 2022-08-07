namespace BulletinBoard.API.Models.Advertisement
{
    public class UpdateAdvertisementRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int TownId { get; set; }
    }
}
