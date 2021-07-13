using System;
using System.Linq.Expressions;
using DataBase.Abstractions.Entities;
using DataLayer.Abstractions.Filters;

namespace DataLayer.Filters
{
    public class KittenSearchFilter : SearchFilter<Kitten, KittenSearchFilterData>
    {
        public override Expression<Func<Kitten, bool>> Inject() => arg => arg.Nickname.Equals(Data.Nickname, StringComparison.OrdinalIgnoreCase);
    }
}