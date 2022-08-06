using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Town;

namespace BulletinBoard.API.Services
{
    public class TownService
    {
        /// <summary>
        /// Получает список всех городов
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public BaseResponse<List<GetAllTownResponse>> GetAllTowns()
        {
            try
            {
                using var db = new DataBaseContext();
                var towns = db.Towns?.ToList();
                var response = new List<GetAllTownResponse>();
                towns.ForEach(item => response.Add(new GetAllTownResponse(item.Id, item.Name)));
                return towns != null
                    ? new BaseResponse<List<GetAllTownResponse>>(response)
                    : new BaseResponse<List<GetAllTownResponse>>(1, "Города не найдены", response);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public BaseResponse AddTown(string nameTown)
        {
            try
            {
                using var db = new DataBaseContext();
                var newTown = new Town(nameTown);
                db.Towns.Add(newTown);
                db.SaveChanges();
                return new BaseResponse(0);
            }
            catch (Exception)
            {
                return new BaseResponse(1);
            }

        }

        public BaseResponse UpdateTown(int id, string newNameTown)
        {
            using var db = new DataBaseContext();
            var existTown = db.Towns.FirstOrDefault(x => x.Id == id);
            if (existTown == null) return new BaseResponse(1, "Город не найден");
            existTown.Name = newNameTown;
            db.Towns.Update(existTown);
            db.SaveChanges();
            return new BaseResponse(0);
        }

        public BaseResponse RemoveTown(int id)
        {
            using var db = new DataBaseContext();
            var existTown = db.Towns.FirstOrDefault(x => x.Id == id);
            if (existTown == null) return new BaseResponse(1, "Город не найден");
            db.Towns.Remove(existTown);
            db.SaveChanges();
            return new BaseResponse(0);
        }
    }
}
