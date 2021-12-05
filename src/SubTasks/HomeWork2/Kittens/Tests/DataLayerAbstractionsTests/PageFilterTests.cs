using System;
using DataLayer.Abstractions.Filters;
using Xunit;

namespace DataLayerAbstractionsTests
{
    public class PageFilterTests
    {
        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, -1)]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void SetPageOrPageSizeLower_1_CauseException(int page, int pageSize)
        {
            Assert.Throws<ArgumentException>(() => new PageFilter(page, pageSize));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public void SetPageAndPageSizeGreaterOrEqual_1_IsCorrect(int page, int pageSize)
        {
            _ = new PageFilter(page, pageSize);
        }
    }
}
