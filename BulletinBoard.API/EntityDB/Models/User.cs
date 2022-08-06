using BulletinBoard.API.Models.Enum;

namespace BulletinBoard.API.EntityDB.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public EnumUserRole UserRole { get; set; }

        public User(string userName, string password, EnumUserRole userRole)
        {
            UserName = userName;
            Password = password;
            UserRole = userRole;
        }

        public List<Advertisement> Advertisements { get; set; } = new();
    }
}
