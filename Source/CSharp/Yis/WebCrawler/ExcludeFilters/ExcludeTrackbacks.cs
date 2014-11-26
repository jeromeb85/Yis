using System;

namespace Yis.WebCrawler.ExcludeFilters
{
    public class ExcludeTrackbacks : IExcludeFilter
    {
        public bool IsMatch(Uri url)
        {
            return url.AbsoluteUri.EndsWith("/trackback");
        }
    }
}