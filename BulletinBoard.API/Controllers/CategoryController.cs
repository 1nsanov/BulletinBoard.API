using System.ComponentModel.DataAnnotations;
using BulletinBoard.API.ExtensionClasses;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Category;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class CategoryController : ControllerBase<CategoryService>
    {
        public CategoryController(CategoryService service) : base(service) { }

        /// <summary>
        /// Отправляет список категорий
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task GetAllCategory(HttpContext ctx)
        {
            try
            {
                var categories = _service.GetCategories();
                var subCategory = _service.GetSubCategories();
                var categoriesResult = new List<GetAllCategoryResponse>();
                categories.ForEach(parent =>
                {
                    var subs = subCategory.Where(item => item.CategoryId == parent.Id).ToList();
                    categoriesResult.Add(new GetAllCategoryResponse(parent.Id, parent.Name, parent.ImageUrl, subs.ConvertAll(x => new SubCategoryModel(x.Id, x.Name, x.ImageUrl))));
                });
                var response = new BaseResponse<List<GetAllCategoryResponse>>(categoriesResult);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Создание категории/подкатегории
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task CreateCategory(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<CreateCategoryRequest>();
                var response = new BaseResponse(0);
                if (!Identification.IsAdmin(ctx.Connection.Id)) response = new BaseResponse(1, "Только админ может редактировать категории.");
                else
                {
                    if (request.ParentId == null) _service.AddCategory(request.Name, request.ImageUrl);
                    else _service.AddCategory(request.Name, request.ImageUrl, (int)request.ParentId);
                }
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Редактирование категории/подкатегории
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task UpdateCategory(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<UpdateCategoryRequest>();
                var response = new BaseResponse(0);
                if (!Identification.IsAdmin(ctx.Connection.Id)) response = new BaseResponse(1, "Только админ может редактировать категории.");
                else
                {
                    var subCategory = _service.GetSubCategoryById(request.Id, request.ParentId);
                    if (subCategory == null)
                    {
                        var category = _service.GetCategoryById(request.Id);
                        if (category == null) response = new BaseResponse(1, "Такой категории не существует");
                        else _service.UpdateCategory(category, request.Name, request.ImageUrl);
                    }
                    else _service.UpdateCategory(subCategory, request.Name, request.ImageUrl);
                }
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Удаление категории/подкатегории
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task RemoveCategory(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<RemoveCategoryRequest>();
                var response = new BaseResponse(0);
                if (!Identification.IsAdmin(ctx.Connection.Id)) response = new BaseResponse(1, "Только админ может редактировать категории.");
                else
                {
                    var subCategory = _service.GetSubCategoryById(request.Id, request.ParentId);
                    if (subCategory == null)
                    {
                        var category = _service.GetCategoryById(request.Id);
                        if (category == null) response = new BaseResponse(1, "Такой категории не существует");
                        else if (_service.IsHaveSubCategory(category.Id))
                        {
                            response = new BaseResponse(1, "У категории есть подкатегории, удаление невозможно.");
                        }
                        else if (_service.IsHaveAdvertisement(category.Id, false))
                        {
                            response = new BaseResponse(1, "К категории привязаны объявления. Для удаления необходимо удалить все привязаные объявления");
                        }
                        else _service.RemoveCategory(category);

                    }
                    else
                    {
                        if (_service.IsHaveAdvertisement(subCategory.Id, true))
                        {
                            response = new BaseResponse(1,
                                "К категории привязаны объявления. Для удаления необходимо удалить все привязаные объявления");
                        }
                        else _service.RemoveCategory(subCategory);
                    }
                }
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
    }
}
