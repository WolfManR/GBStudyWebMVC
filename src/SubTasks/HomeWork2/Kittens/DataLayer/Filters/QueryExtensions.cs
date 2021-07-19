using System.Linq;
using DataLayer.Abstractions.Filters;

namespace DataLayer.Filters
{
    public static class QueryExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, PageFilter filter)
        {
            return query.Skip((filter.Page > 1 ? filter.Page - 1 : 0) * filter.Size).Take(filter.Size);
        }

        public static IQueryable<T> SearchWith<T, TData>(this IQueryable<T> query, SearchFilter<T, TData> filter) where TData : SearchFilterData
        {
            return query.Where(filter.Inject());
        }
    }
}