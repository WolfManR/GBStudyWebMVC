using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Validations;
using BusinessLayer.Validations;
using Xunit;

namespace BusinessLayerTests
{
    public class KittenValidationTests
    {
        private static readonly ValidationService<Kitten> ValidationService = new KittenValidation();

        [Fact]
        public void KittenNickname_CannotBeEmpty()
        {
            var toTest = new Kitten()
            {
                Nickname = "",
                Weight = 22,
                Color = "Lorem",
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-31b", actual.Code);
            Assert.Equal(nameof(Kitten.Nickname), actual.PropertyName);
        }

        [Fact]
        public void KittenNickname_CannotBeNULL()
        {
            var toTest = new Kitten
            {
                Nickname = null,
                Weight = 22,
                Color = "Lorem",
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-31a", actual.Code);
            Assert.Equal(nameof(Kitten.Nickname), actual.PropertyName);
        }

        [Fact]
        public void KittenNicknameNotNULLOrEmpty_IsCorrect()
        {
            var toTest = new Kitten
            {
                Nickname = "Lorem",
                Weight = 22,
                Color = "Lorem",
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest).Count;

            Assert.Equal(0, actual);
        }

        [Fact]
        public void KittenWeight_CannotBeGreater_40()
        {
            var toTest = new Kitten()
            {
                Nickname = "Lorem",
                Weight = 41,
                Color = "Lorem",
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-32b", actual.Code);
            Assert.Equal(nameof(Kitten.Weight), actual.PropertyName);
        }

        [Fact]
        public void KittenWeight_CannotBeLower_0()
        {
            var toTest = new Kitten
            {
                Nickname = "Lorem",
                Weight = -1,
                Color = "Lorem",
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-32a", actual.Code);
            Assert.Equal(nameof(Kitten.Weight), actual.PropertyName);
        }

        [Fact]
        public void KittenWeigh_Beetween_0_and_40_IsCorrect()
        {
            var toTest = new Kitten
            {
                Nickname = "Lorem",
                Weight = 33,
                Color = "Lorem",
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest).Count;

            Assert.Equal(0, actual);
        }

        [Fact]
        public void KittenColor_CannotBeEmpty()
        {
            var toTest = new Kitten()
            {
                Nickname = "Lorem",
                Weight = 33,
                Color = "",
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-33b", actual.Code);
            Assert.Equal(nameof(Kitten.Color), actual.PropertyName);
        }

        [Fact]
        public void KittenColor_CannotBeNULL()
        {
            var toTest = new Kitten
            {
                Nickname = "Lorem",
                Weight = 33,
                Color = null,
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-33a", actual.Code);
            Assert.Equal(nameof(Kitten.Color), actual.PropertyName);
        }

        [Fact]
        public void KittenColorNotNULLOrEmpty_IsCorrect()
        {
            var toTest = new Kitten
            {
                Nickname = "Lorem",
                Weight = 33,
                Color = "Lorem",
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest).Count;

            Assert.Equal(0, actual);
        }

        [Fact]
        public void KittenFeed_CannotBeEmpty()
        {
            var toTest = new Kitten()
            {
                Nickname = "Lorem",
                Weight = 33,
                Color = "Lorem",
                Feed = ""
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-34b", actual.Code);
            Assert.Equal(nameof(Kitten.Feed), actual.PropertyName);
        }

        [Fact]
        public void KittenFeed_CannotBeNULL()
        {
            var toTest = new Kitten
            {
                Nickname = "Lorem",
                Weight = 33,
                Color = "Lorem",
                Feed = null
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-34a", actual.Code);
            Assert.Equal(nameof(Kitten.Feed), actual.PropertyName);
        }

        [Fact]
        public void KittenFeedNotNULLOrEmpty_IsCorrect()
        {
            var toTest = new Kitten
            {
                Nickname = "Lorem",
                Weight = 33,
                Color = "Lorem",
                Feed = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest).Count;

            Assert.Equal(0, actual);
        }
    }
}