using BulletinBoard.API.Models;

namespace BulletinBoard.API.ExtensionClasses
{
    public static class SenderError
    {
        public static async Task Error500(HttpContext ctx, Exception e)
        {
            ctx.Response.StatusCode = 500;
            await ctx.Response.WriteAsJsonAsync(new BaseResponse(1, e.Message));
        }
    }
}
