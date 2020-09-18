using System;

namespace TestApplication.Exceptions
{
    public class MaxCountException : Exception
    {
        public MaxCountException(int count, int maxCount) : base($"Numbers count ({count}) is greater then allowed ({maxCount})")
        {
        }
    }
}