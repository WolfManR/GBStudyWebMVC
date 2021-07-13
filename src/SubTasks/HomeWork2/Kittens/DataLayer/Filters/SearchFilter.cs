using System;
using System.Linq.Expressions;
using DataLayer.Abstractions.Filters;

namespace DataLayer.Filters
{
    public abstract class SearchFilter<T, TData> where TData : SearchFilterData
    {
        public TData Data { get; init; }
        public abstract Expression<Func<T, bool>> Inject();
    }
}