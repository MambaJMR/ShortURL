namespace ShortURL.Serveces
{
    public static class UrlService
    {
        public static string GuidCreate()
        {
            return Guid.NewGuid().ToString().Substring(0, 7);
        }
    }
}
