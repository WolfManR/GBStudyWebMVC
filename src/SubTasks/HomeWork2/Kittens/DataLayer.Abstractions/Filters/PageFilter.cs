using System;

namespace DataLayer.Abstractions.Filters
{
    public readonly struct PageFilter : IEquatable<PageFilter>
    {
        public int Page { get; init; }
        public int Size { get; init; }

        public static PageFilter Empty() => new();

        public override bool Equals(object obj)
        {
            return obj is PageFilter filter && Equals(filter);
        }

        public bool Equals(PageFilter other)
        {
            return Page == other.Page &&
                   Size == other.Size;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Page, Size);
        }

        public static bool operator ==(PageFilter left, PageFilter right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PageFilter left, PageFilter right)
        {
            return !(left == right);
        }
    }
}