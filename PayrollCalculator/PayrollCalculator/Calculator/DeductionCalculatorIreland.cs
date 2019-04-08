using System;
namespace PayrollCalculator.Calculator
{
    public class DeductionCalculatorIreland : DeductionCalculator
    {
        #region constants
        private const decimal compulsoryPensionRate = 0.04m;
        #endregion

        #region constructor
        public DeductionCalculatorIreland()
        {
        }
        #endregion

        #region methods
        //Given the employee is located in Ireland, income tax at a rate of 25% 
        //for the first €600 and 40% thereafter
        public override decimal CalculateIncomeTax(decimal rate, int hours)
        {
            decimal salary = rate * hours;
            decimal incomeTax;
            if (salary <= 600.0m)
            {
                incomeTax = 0.25m * salary;
            }
            else
            {
                decimal tax1 = 0.25m * 600.0m;
                decimal tax2 = 0.40m * (salary - 600.0m);
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
            decimal usc = CalculateUSC(rate, hours);
            total = incomeTax + compulsoryPension + usc;
            return total;
        }

        //Given the employee is located in Ireland, a Universal social charge of 7%
        // is applied for the first €500 euro and 8% thereafter
        public decimal CalculateUSC(decimal rate, int hours)
        {
            decimal salary = rate * hours;
            decimal usc;
            if (salary <= 500.0m)
            {
                usc = 0.07m * salary;
            }
            else
            {
                decimal uscBefore500 = 0.07m * 500.0m;
                decimal uscAfter500 = 0.08m * (salary - 500.0m);
                usc = uscBefore500 + uscAfter500;
            }
            return usc;
        }
        #endregion
    }
}
