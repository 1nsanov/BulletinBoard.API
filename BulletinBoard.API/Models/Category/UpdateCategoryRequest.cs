namespace BulletinBoard.API.Models.Category
{
    public class UpdateCategoryRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? ParentId { get; set; }
    }
}
