namespace BulletinBoard.API.Models.Category
{
    public class GetAllCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public List<SubCategoryModel>? SubCategories { get; set; }

        public GetAllCategoryResponse(int id, string name, string? imageUrl, List<SubCategoryModel>? subCategories)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            SubCategories = subCategories;
        }
    }
}
