using BulletinBoard.API.EntityDB;
using BulletinBoard.API.EntityDB.Models;
using BulletinBoard.API.Models;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class TownController
    {
        private TownService _townService;

        public TownController()
        {
            _townService = new TownService();
        }

        public async Task GetAllTown(HttpContext ctx)
        {
            var towns = _townService.GetAllTowns();
            var response = towns != null 
                ? new BaseResponse<List<Town>>(towns) 
                : new BaseResponse<List<Town>>(1, "Города не найдены", towns);
            await ctx.Response.WriteAsJsonAsync(response);
        }

        public async Task AddTown(HttpContext ctx)
        {
            //await using (var db = new DataBaseContext())
            //{
            //    var newTown = new Town("Тирасполь");
            //    db.Towns?.Add(newTown);
            //    await db.SaveChangesAsync();
            //}
            await ctx.Response.WriteAsJsonAsync(new { message = "Город добавлен" });
        }
    }
}
