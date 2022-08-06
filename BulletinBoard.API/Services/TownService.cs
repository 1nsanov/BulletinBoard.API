using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;

namespace BulletinBoard.API.Services
{
    public class TownService
    {
        /// <summary>
        /// Получает список всех городов
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public List<Town> GetAllTowns()
        {
            using var db = new DataBaseContext();
            var listTown = db.Towns?.ToList();
            return listTown;
        }
    }
}
