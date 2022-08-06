namespace BulletinBoard.API.Models.Auth
{
    public class SingUpRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public SingUpRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
