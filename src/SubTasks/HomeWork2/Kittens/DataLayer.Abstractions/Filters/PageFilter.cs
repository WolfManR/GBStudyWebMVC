using System;

namespace DataLayer.Abstractions.Filters
{
    public class PageFilter
    {
        public PageFilter(int page, int pageSize)
        {
            if (page < 1) throw new ArgumentException("Page cannot be lower 1");
            if (pageSize < 1) throw new ArgumentException("Size of page cannot be lower 1");

            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; }
        public int PageSize { get; }
    }
}