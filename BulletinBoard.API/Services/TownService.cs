using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;

namespace BulletinBoard.API.Services
{
    public class TownService
    {
        public List<Town> GetTowns()
        {
            try
            {
                using var db = new DataBaseContext();
                return db.Towns?.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Town GetTown(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                return db.Towns.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool IsExistTown(string name)
        {
            try
            {
                using var db = new DataBaseContext();
                var town = db.Towns.FirstOrDefault(x => x.Name == name);
                return town != null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool IsExistTown(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                var town = db.Towns.FirstOrDefault(x => x.Id == id);
                return town != null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddTown(string name)
        {
            try
            {
                using var db = new DataBaseContext();
                db.Towns.Add(new Town(name));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateTown(int id, string name)
        {
            try
            {
                var town = GetTown(id);
                using var db = new DataBaseContext();
                town.Name = name;
                db.Towns.Update(town);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RemoveTown(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                var town = GetTown(id);
                db.Towns.Remove(town);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool IsCanRemoveTown(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                var advert = db.Advertisements.FirstOrDefault(x => x.TownId == id);
                return advert == null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
