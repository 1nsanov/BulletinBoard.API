using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Advertisement;

namespace BulletinBoard.API.Services
{
    public class AdvertisementService
    {
        /// <summary>
        /// Получает список объявлений по фильтру (request)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse<List<AdvertisementListItemModel>> GetAdvertisementList(GetAdvertisementListRequest request)
        {
            try
            {
                using var db = new DataBaseContext();
                var listAdvertisement = db.Advertisements.ToList();

                if (request.UserId != null)
                    listAdvertisement = listAdvertisement.Where(item => item.UserId == request.UserId).ToList();

                if (request.TownId != null)
                    listAdvertisement = listAdvertisement.Where(item => item.TownId == request.TownId).ToList();

                if (request.CategoryId != null)
                    listAdvertisement = listAdvertisement.Where(item => item.CategoryId == request.CategoryId).ToList();

                if (request.SubCategoryId != null)
                    listAdvertisement = listAdvertisement.Where(item => item.SubCategoryId == request.SubCategoryId).ToList();

                var response = listAdvertisement
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList()
                    .ConvertAll(ConvertAdvertisementListItem);

                return new BaseResponse<List<AdvertisementListItemModel>>(response);
            }
            catch (Exception e)
            {
                return new BaseResponse<List<AdvertisementListItemModel>>(1, e.Message, null);
            }
        }
        
        /// <summary>
        /// Получает детальную информацию о объявлении по id 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse<AdvertisementItemDetailModel> GetAdvertisementDetail(GetAdvertisementDetailRequest request)
        {
            try
            {
                using var db = new DataBaseContext();
                var exist = db.Advertisements.FirstOrDefault(item => item.Id == request.Id);
                if (exist == null) return new BaseResponse<AdvertisementItemDetailModel>(1, "Объявление не найдено", null);

                var response = ConvertAdvertisementDetailItem(exist);
                response.CategoryName = db.Categories.FirstOrDefault(x => x.Id == response.CategoryId).Name;
                response.TownName = db.Towns.FirstOrDefault(x => x.Id == response.TownId).Name;
                if (response.SubCategoryId != null)
                {
                    response.SubCategoryName =
                        db.SubCategories.FirstOrDefault(x => x.Id == response.SubCategoryId).Name;
                }
                return new BaseResponse<AdvertisementItemDetailModel>(response);
            }
            catch (Exception e)
            {
                return new BaseResponse<AdvertisementItemDetailModel>(1, e.Message);
            }
        }

        /// <summary>
        /// Создает объявление
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse CreateAdvertisement(CreateAdvertisementRequest request)
        {
            try
            {
                using var db = new DataBaseContext();
                var dateNow = DateTime.Now;
                var newAdvertisement = new Advertisement(request.Title, request.Description, request.PhoneNumber,
                    request.Price, dateNow, request.ImageUrl, request.UserId, request.CategoryId, request.TownId,
                    request.SubCategoryId);

                db.Advertisements.Add(newAdvertisement);
                db.SaveChanges();
                return new BaseResponse(0);
            }
            catch (Exception e)
            {
                return new BaseResponse(1, e.Message);
            }
        }

        /// <summary>
        /// Редактирует объявление
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse UpdateAdvertisement(UpdateAdvertisementRequest request)
        {
            try
            {
                using var db = new DataBaseContext();
                var exist = db.Advertisements.FirstOrDefault(item => item.Id == request.Id);
                if (exist == null) return new BaseResponse(1, "Объявление не найдено");

                exist.Title = request.Title;
                exist.Description = request.Description;
                exist.PhoneNumber = request.PhoneNumber;
                exist.ImageUrl = request.ImageUrl;
                exist.Price = request.Price;
                exist.TownId = request.TownId;
                exist.CategoryId = request.CategoryId;
                exist.SubCategoryId = request.SubCategoryId;

                db.Advertisements.Update(exist);
                db.SaveChanges();
                return new BaseResponse(0);
            }
            catch (Exception e)
            {
                return new BaseResponse(1, e.Message);
            }
        }

        /// <summary>
        /// Удаляет объявление
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse RemoveAdvertisement(RemoveAdvertisementRequest request)
        {
            try
            {
                using var db = new DataBaseContext();
                var exist = db.Advertisements.FirstOrDefault(item => item.Id == request.Id);
                if (exist == null) return new BaseResponse(1, "Объявление не найдено");

                db.Advertisements.Remove(exist);
                db.SaveChanges();
                return new BaseResponse(0);
            }
            catch (Exception e)
            {
                return new BaseResponse(1, e.Message);
            }
        }

        /// <summary>
        /// Конвертер в детальную информацию объявления из БД
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static AdvertisementItemDetailModel ConvertAdvertisementDetailItem(Advertisement item)
        {
            return new AdvertisementItemDetailModel(item.Id, item.Title, item.Description, item.PhoneNumber, item.Price, item.CreatedDate,
                item.ImageUrl, item.CategoryId, null, item.SubCategoryId, null, item.TownId, null, item.UserId);
        }

        /// <summary>
        /// Конвертер в айтем списка объявления из БД
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static AdvertisementListItemModel ConvertAdvertisementListItem(Advertisement item)
        {
            return new AdvertisementListItemModel(item.Id, item.Title, item.ImageUrl, item.Price);
        }
    }
}
