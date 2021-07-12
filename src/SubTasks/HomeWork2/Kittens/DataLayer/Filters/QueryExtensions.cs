using System.Linq;
using DataLayer.Abstractions.Filters;

namespace DataLayer.Filters
{
    public static class QueryExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, PageFilter filter)
        {
            return query.Skip(filter.Page * filter.Size).Take(filter.Size);
        }

        public static IQueryable<T> SearchWith<T>(this IQueryable<T> query, SearchFilter<T> filter)
        {
            return query.Where(filter.Inject());
        }
    }
}