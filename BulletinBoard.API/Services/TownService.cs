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
                return new BaseResponse<List<GetAllTownResponse>>(response);
            }
            catch (Exception)
            {
                return new BaseResponse<List<GetAllTownResponse>>(1, "Города не найдены", null);
            }
        }

        public BaseResponse AddTown(string nameTown)
        {
            try
            {
                using var db = new DataBaseContext();
                var exist = db.Towns?.FirstOrDefault(item => item.Name == nameTown);
                if (exist != null) return new BaseResponse(1, "Такой город уже существует");
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
            var isFreeName = db.Towns.FirstOrDefault(x => x.Name == newNameTown);
            if (isFreeName != null) return new BaseResponse(1, "Такой город уже существует");

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

            var advert = db.Advertisements.FirstOrDefault(x => x.TownId == existTown.Id);
            if (advert != null) return new BaseResponse(1, "К городу привязаны объявления. Для удаления необходимо удалить все привязаные объявления");

            db.Towns.Remove(existTown);
            db.SaveChanges();
            return new BaseResponse(0);
        }
    }
}
