namespace BulletinBoard.API.Models.Category
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? ParentId { get; set; }
    }
}
