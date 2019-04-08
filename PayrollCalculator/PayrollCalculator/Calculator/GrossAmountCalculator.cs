using System;
using PayrollCalculator.Interfaces;

namespace PayrollCalculator.Calculator
{
    public class GrossAmountCalculator : IGrossAmountCalculator
    {
        #region methods
        public decimal CalculateGrossAmount(decimal rate, int hour)
        {
            return rate * hour;
        }
        #endregion
    }
}
