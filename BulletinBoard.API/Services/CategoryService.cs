using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Category;

namespace BulletinBoard.API.Services
{
    public class CategoryService
    {
        public BaseResponse<List<GetAllCategoryResponse>> GetAllCategory()
        {
            try
            {
                using var db = new DataBaseContext();
                var categories = db.Categories.ToList();
                var response = new List<GetAllCategoryResponse>();
                categories.ForEach(parent =>
                {
                    var child = new List<SubCategoryModel>();
                    parent.SubCategories.ForEach(sub =>
                    {
                        child.Add(new SubCategoryModel(sub.Id, sub.Name, sub.ImageUrl));
                    });
                    response.Add(new GetAllCategoryResponse(parent.Id, parent.Name, parent.ImageUrl, child));
                });

                return new BaseResponse<List<GetAllCategoryResponse>>(response);
            }
            catch (Exception e)
            {
                return new BaseResponse<List<GetAllCategoryResponse>>(1, e.Message, null);
            }
        }

        public BaseResponse CreateCategory(CreateCategoryRequest request)
        {
            try
            {
                using var db = new DataBaseContext();
                if (request.ParentId == null)
                {
                    db.Categories.Add(new Category(request.Name, request.ImageUrl));
                }
                else
                {
                    db.SubCategories.Add(new SubCategory(request.Name, request.ImageUrl, (int)request.ParentId));
                }
                return new BaseResponse(0);
            }
            catch (Exception e)
            {
                return new BaseResponse(1, e.Message);
            }
        }

        public BaseResponse UpdateCategory(UpdateCategoryRequest request)
        {
            try
            {
                using var db = new DataBaseContext();
                var existSubCategory = db.SubCategories.FirstOrDefault(item => item.Id == request.Id && item.CategoryId == request.ParentId);

                if (existSubCategory == null)
                {
                    var existCategory = db.Categories.FirstOrDefault(item => item.Id == request.Id);
                    if (existCategory == null) return new BaseResponse(1, "Такой категории не существует");
                    existCategory.Name = request.Name;
                    existCategory.ImageUrl = request.ImageUrl;
                    db.Categories.Update(existCategory);
                    db.SaveChanges();
                }
                else
                {
                    existSubCategory.Name = request.Name;
                    existSubCategory.ImageUrl = request.ImageUrl;
                    db.SubCategories.Update(existSubCategory);
                    db.SaveChanges();
                }
                return new BaseResponse(0);
            }
            catch (Exception e)
            {
                return new BaseResponse(1, e.Message);
            }
        }

        public BaseResponse RemoveCategory(RemoveCategoryRequest request)
        {
            try
            {
                using var db = new DataBaseContext();
                var existSubCategory = db.SubCategories.FirstOrDefault(item => item.Id == request.Id && item.CategoryId == request.ParentId);

                if (existSubCategory == null)
                {
                    var existCategory = db.Categories.FirstOrDefault(item => item.Id == request.Id);
                    if (existCategory == null) return new BaseResponse(1, "Такой категории не существует");
                    db.Categories.Remove(existCategory);
                    db.SaveChanges();
                }
                else
                {
                    db.SubCategories.Remove(existSubCategory);
                    db.SaveChanges();
                }

                return new BaseResponse(0);
            }
            catch (Exception e)
            {
                return new BaseResponse(1, e.Message);
            }
        }
    }
}
