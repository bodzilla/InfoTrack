namespace InfoTrack.Core.Helpers
{
    public static class UriHelper
    {
        public static string ToUri(string url) => !url.Contains("http://") && !url.Contains("https://") ? $"http://{url}" : url;
    }
}
