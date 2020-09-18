using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Moq;
using TestApplication.Services;
using Xunit;

namespace TestApplication.Tests.Services
{
    public class NumbersServiceTests
    {
        private readonly NumbersService _service;

        public NumbersServiceTests()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"Numbers:MinTimeOut", "5"},
                {"Numbers:MaxTimeOut", "10"},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var mockValidator = new Mock<INumbersValidator>();
            mockValidator.Setup(val => val.Validate(It.IsAny<List<int>>())).Returns(true);

            var mockCash = new Mock<INumbersCashService>();
            mockCash.Setup(val => val.TryGet(It.IsAny<int>(), out It.Ref<int>.IsAny)).Returns(false);
            mockCash.Setup(val => val.Set(It.IsAny<int>(), It.IsAny<int>()));

            _service = new NumbersService(mockValidator.Object, mockCash.Object, configuration);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1, 1)]
        [InlineData(3, 1, 1, 1)]
        [InlineData(5, 1, 2)]
        public void GetSquaredSum_ReturnCorrectSum(int sum, params int[] numbers)
        {
            var result = _service.GetSquaredSum(numbers.ToList());

            Assert.Equal(sum, result);
        }
    }
}
