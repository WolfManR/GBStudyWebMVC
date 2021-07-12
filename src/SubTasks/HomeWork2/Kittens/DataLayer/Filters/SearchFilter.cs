using System;
using System.Linq.Expressions;

namespace DataLayer.Filters
{
    public abstract class SearchFilter<T>
    {
        public abstract Expression<Func<T, bool>> Inject();
    }
}