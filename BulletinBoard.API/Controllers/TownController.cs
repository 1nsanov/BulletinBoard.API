using BulletinBoard.API.ExtensionClasses;
using BulletinBoard.API.Models.Town;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class TownController : ControllerBase<TownService>
    {
        public TownController(TownService service) : base(service) {}

        /// <summary>
        /// Отправляет список городов
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task GetAllTown(HttpContext ctx)
        {
            try
            {
                var response = _service.GetAllTowns();
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Создание города
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task AddTown(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<AddTownRequest>();
                var response = _service.AddTown(request.Name);

                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Редактирование города
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task UpdateTown(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<UpdateTownRequest>();
                var response = _service.UpdateTown(request.Id, request.Name);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
        
        /// <summary>
        /// Удаление города
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task RemoveTown(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<RemoveTownRequest>();
                var response = _service.RemoveTown(request.Id);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

    }
}
