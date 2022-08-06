using BulletinBoard.API.Models.Interfaces;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class AuthController : ControllerBase<AuthService>
    {
        public AuthController(AuthService service) : base(service) {}
    }
}
