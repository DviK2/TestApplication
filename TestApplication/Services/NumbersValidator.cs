using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace TestApplication.Services
{
    public interface INumbersValidator
    {
        bool Validate(List<int> numbers);
    }

    public class NumbersValidator : INumbersValidator
    {
        private readonly int _maxCount;
        private readonly int _max;
        private readonly int _min;

        public NumbersValidator(IConfiguration config)
        {
            _maxCount = int.Parse(config.GetSection("Numbers:MaxCount").Value);
            _max = int.Parse(config.GetSection("Numbers:Max").Value);
            _min = int.Parse(config.GetSection("Numbers:Min").Value);
        }

        public bool Validate(List<int> numbers)
        {
            if (numbers.Count > _maxCount)
            {
                throw new MaxCountException(numbers.Count, _maxCount);
            }

            foreach (var number in numbers)
            {
                if (number < _min)
                {
                    throw new MinNumberException(number, _min);
                }

                if (number > _max)
                {
                    throw new MaxNumberException(number, _max);
                }
            }

            return true;
        }
    }

    public class MaxNumberException : Exception
    {
        public MaxNumberException(int num, int max) : base($"The number ({num}) is greater then allowed ({max})")
        {
        }
    }


    public class MinNumberException : Exception
    {
        public MinNumberException(int num, int min) : base($"The number ({num}) is less then allowed ({min})")
        {
        }
    }

    public class MaxCountException : Exception
    {
        public MaxCountException(int count, int maxCount) : base($"Numbers count ({count}) is greater then allowed ({maxCount})")
        {
        }
    }
}
