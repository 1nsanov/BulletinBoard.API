namespace BulletinBoard.API.Models.Advertisement
{
    public class GetAdvertisementListRequest
    {
        public int? TownId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? UserId { get; set; }
    }
}
