namespace BulletinBoard.API.Models.Category
{
    public class RemoveCategoryRequest
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
    }
}
