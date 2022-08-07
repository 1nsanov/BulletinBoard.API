using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;
using BulletinBoard.API.Models;
using BulletinBoard.API.Models.Advertisement;

namespace BulletinBoard.API.Services
{
    public class AdvertisementService
    {
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
                    .OrderBy(x => x.CreatedDate)
                    .ToList()
                    .ConvertAll(ConvertAdvertisementListItem);

                return new BaseResponse<List<AdvertisementListItemModel>>(response);
            }
            catch (Exception e)
            {
                return new BaseResponse<List<AdvertisementListItemModel>>(1, e.Message, null);
            }
        }

        public BaseResponse<AdvertisementItemDetailModel> GetAdvertisementDetail(GetAdvertisementDetailRequest request)
        {
            try
            {
                using var db = new DataBaseContext();
                var exist = db.Advertisements.FirstOrDefault(item => item.Id == request.Id);
                if (exist == null) new BaseResponse<AdvertisementItemDetailModel>(1, "Объявление не найдено", null);

                var response = ConvertAdvertisementDetailItem(exist);
                return new BaseResponse<AdvertisementItemDetailModel>(response);
            }
            catch (Exception e)
            {
                return new BaseResponse<AdvertisementItemDetailModel>(1, e.Message, null);
            }
        }

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

        private static AdvertisementItemDetailModel ConvertAdvertisementDetailItem(Advertisement item)
        {
            return new AdvertisementItemDetailModel(item.Id, item.Title, item.Description, item.PhoneNumber, item.Price, item.CreatedDate,
                item.ImageUrl);
        }

        private static AdvertisementListItemModel ConvertAdvertisementListItem(Advertisement item)
        {
            return new AdvertisementListItemModel(item.Id, item.Title, item.ImageUrl, item.Price);
        }
    }
}
