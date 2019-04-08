using System;
namespace PayrollCalculator.Calculator
{
    public abstract class DeductionCalculator
    {
        #region abstract methods
        public abstract decimal CalculateIncomeTax(decimal rate, int hours);

        public abstract decimal CalculateDeductionTotal(decimal rate, int hours);
        #endregion
    }
}
