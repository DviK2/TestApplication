using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestApplication.Services
{
    public class NumbersService
    {
        private readonly INumbersValidator _validator;
        private readonly INumbersCashService _cashService;
        private readonly int _minTimeout;
        private readonly int _maxTimeout;

        public NumbersService(INumbersValidator validator, INumbersCashService cashService, IConfiguration config)
        {
            _validator = validator;
            _cashService = cashService;

            _minTimeout = int.Parse(config.GetSection("Numbers:MinTimeOut").Value);
            _maxTimeout = int.Parse(config.GetSection("Numbers:MaxTimeOut").Value);
        }

        public int GetSquaredSum(List<int> numbers)
        {
            _validator.Validate(numbers);

            int result = numbers.Sum(number => GetSquare(number));

            return result;
        }

        private int GetSquare(in int number)
        {
            if (_cashService.TryGet(number, out int value))
            {
                return value;
            }

            Wait();

            value = number * number;
            _cashService.Set(number, value);
            return number * number;
        }

        private void Wait()
        {
            var rng = new Random();
            int sleepTime = rng.Next(_minTimeout, _maxTimeout);
            Thread.Sleep(sleepTime);
        }
    }
}
