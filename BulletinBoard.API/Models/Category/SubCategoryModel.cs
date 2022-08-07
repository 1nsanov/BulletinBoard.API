namespace BulletinBoard.API.Models.Category
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }

        public SubCategoryModel(int id, string name, string? imageUrl)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}
