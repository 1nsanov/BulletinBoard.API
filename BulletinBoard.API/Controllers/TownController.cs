using BulletinBoard.API.ExtensionClasses;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Town;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class TownController : ControllerBase<TownService>
    {
        public TownController(TownService service) : base(service) { }

        /// <summary>
        /// Отправляет список городов
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task GetAllTown(HttpContext ctx)
        {
            try
            {
                var townList = _service.GetTowns();
                var towns = townList.ConvertAll(x => new GetAllTownResponse(x.Id, x.Name));
                var response = new BaseResponse<List<GetAllTownResponse>>(towns);
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
                var response = new BaseResponse(0);

                if (!_service.IsExistTown(request.Name)) _service.AddTown(request.Name);
                else response = new BaseResponse(1, "Такой город уже существует");

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
                var response = new BaseResponse(0);
                if (_service.IsExistTown(request.Id))
                {
                    if (_service.IsExistTown(request.Name))
                    {
                        response = new BaseResponse(1, "Такой город уже существует");
                    }
                    else
                    {
                        _service.UpdateTown(request.Id, request.Name);
                    }
                }
                else response = new BaseResponse(1, "Город не найден");
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
                var response = new BaseResponse(0);
                if (_service.IsExistTown(request.Id))
                {
                    if (_service.IsCanRemoveTown(request.Id)) _service.RemoveTown(request.Id);
                    else response = new BaseResponse(1, "К городу привязаны объявления. Для удаления необходимо удалить все привязаные объявления"); ;
                }
                else response = new BaseResponse(1, "Город не найден");
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
    }
}
