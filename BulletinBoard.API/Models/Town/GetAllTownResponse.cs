namespace BulletinBoard.API.Models.Town
{
    public class GetAllTownResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GetAllTownResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
