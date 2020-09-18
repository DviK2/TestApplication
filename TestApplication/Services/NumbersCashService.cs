using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace TestApplication.Services
{
    public class NumbersCashService
    {
        private const string NumbersCashKey = "number";

        private readonly IMemoryCache _cache;

        public NumbersCashService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool TryGet(int number, out int value)
        {
            if (_cache.TryGetValue($"{NumbersCashKey}{number}", out var cashValue))
            {
                value = int.Parse(cashValue.ToString());
                return true;
            }
            else
            {
                value = 0;
                return false;
            }
        }

        public void Set(int number, int value)
        {
            _cache.Set($"{NumbersCashKey}{number}", value);
        }
    }
}
