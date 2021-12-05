using System;
using System.Linq;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Validations;
using BusinessLayer.Validations;
using Xunit;

namespace BusinessLayerTests
{
    public class ClinicValidationTests
    {
        private static readonly ValidationService<Clinic> ValidationService = new ClinicValidation();

        [Fact]
        public void ClinicName_CannotBeEmpty()
        {
            var toTest = new Clinic
            {
                Name = ""
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-41b", actual.Code);
            Assert.Equal(nameof(Clinic.Name), actual.PropertyName);
        }

        [Fact]
        public void ClinicName_CannotBeNULL()
        {
            var toTest = new Clinic
            {
                Name = null
            };

            var actual = ValidationService.ValidateEntry(toTest)[0];

            Assert.Equal("BCL-41a", actual.Code);
            Assert.Equal(nameof(Clinic.Name), actual.PropertyName);
        }

        [Fact]
        public void ClinicNameNotNULLOrEmpty_IsCorrect()
        {
            var toTest = new Clinic
            {
                Name = "Lorem"
            };

            var actual = ValidationService.ValidateEntry(toTest).Count;

            Assert.Equal(0, actual);
        }
    }
}
