using System;

namespace AuthenticationService.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
