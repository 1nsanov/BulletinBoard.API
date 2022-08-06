using BulletinBoard.API.Models.Interfaces;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class AdvertisementController: ControllerBase<AdvertisementService>
    {
        public AdvertisementController(AdvertisementService service) : base(service) {}
    }
}
