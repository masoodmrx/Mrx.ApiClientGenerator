namespace Mrx.ApiClientGenerator.Dart.Helpers
{
    public static class StringExtensions
    {
        public static bool IsValidUrl(this string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
