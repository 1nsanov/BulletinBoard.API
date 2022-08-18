using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;

namespace BulletinBoard.API.Services
{
    public class AuthService
    {
        public User? GetUserByUserName(string username)
        {
            try
            {
                using var db = new DataBaseContext();
                return db.Users.FirstOrDefault(user => user.UserName == username);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddNewUser(string username, string password)
        {
            try
            {
                using var db = new DataBaseContext();
                db.Users.Add(new User(username, password, 0));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool CheckExistUser(string username)
        {
            try
            {
                using var db = new DataBaseContext();
                var existUser = db.Users.FirstOrDefault(user => user.UserName == username);
                return existUser != null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdatePassword(int id, string newPassword)
        {
            try
            {
                using var db = new DataBaseContext();
                var existUser = db.Users.FirstOrDefault(user => user.Id == id);
                existUser.Password = newPassword;
                db.Users.Update(existUser);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
