﻿using System;

namespace Yis.WebCrawler.ExcludeFilters
{
    public class ExcludeMailTo : IExcludeFilter
    {
        public bool IsMatch(Uri url)
        {
            return url.AbsoluteUri.StartsWith("mailto:");
        }
    }
}