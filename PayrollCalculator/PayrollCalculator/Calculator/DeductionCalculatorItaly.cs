using System;
namespace PayrollCalculator.Calculator
{
    public class DeductionCalculatorItaly : DeductionCalculator
    {
        #region constants
        private const decimal incomeTaxRate = 0.25m;
        #endregion

        #region constructor
        public DeductionCalculatorItaly() { }
        #endregion


        #region methods
        public override decimal CalculateIncomeTax(decimal rate, int hours) => incomeTaxRate * rate * hours;

        // calculating INPS This is charged at 9% for the first €500 and 
        // increases by .1% over every €100 thereafter. There is probably a 
        // more efficient way of solving this. 
        public decimal CalculateINPS(decimal rate, int hours)
        {
            decimal inpsTotal;
            decimal grossAmount = rate * hours;
            if (grossAmount <= 500.0m)
            {
                inpsTotal = 0.09m * grossAmount;
            }
            else
            {
                inpsTotal = 0.09m * 500.0m;

                decimal tax = 0.09m;
                decimal i = 600.0m;
                while (i <= grossAmount)
                {
                    tax += 0.001m;
                    inpsTotal += 100.0m * tax;
                    i += 100.0m;
                }
                if (i % 100.0m > 0.0m)
                {
                    inpsTotal += (i % 100.0m) * (tax + 0.001m);
                }
            }
            return inpsTotal;
        }

        public override decimal CalculateDeductionTotal(decimal rate, int hours)
        {
            decimal total;
            decimal incomeTax = CalculateIncomeTax(rate, hours);
            decimal inps = CalculateINPS(rate, hours);
            total = incomeTax + inps;
            return total;
        }
        #endregion
    }
}
