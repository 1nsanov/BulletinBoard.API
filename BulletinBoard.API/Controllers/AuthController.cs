using BulletinBoard.API.EntityDB;
using BulletinBoard.API.ExtensionClasses;
using BulletinBoard.API.Models.Auth;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class AuthController : ControllerBase<AuthService>
    {
        public AuthController(AuthService service) : base(service) {}

        public async Task SingUp(HttpContext ctx)
        {
            try
            {
                var request = ctx.Request.ReadFromJsonAsync<SingUpRequest>();
                
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        public async Task SingIn(HttpContext ctx)
        {

        }

        public async Task RecoveryPassword(HttpContext ctx)
        {

        }
    }
}
