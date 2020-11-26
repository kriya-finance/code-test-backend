using System;

namespace SlothEnterprise.ProductApplication.Exceptions
{
    public class InvalidApplicationException : Exception
    {
        public InvalidApplicationException() { }

        public InvalidApplicationException(string message)
            : base(message) { }

        public InvalidApplicationException(string message, Exception inner)
            : base(message, inner) { }
    }
}
