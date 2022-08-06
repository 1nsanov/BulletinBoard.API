namespace BulletinBoard.API.EntityDB.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? ParentIdCategory { get; set; }

        public Category(string name, string? imageUrl, int? parentIdCategory)
        {
            Name = name;
            ImageUrl = imageUrl;
            ParentIdCategory = parentIdCategory;
        }

        public List<Advertisement> Advertisements { get; set; } = new();
    }
}
