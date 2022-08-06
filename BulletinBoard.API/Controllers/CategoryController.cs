using BulletinBoard.API.Models.Interfaces;
using BulletinBoard.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.API.Controllers
{
    public class CategoryController : ControllerBase<CategoryService>
    {
        public CategoryController(CategoryService service) : base(service){}
    }
}
