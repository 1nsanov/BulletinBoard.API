using BulletinBoard.API.ExtensionClasses;
using BulletinBoard.API.Models.Category;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class CategoryController : ControllerBase<CategoryService>
    {
        public CategoryController(CategoryService service) : base(service){}

        public async Task GetAllCategory(HttpContext ctx)
        {
            try
            {
                var response = _service.GetAllCategory();

                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        public async Task CreateCategory(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<CreateCategoryRequest>();
                var response = _service.CreateCategory(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        public async Task UpdateCategory(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<UpdateCategoryRequest>();
                var response = _service.UpdateCategory(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        public async Task RemoveCategory(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<RemoveCategoryRequest>();
                var response = _service.RemoveCategory(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
    }
}
