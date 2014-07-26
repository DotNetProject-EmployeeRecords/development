using System;
namespace Eisk.Helpers
{
    public class InvalidDataKeyException : Exception
    {
        public InvalidDataKeyException(string message) : base(message) { }
    }

    public class NullValueNotAllowedException : Exception
    {
        public NullValueNotAllowedException(string message) : base(message) { }
    }

    public class EmptyValueNotAllowedException : Exception
    {
        public EmptyValueNotAllowedException(string message) : base(message) { }
    }

    public class DataNotUpdatedException : Exception
    {
        public DataNotUpdatedException(string message) : base(message) { }
    }

    public class BusinessRuleViolationOnInMemoryException : Exception
    {
        public BusinessRuleViolationOnInMemoryException(string message) : base(message) { }
    }

    public class BusinessRuleViolationOnDbAccessException : Exception
    {
        public BusinessRuleViolationOnDbAccessException(string message) : base(message) { }
    }
}
