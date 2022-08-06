using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Interfaces;
using BulletinBoard.API.Models.Town;
using BulletinBoard.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.API.Controllers
{
    public class TownController : ControllerBase<TownService>
    {
        public TownController(TownService service) : base(service) {}

        public async Task GetAllTown(HttpContext ctx)
        {
            try
            {
                var towns = _service.GetAllTowns();
                var response = towns != null
                    ? new BaseResponse<List<GetAllTownResponse>>(towns)
                    : new BaseResponse<List<GetAllTownResponse>>(1, "Города не найдены", towns);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SendError(ctx, e);
            }
        }

        public async Task AddTown(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<AddTownRequest>();
                var isSuccess = _service.AddTown(request.Name);

                var response = isSuccess
                    ? new BaseResponse(0, "Город успешно добавлен")
                    : new BaseResponse(1, "Произошла неизвестная ошибка");
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SendError(ctx, e);
            }
        }

        public async Task UpdateTown(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<UpdateTownRequest>();
                var isSuccess = _service.UpdateTown(request.Id, request.Name);

                var response = isSuccess
                    ? new BaseResponse(0, "Город успешно обновлен")
                    : new BaseResponse(1, "Город не найден");
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SendError(ctx, e);
            }
        }

        public async Task RemoveTown(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<RemoveTownRequest>();
                var isSuccess = _service.RemoveTown(request.Id);

                var response = isSuccess
                    ? new BaseResponse(0, "Город успешно удален")
                    : new BaseResponse(1, "Город не найден");
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SendError(ctx, e);
            }

        }

        private static async Task SendError(HttpContext ctx, Exception e)
        {
            ctx.Response.StatusCode = 500;
            await ctx.Response.WriteAsJsonAsync(new BaseResponse(1, e.Message));
        }
    }
}
