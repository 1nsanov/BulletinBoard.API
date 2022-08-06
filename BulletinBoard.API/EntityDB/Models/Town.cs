namespace BulletinBoard.API.EntityDB.Models
{
    public class Town
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Town(string name)
        {
            Name = name;
        }
    }
}
