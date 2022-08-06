using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;
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
        public List<GetAllTownResponse>? GetAllTowns()
        {
            try
            {
                using var db = new DataBaseContext();
                var towns = db.Towns?.ToList();
                var response = new List<GetAllTownResponse>();
                towns.ForEach(item => response.Add(new GetAllTownResponse(item.Id, item.Name)));
                return response;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool AddTown(string nameTown)
        {
            try
            {
                using var db = new DataBaseContext();
                var newTown = new Town(nameTown);
                db.Towns.Add(newTown);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateTown(int id, string newNameTown)
        {
            using var db = new DataBaseContext();
            var existTown = db.Towns.FirstOrDefault(x => x.Id == id);
            if (existTown == null) return false;
            existTown.Name = newNameTown;
            db.Towns.Update(existTown);
            db.SaveChanges();
            return true;
        }

        public bool RemoveTown(int id)
        {
            using var db = new DataBaseContext();
            var existTown = db.Towns.FirstOrDefault(x => x.Id == id);
            if (existTown == null) return false;
            db.Towns.Remove(existTown);
            db.SaveChanges();
            return true;
        }
    }
}
