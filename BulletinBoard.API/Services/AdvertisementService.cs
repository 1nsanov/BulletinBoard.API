using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;

namespace BulletinBoard.API.Services
{
    public class AdvertisementService
    {
        public List<Advertisement> GetAdvertisements()
        {
            try
            {
                using var db = new DataBaseContext();
                return db.Advertisements.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Advertisement GetAdvertisement(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                return db.Advertisements.FirstOrDefault(item => item.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetNameCategory(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                return db.Categories.FirstOrDefault(x => x.Id == id).Name;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetNameSubCategory(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                return db.SubCategories.FirstOrDefault(x => x.Id == id).Name;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetNameTown(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                return db.Towns.FirstOrDefault(x => x.Id == id).Name;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddAdvertisement(Advertisement advertisement)
        {
            try
            {
                using var db = new DataBaseContext();
                db.Advertisements.Add(advertisement);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateAdvertisement(Advertisement advertisement)
        {
            try
            {
                using var db = new DataBaseContext();
                db.Advertisements.Update(advertisement);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RemoveAdvertisement(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                var advert = db.Advertisements.FirstOrDefault(x => x.Id == id);
                db.Advertisements.Remove(advert);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
