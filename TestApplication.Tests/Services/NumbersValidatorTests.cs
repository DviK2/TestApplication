using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using TestApplication.Exceptions;
using TestApplication.Services;
using Xunit;

namespace TestApplication.Tests.Services
{
    public class NumbersValidatorTests
    {
        private NumbersValidator _validator;

        public NumbersValidatorTests()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"Numbers:MaxCount", "5"},
                {"Numbers:Max", "10"},
                {"Numbers:Min", "1"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            _validator = new NumbersValidator(configuration);
        }

        [Fact]
        public void CorrectNumbers_ReturnTrue()
        {
            var numbers = new List<int>() {1, 2, 1, 9};

            bool result = _validator.Validate(numbers);

            Assert.True(result);
        }

        [Fact]
        public void LargeNumber_ThrowException()
        {
            var numbers = new List<int>() {1, 2, 100};

            Assert.Throws<MaxNumberException>(() => _validator.Validate(numbers));
        }

        [Fact]
        public void LessNumber_ThrowException()
        {
            var numbers = new List<int>() {1, 2, -100};

            Assert.Throws<MinNumberException>(() => _validator.Validate(numbers));
        }

        [Fact]
        public void MaxCountNumbers_ThrowException()
        {
            var numbers = new List<int>() {1, 2, 1, 1, 2, 4};

            Assert.Throws<MaxCountException>(() => _validator.Validate(numbers));
        }
    }
}
