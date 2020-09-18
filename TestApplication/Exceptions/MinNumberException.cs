using System;

namespace TestApplication.Exceptions
{
    public class MinNumberException : Exception
    {
        public MinNumberException(int num, int min) : base($"The number ({num}) is less then allowed ({min})")
        {
        }
    }
}