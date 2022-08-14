using BulletinBoard.API.EntityDB;
using BulletinBoard.API.ExtensionClasses;
using BulletinBoard.API.Models.Auth;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class AuthController : ControllerBase<AuthService>
    {
        public AuthController(AuthService service) : base(service){}

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task SingUp(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<SingUpRequest>();
                var response = _service.SingUp(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task SingIn(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<SingInRequest>();
                var response = _service.SingIn(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Проверка на существования пользователя
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task CheckExistUser(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<CheckExistUserRequest>();
                var response = _service.CheckExistUser(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Восстановление пароля
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task RecoveryPassword(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<RecoveryPasswordRequest>();
                var response = _service.RecoveryPassword(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
    }
}
