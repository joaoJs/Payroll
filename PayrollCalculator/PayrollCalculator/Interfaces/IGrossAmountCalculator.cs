using System;
namespace PayrollCalculator.Interfaces
{
    public interface IGrossAmountCalculator
    {
        decimal CalculateGrossAmount(decimal rate, int hours);
    }
}
