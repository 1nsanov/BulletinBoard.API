namespace BulletinBoard.API.EntityDB.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }

        public Category(string name, string? imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }

        public List<SubCategory> SubCategories { get; set; } = new();
        public List<Advertisement> Advertisements { get; set; } = new();
    }
}
