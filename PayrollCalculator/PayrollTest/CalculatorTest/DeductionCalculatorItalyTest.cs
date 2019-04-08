using System;
using NUnit.Framework;
using PayrollCalculator.Calculator;

namespace PayrollTest.CalculatorTest
{
    public class DeductionCalculatorItalyTest
    {
        private DeductionCalculatorItaly deductionCalculator;
        private decimal rate;
        private int hours;
        private decimal grossAmount;
        decimal incomeTaxRate;

        [SetUp]
        public void init()
        {
            deductionCalculator = new DeductionCalculatorItaly();
            rate = 10.0m;
            hours = 4;
            grossAmount = rate * hours;
            // Compulsory pension should be 2%
            incomeTaxRate = 0.25m;
        }

        #region TestMethods
        [Test]
        public void TestCalculatorCalculatesCorrectIncomeTax()
        {
            decimal incomeTax = deductionCalculator.CalculateIncomeTax(rate, hours);
            decimal expected = incomeTaxRate * rate * hours;
            Assert.AreEqual(expected, incomeTax);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectINPS()
        {
            decimal inps = deductionCalculator.CalculateINPS(rate, hours);
            decimal expected = CalculateINPSHelper(rate,hours);
            Assert.AreEqual(expected, inps);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectINPSWhenSalaryGreaterThan500()
        {
            decimal newRate = 50.0m;
            int newHours = 40;
            decimal inps = deductionCalculator.CalculateINPS(newRate, newHours);
            decimal expected = CalculateINPSHelper(newRate, newHours);
            Assert.AreEqual(expected, inps);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectTotal()
        {
            decimal total = deductionCalculator.CalculateDeductionTotal(rate, hours);
            decimal incomeTax = incomeTaxRate * rate * hours;
            decimal inps = CalculateINPSHelper(rate, hours);
            decimal expected = incomeTax + inps;
            Assert.AreEqual(expected, total);
        }
        #endregion


        #region HelperMethods
        // calculating INPS This is charged at 9% for the first €500 and 
        // increases by .1% over every €100 thereafter. There is probably a 
        // more efficient way of solving this. 
        private decimal CalculateINPSHelper(decimal rateLocal, int hoursLocal)
        {
            decimal inpsTotal;
            decimal grossAmountLocal = rateLocal * hoursLocal;
            if (grossAmountLocal <= 500.0m)
            {
                inpsTotal = 0.09m * grossAmountLocal;
            }
            else
            {
                inpsTotal = 0.09m * 500.0m;

                decimal tax = 0.09m;
                decimal i = 600.0m;
                while (i <= grossAmountLocal)
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
        #endregion
    }
}
