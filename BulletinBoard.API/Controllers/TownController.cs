namespace BulletinBoard.API.Controllers
{
    public class TownController
    {
        public async Task GetAllTown(HttpContext ctx)
        {
            await ctx.Response.WriteAsJsonAsync(new { message = "Вызов GetAllTown" });
        }

        public async Task AddTown(HttpContext ctx)
        {
            await ctx.Response.WriteAsJsonAsync(new { message = "Вызов AddTown" });
        }
    }
}
