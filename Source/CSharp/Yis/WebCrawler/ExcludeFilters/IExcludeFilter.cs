using System;

namespace Yis.WebCrawler.ExcludeFilters
{
    public interface IExcludeFilter
    {
        bool IsMatch(Uri url);
    }
}