using System;
namespace PayrollCalculator.Exceptions
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException(string message)
        : base(message)
        {
        }

        public InvalidValueException()
        : base("Please provide a valid value.")
        {
        }
    }
}
