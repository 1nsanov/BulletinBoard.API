using System.Reflection;
using BulletinBoard.API.Models;
using BulletinBoard.API.Services;

namespace BulletinBoard.API.Controllers
{
    public class AppController
    {
        private const string TownUrl = "town";
        private const string CategoryUrl = "category";
        private const string AdvertisementUrl = "advertisement";
        private const string AuthUrl = "auth";

        private readonly TownController TownController;
        private readonly CategoryController CategoryController;
        private readonly AdvertisementController AdvertisementController;
        private readonly AuthController AuthController;

        private readonly DataSourceMethodsService DataSourceMethods;

        public AppController()
        {
            TownController = new TownController(new TownService());
            CategoryController = new CategoryController(new CategoryService());
            AdvertisementController = new AdvertisementController(new AdvertisementService());
            AuthController = new AuthController(new AuthService());
            DataSourceMethods = new DataSourceMethodsService();
        }

        public async Task Run(HttpContext ctx)
        {
            var path = ctx.Request.Path;
            var nameService = GetNameFromPath(path, 1);
            switch (nameService)
            {
                case TownUrl:
                    TownService(ctx);
                    break;
                case CategoryUrl:
                    CategoryService(ctx);
                    break;
                case AdvertisementUrl:
                    AdvertisementService(ctx);
                    break;
                case AuthUrl:
                    AuthService(ctx);
                    break;
                default:
                    ctx.Response.StatusCode = 404;
                    await ctx.Response.WriteAsJsonAsync(new BaseResponse(1, "Такого контроллера не существует"));
                    break;
            }
        }

        private void TownService(HttpContext ctx)
        {
            Invoker(ctx, DataSourceMethods.TownMethodsList, TownController);
        }

        private void CategoryService(HttpContext ctx)
        {
            Invoker(ctx, DataSourceMethods.CategoryMethodsList, CategoryController);
        }

        private void AdvertisementService(HttpContext ctx)
        {
            Invoker(ctx, DataSourceMethods.AdvertisementMethodsList, AdvertisementController);
        }

        private void AuthService(HttpContext ctx)
        {
            Invoker(ctx, DataSourceMethods.AuthMethodsList, AuthController);
        }


        /// <summary>
        /// На основе принятых параметров вызывает определенный метод контроллера.
        /// Поиск осуществляется исходя из имени метода, указанного в ссылке (ctx.Request.Path)
        /// </summary>
        /// <typeparam name="T">Тип контроллера</typeparam>
        /// <param name="ctx">HttpContext контекст</param>
        /// <param name="listMethodInfo">Список методов контроллера</param>
        /// <param name="controller">контроллер</param>
        /// <exception cref="ArgumentNullException"></exception>
        private void Invoker<T>(HttpContext ctx, List<MethodInfo> listMethodInfo, T controller)
            where T : class
        {
            var nameMethod = GetNameFromPath(ctx.Request.Path, 2);
            var method = listMethodInfo.Find(item => item.Name == nameMethod);
            if (method != null) method.Invoke(controller, new object?[] { ctx });
            else throw new ArgumentNullException("Такого метода не существует");
        }

        private string GetNameFromPath(PathString path, int index)
        {
            return path.Value.Split("/")[index];
        }
    }
}
