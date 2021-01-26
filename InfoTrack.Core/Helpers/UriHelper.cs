using System;

namespace InfoTrack.Core.Helpers
{
    public static class UriHelper
    {
        public static string ToUri(string url) => !url.Contains("http://") && !url.Contains("https://") ? $"http://{url}" : url;

        public static string ToBaseUrl(Uri uri) => uri.Host.Contains("www.") ? uri.Host[4..] : uri.Host;
    }
}
