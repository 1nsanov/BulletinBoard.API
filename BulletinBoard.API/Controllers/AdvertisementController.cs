using BulletinBoard.API.EntityDB.Models;
using BulletinBoard.API.ExtensionClasses;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Advertisement;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class AdvertisementController : ControllerBase<AdvertisementService>
    {
        public AdvertisementController(AdvertisementService service) : base(service) { }

        /// <summary>
        /// Отправляет список объявлений
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task GetAdvertisementList(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<GetAdvertisementListRequest>();
                var adverts = _service.GetAdvertisements();

                if (request.UserId != null)
                    adverts = adverts.Where(item => item.UserId == request.UserId).ToList();

                if (request.TownId != null)
                    adverts = adverts.Where(item => item.TownId == request.TownId).ToList();

                if (request.CategoryId != null)
                    adverts = adverts.Where(item => item.CategoryId == request.CategoryId).ToList();

                if (request.SubCategoryId != null)
                    adverts = adverts.Where(item => item.SubCategoryId == request.SubCategoryId).ToList();

                var result = adverts
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList()
                    .ConvertAll(ConvertAdvertisementListItem);

                var response = new BaseResponse<List<AdvertisementListItemModel>>(result);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Отправляет детальную информацию объявления
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task GetAdvertisementDetail(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<GetAdvertisementDetailRequest>();
                BaseResponse<AdvertisementItemDetailModel> response;
                var advert = _service.GetAdvertisement(request.Id);
                if (advert == null) response = new BaseResponse<AdvertisementItemDetailModel>(1, "Объявление не найдено");
                else
                {
                    var result = ConvertAdvertisementDetailItem(advert);
                    result.TownName = _service.GetNameTown(result.TownId);
                    result.CategoryName = _service.GetNameCategory(result.CategoryId);
                    if (result.SubCategoryId != null) _service.GetNameSubCategory((int)result.SubCategoryId);
                    response = new BaseResponse<AdvertisementItemDetailModel>(result);
                }
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Создание объявления
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task CreateAdvertisement(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<CreateAdvertisementRequest>();
                var dateNow = DateTime.Now;
                var advert = new Advertisement(request.Title, request.Description, request.PhoneNumber,
                    request.Price, dateNow, request.ImageUrl, request.UserId, request.CategoryId, request.TownId,
                    request.SubCategoryId);
                _service.AddAdvertisement(advert);
                var response = new BaseResponse(0);
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Редактирование объявления
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task UpdateAdvertisement(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<UpdateAdvertisementRequest>();
                var response = new BaseResponse(0);
                var advert = _service.GetAdvertisement(request.Id);
                if (advert == null) response =new BaseResponse(1, "Объявление не найдено");
                else
                {
                    advert.Title = request.Title;
                    advert.Description = request.Description;
                    advert.PhoneNumber = request.PhoneNumber;
                    advert.ImageUrl = request.ImageUrl;
                    advert.Price = request.Price;
                    advert.TownId = request.TownId;
                    advert.CategoryId = request.CategoryId;
                    advert.SubCategoryId = request.SubCategoryId;
                    _service.UpdateAdvertisement(advert);
                }
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }

        /// <summary>
        /// Удалениие объявления
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task RemoveAdvertisement(HttpContext ctx)
        {
            try
            {
                var request = await ctx.Request.ReadFromJsonAsync<RemoveAdvertisementRequest>();
                var response = new BaseResponse(0);
                var advert = _service.GetAdvertisement(request.Id);
                if (advert != null) _service.RemoveAdvertisement(advert.Id);
                else response = new BaseResponse(1, "Объявление не найдено");
                await ctx.Response.WriteAsJsonAsync(response);
            }
            catch (Exception e)
            {
                await SenderError.Error500(ctx, e);
            }
        }


        /// <summary>
        /// Конвертер в детальную информацию объявления из модели БД
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static AdvertisementItemDetailModel ConvertAdvertisementDetailItem(Advertisement item)
        {
            return new AdvertisementItemDetailModel(item.Id, item.Title, item.Description, item.PhoneNumber, item.Price, item.CreatedDate,
                item.ImageUrl, item.CategoryId, null, item.SubCategoryId, null, item.TownId, null, item.UserId);
        }

        /// <summary>
        /// Конвертер в айтем списка объявления из модели БД
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static AdvertisementListItemModel ConvertAdvertisementListItem(Advertisement item)
        {
            return new AdvertisementListItemModel(item.Id, item.Title, item.ImageUrl, item.Price);
        }
    }
}
