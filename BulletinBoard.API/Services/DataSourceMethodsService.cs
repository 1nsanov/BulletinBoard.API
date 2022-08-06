using System.Reflection;
using BulletinBoard.API.Controllers;

namespace BulletinBoard.API.Services
{
    public class DataSourceMethodsService
    {
        public readonly List<MethodInfo> TownMethodsList;
        public readonly List<MethodInfo> CategoryMethodsList;
        public readonly List<MethodInfo> AdvertisementMethodsList;
        public readonly List<MethodInfo> AuthMethodsList;

        public DataSourceMethodsService()
        {
            TownMethodsList = InitTownList();
            CategoryMethodsList = InitCategoryList();
            AdvertisementMethodsList = InitAdvertisementList();
            AuthMethodsList = InitAuthList();
        }

        private List<MethodInfo> InitTownList()
        {
            return CreateListMethodInfo(typeof(TownController));
        }
        private List<MethodInfo> InitCategoryList()
        {
            return CreateListMethodInfo(typeof(CategoryController));
        }
        private List<MethodInfo> InitAdvertisementList()
        {
            return CreateListMethodInfo(typeof(AdvertisementController));
        }
        private List<MethodInfo> InitAuthList()
        {
            return CreateListMethodInfo(typeof(AuthController));
        }

        private static List<MethodInfo> CreateListMethodInfo(Type tp)
        {
            var methods = tp.GetMethods();
            var list = new List<MethodInfo>();
            if (list == null) throw new ArgumentNullException(nameof(list));
            list.AddRange(methods);
            return list;
        }
    }
}
