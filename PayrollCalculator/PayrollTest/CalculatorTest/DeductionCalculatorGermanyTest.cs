using System;
using NUnit.Framework;
using PayrollCalculator.Calculator;

namespace PayrollTest.CalculatorTest
{
    public class DeductionCalculatorGermanyTest
    {
        private DeductionCalculatorGermany deductionCalculator;
        private decimal rate;
        private decimal newRate;
        private int hours;
        private int newHours;
        private decimal grossAmount;
        decimal compulsoryPensionRate;

        [SetUp]
        public void init()
        {
            deductionCalculator = new DeductionCalculatorGermany();
            rate = 10.0m;
            newRate = 50.0m;
            hours = 4;
            newHours = 40;
            grossAmount = rate * hours;
            // Compulsory pension should be 2%
            compulsoryPensionRate = 0.02m;
        }

        #region TestMethods
        [Test]
        public void TestCalculatorCalculatesCorrectIncomeTax()
        {
            decimal incomeTax = deductionCalculator.CalculateIncomeTax(rate, hours);
            decimal expected = CalculateIncomeTaxHelper(rate, hours);
            Assert.AreEqual(expected, incomeTax);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectIncomeTaxWhenSalaryGreaterThan400()
        {
            decimal incomeTax = deductionCalculator.CalculateIncomeTax(newRate, newHours);
            decimal expected = CalculateIncomeTaxHelper(newRate, newHours);
            Assert.AreEqual(expected, incomeTax);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectCompulsoryPension()
        {
            decimal compulsoryPension = deductionCalculator.CalculateCompulsoryPension(rate, hours);
            // Compulsory pension should be 2%
            decimal expected = compulsoryPensionRate * rate * hours;
            Assert.AreEqual(expected, compulsoryPension);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectTotal()
        {
            decimal total = deductionCalculator.CalculateDeductionTotal(rate, hours);
            decimal incomeTax = CalculateIncomeTaxHelper(rate, hours);
            decimal compulsoryPension = compulsoryPensionRate * rate * hours;
            decimal expected = incomeTax + compulsoryPension;
            Assert.AreEqual(expected, total);
        }
        #endregion

        #region HelperMethods
        //Given the employee is located in Germany, income tax at a rate of 25% 
        //is applied on the first €400 and 32% thereafter
        private decimal CalculateIncomeTaxHelper(decimal rateLocal, int hoursLocal)
        {
            decimal salary = rateLocal * hoursLocal;
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
        #endregion
    }
}
