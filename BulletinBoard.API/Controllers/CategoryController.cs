using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class CategoryController : ControllerBase<CategoryService>
    {
        public CategoryController(CategoryService service) : base(service){}
    }
}
