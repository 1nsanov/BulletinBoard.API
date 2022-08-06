namespace BulletinBoard.API.Models.Auth
{
    public class SingInRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public SingInRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
