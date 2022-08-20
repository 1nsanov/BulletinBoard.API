using BulletinBoard.API.Models.Enum;

namespace BulletinBoard.API.Models.Auth
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public EnumUserRole UserRole { get; set; }

        public UserModel(int id, string userName, string password, EnumUserRole userRole)
        {
            Id = id;
            UserName = userName;
            Password = password;
            UserRole = userRole;
        }
    }
}
