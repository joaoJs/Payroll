using System;
namespace PayrollCalculator.Calculator
{
    public class DeductionCalculatorGermany : DeductionCalculator
    {

        #region constants
        private const decimal compulsoryPensionRate = 0.02m;
        #endregion

        #region constructor
        public DeductionCalculatorGermany()
        {
        }
        #endregion

        #region methods
        //Given the employee is located in Germany, income tax at a rate of 25% 
        //is applied on the first €400 and 32% thereafter
        public override decimal CalculateIncomeTax(decimal rate, int hours)
        {
            decimal salary = rate * hours;
            decimal incomeTax;
            if (salary <= 400.0m)
            {
                incomeTax = 0.25m * salary;
            }
            else
            {
                decimal tax1 = 0.25m * 400.0m;
                decimal tax2 = 0.32m * (salary - 400.0m);
                incomeTax = tax1 + tax2;
            }
            return incomeTax;
        }

        public decimal CalculateCompulsoryPension(decimal rate, int hours) => compulsoryPensionRate * rate * hours;

        public override decimal CalculateDeductionTotal(decimal rate, int hours)
        {
            decimal total;
            decimal incomeTax = CalculateIncomeTax(rate, hours);
            decimal compulsoryPension = CalculateCompulsoryPension(rate, hours);
            total = incomeTax + compulsoryPension;
            return total;
        }
        #endregion
    }
}
