using System;
using PayrollCalculator.Exceptions;
using PayrollCalculator.Util;

namespace PayrollCalculator.Models
{
    public class Employee
    {
        #region Fields
        private decimal _rate { get; set; }
        private int _hours { get; set; }
        private String _location { get; set; }
        private LocationChecker _locationChecker { get; set; }
        #endregion

        #region Constructor
        public Employee(decimal rate, int hours, String location)
        {
            if (rate < 0.0m)
            {
                throw new InvalidValueException();
            }
            if (hours < 0)
            {
                throw new InvalidValueException();
            }
            _locationChecker = new LocationChecker();

            if (!_locationChecker.CheckLocation(location))
            {
                throw new InvalidValueException();
            }
            _rate = rate;
            _hours = hours;
            _location = location;
        }
        #endregion
    }
}
