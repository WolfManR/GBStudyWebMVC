using System.Linq;
using DataLayer.Abstractions.Filters;

namespace DataLayer.Filters
{
    public static class QueryExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, PageFilter filter)
        {
            if (filter is null) return query.Take(0);
            return query.Skip((filter.Page > 1 ? filter.Page - 1 : 0) * filter.PageSize).Take(filter.PageSize);
        }

        public static IQueryable<T> SearchWith<T, TData>(this IQueryable<T> query, SearchFilter<T, TData> filter) where TData : SearchFilterData
        {
            if (filter is null) return query.Take(0);
            return query.Where(filter.Inject());
        }
    }
}