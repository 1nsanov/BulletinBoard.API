using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Category;

namespace BulletinBoard.API.Services
{
    public class CategoryService
    {
        public List<Category> GetCategories()
        {
            try
            {
                using var db = new DataBaseContext();
                return db.Categories.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<SubCategory> GetSubCategories()
        {
            try
            {
                using var db = new DataBaseContext();
                return db.SubCategories.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddCategory(string name, string imgUrl)
        {
            try
            {
                using var db = new DataBaseContext();
                db.Categories.Add(new Category(name, imgUrl));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddCategory(string name, string imgUrl, int parentId)
        {
            try
            {
                using var db = new DataBaseContext();
                db.SubCategories.Add(new SubCategory(name, imgUrl, parentId));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Category? GetCategoryById(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                return db.Categories.FirstOrDefault(item => item.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public SubCategory? GetSubCategoryById(int id, int? parentId)
        {
            try
            {
                using var db = new DataBaseContext();
                return db.SubCategories.FirstOrDefault(item => item.Id == id && item.CategoryId == parentId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateCategory(Category currentCategory, string newName, string newImgUrl)
        {
            try
            {
                using var db = new DataBaseContext();
                currentCategory.Name = newName;
                currentCategory.ImageUrl = newImgUrl;
                db.Categories.Update(currentCategory);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateCategory(SubCategory currentSubCategory, string newName, string newImgUrl)
        {
            try
            {
                using var db = new DataBaseContext();
                currentSubCategory.Name = newName;
                currentSubCategory.ImageUrl = newImgUrl;
                db.SubCategories.Update(currentSubCategory);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool IsHaveSubCategory(int id)
        {
            try
            {
                using var db = new DataBaseContext();
                var item = db.SubCategories.FirstOrDefault(item => item.CategoryId == id);
                return item != null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool IsHaveAdvertisement(int id, bool isSubCategory)
        {
            try
            {
                using var db = new DataBaseContext();
                var item = db.Advertisements.FirstOrDefault(x => isSubCategory ? x.SubCategoryId  == id : x.CategoryId == id);
                return item != null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RemoveCategory(Category category)
        {
            try
            {
                using var db = new DataBaseContext();
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RemoveCategory(SubCategory subCategory)
        {
            try
            {
                using var db = new DataBaseContext();
                db.SubCategories.Remove(subCategory);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
