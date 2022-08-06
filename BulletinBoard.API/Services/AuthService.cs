using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Auth;

namespace BulletinBoard.API.Services
{
    public class AuthService
    {
        public BaseResponse SingUp(SingUpRequest request)
        {
            using var db = new DataBaseContext();
            {
                var existUser = db.Users.FirstOrDefault(user => user.UserName == request.UserName);
                if (existUser != null) return new BaseResponse(1, "Пользователь с таким логином уже существует");

                var valid = Validation(request.UserName, request.Password);
                if (!valid.IsSuccess) return valid;

                db.Users.Add(new User(request.UserName, request.Password, 0));
                db.SaveChanges();
                return new BaseResponse(0);
            }
        }

        public BaseResponse SingIn(SingInRequest request)
        {
            using var db = new DataBaseContext();
            {
                var existUser = db.Users.FirstOrDefault(user => user.UserName == request.UserName);
                if (existUser == null) return new BaseResponse(1, "Пользователя с таким логином не существует");
                if (existUser.Password != request.Password) return new BaseResponse(1, "Не верный пароль, повторите попытку");

                return new BaseResponse(0);
            }
        }

        public BaseResponse CheckExistUser(CheckExistUserRequest request)
        {
            using var db = new DataBaseContext();
            var existUser = db.Users.FirstOrDefault(user => user.UserName == request.UserName);

            return existUser != null
                ? new BaseResponse(0)
                : new BaseResponse(1, "Пользователя с таким логином не существует");
        }

        public BaseResponse RecoveryPassword(RecoveryPasswordRequest request)
        {
            using var db = new DataBaseContext();
            {
                var existUser = db.Users.FirstOrDefault(user => user.UserName == request.UserName);
                if (existUser.Password == request.Password) return new BaseResponse(1, "Старый и новый пароли совпадают");
                var valid = Validation(existUser.UserName, request.Password);
                if (!valid.IsSuccess) return valid;

                existUser.Password = request.Password;
                db.Users.Update(existUser);
                db.SaveChanges();
                return new BaseResponse(0);
            }
        }

        private BaseResponse Validation(string userName, string password)
        {
            if (userName.Length <= 2) return new BaseResponse(1, "Логин должен быть больше двух символов");
            if (password.Length <= 3) return new BaseResponse(1, "Пароль должен быть не меньше четырех символов");
            return new BaseResponse(0);
        }
    }
}
