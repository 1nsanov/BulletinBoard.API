namespace BulletinBoard.API.EntityDB.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }

        public SubCategory(string name, string? imageUrl, int categoryId)
        {
            Name = name;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
        }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Advertisement> Advertisements { get; set; } = new();
    }
}
