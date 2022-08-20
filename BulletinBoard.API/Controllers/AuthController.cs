using BulletinBoard.API.ExtensionClasses;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Auth;
using BulletinBoard.API.Models.Enum;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class AuthController : ControllerBase<AuthService>
    {
        public AuthController(AuthService service) : base(service) { }

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
                var response = new BaseResponse(1, "Пользователь с таким логином уже существует");
                var existUser = _service.GetUserByUserName(request.UserName);
                if (existUser == null)
                {
                    var validMsg = Validation(request.UserName, request.Password);
                    if (validMsg.Length == 0)
                    {
                        _service.AddNewUser(request.UserName, request.Password);
                        response = new BaseResponse(0);
                    }
                    else response = new BaseResponse(1, validMsg);

                    await ctx.Response.WriteAsJsonAsync(response);
                }
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
                var response = new BaseResponse<UserModel>(1, "Пользователя с таким логином не существует");
                var existUser = _service.GetUserByUserName(request.UserName);
                if (existUser != null)
                {
                    if (existUser.Password == request.Password)
                    {
                        response = new BaseResponse<UserModel>(new UserModel(existUser.Id, existUser.UserName,
                            existUser.Password, existUser.UserRole));
                        if(existUser.UserRole == EnumUserRole.Admin) Identification.AddIdConnect(ctx.Connection.Id);
                    }
                    else
                    {
                        response = new BaseResponse<UserModel>(1, "Не верный пароль, повторите попытку");
                    }
                }
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
                var response = new BaseResponse(0);
                var isExistUser = _service.CheckExistUser(request.UserName);
                if (!isExistUser)
                {
                    response = new BaseResponse(1, "Пользователя с таким логином не существует");
                }
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
                var response = new BaseResponse(0);
                var existUser = _service.GetUserByUserName(request.UserName);
                if (existUser.Password == request.Password)
                {
                    response = new BaseResponse(1, "Старый и новый пароли совпадают");
                }
                else
                {
                    var validMsg = Validation(existUser.UserName, existUser.Password);
                    if (validMsg.Length != 0) response = new BaseResponse(1, validMsg);
                    else _service.UpdatePassword(existUser.Id, request.Password);
                }
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        private string? Validation(string userName, string password)
        {
            if (userName.Length <= 2) return "Логин должен быть больше двух символов";
            if (password.Length <= 3) return "Пароль должен быть не меньше четырех символов";
            return string.Empty;
        }
    }
}
