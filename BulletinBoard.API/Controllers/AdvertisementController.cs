using BulletinBoard.API.ExtensionClasses;
using BulletinBoard.API.Models.Advertisement;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class AdvertisementController: ControllerBase<AdvertisementService>
    {
        public AdvertisementController(AdvertisementService service) : base(service) {}

        public async Task GetAdvertisementList(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<GetAdvertisementListRequest>();
                var response = _service.GetAdvertisementList(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
        public async Task GetAdvertisementDetail(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<GetAdvertisementDetailRequest>();
                var response = _service.GetAdvertisementDetail(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
        public async Task CreateAdvertisement(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<CreateAdvertisementRequest>();
                var response = _service.CreateAdvertisement(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
        public async Task UpdateAdvertisement(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<UpdateAdvertisementRequest>();
                var response = _service.UpdateAdvertisement(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
        public async Task RemoveAdvertisement(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<RemoveAdvertisementRequest>();
                var response = _service.RemoveAdvertisement(request);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }
    }
}
