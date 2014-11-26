using System;

namespace Yis.WebCrawler.ExcludeFilters
{
    public class ExcludeAnchors : IExcludeFilter
    {
        public bool IsMatch(Uri url)
        {
            return url.AbsoluteUri.Contains("#");
        }
    }
}