namespace BulletinBoard.API.ExtensionClasses
{
    public static class Identification
    {
        private static List<string> IdsConnectionsList = new();
        public static void AddIdConnect(string id)
        {
            var duplicate = IdsConnectionsList.FirstOrDefault(x => x == id);
            if (duplicate == null) IdsConnectionsList.Add(id);
        }

        public static bool IsAdmin(string currentIdConnection)
        {
            var isAdmin = IdsConnectionsList.FirstOrDefault(x => x == currentIdConnection);
            return isAdmin != null;
        }
    }
}
