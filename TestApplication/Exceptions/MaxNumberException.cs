using System;

namespace TestApplication.Exceptions
{
    public class MaxNumberException : Exception
    {
        public MaxNumberException(int num, int max) : base($"The number ({num}) is greater then allowed ({max})")
        {
        }
    }
}