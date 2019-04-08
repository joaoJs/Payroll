using System;
using NUnit.Framework;
using PayrollCalculator.Calculator;

namespace PayrollTest.CalculatorTest
{
    public class DeductionCalculatorIrelandTest
    {
        private DeductionCalculatorIreland deductionCalculator;
        private decimal rate;
        private decimal newRate;
        private int hours;
        private int newHours;
        private decimal grossAmount;
        decimal compulsoryPensionRate;

        [SetUp]
        public void init()
        {
            deductionCalculator = new DeductionCalculatorIreland();
            rate = 10.0m;
            newRate = 50.0m;
            hours = 4;
            newHours = 40;
            grossAmount = rate * hours;
            // Compulsory pension should be 4%
            compulsoryPensionRate = 0.04m;
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
        public void TestCalculatorCalculatesCorrectIncomeTaxWhenSalaryGreaterThan600()
        {
            decimal incomeTax = deductionCalculator.CalculateIncomeTax(newRate, newHours);
            decimal expected = CalculateIncomeTaxHelper(newRate, newHours);
            Assert.AreEqual(expected, incomeTax);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectCompulsoryPension()
        {
            decimal compulsoryPension = deductionCalculator.CalculateCompulsoryPension(rate, hours);
            decimal expected = compulsoryPensionRate * rate * hours;
            Assert.AreEqual(expected, compulsoryPension);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectUSC()
        {
            decimal usc = deductionCalculator.CalculateUSC(rate, hours);
            decimal expected = CalculateUSCHelper(rate,hours);
            Assert.AreEqual(expected, usc);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectUSCWhenSalaryGreaterThan500()
        {
            decimal usc = deductionCalculator.CalculateUSC(newRate, newHours);
            decimal expected = CalculateUSCHelper(newRate, newHours);
            Assert.AreEqual(expected, usc);
        }

        [Test]
        public void TestCalculatorCalculatesCorrectTotal()
        {
            decimal total = deductionCalculator.CalculateDeductionTotal(rate, hours);
            decimal incomeTax = CalculateIncomeTaxHelper(rate, hours);
            decimal compulsoryPension = compulsoryPensionRate * rate * hours;
            decimal usc = CalculateUSCHelper(rate, hours);
            decimal expected = incomeTax + compulsoryPension + usc;
            Assert.AreEqual(expected, total);
        }
        #endregion

        #region helper methods
        //Given the employee is located in Ireland, income tax at a rate of 25% 
        //for the first €600 and 40% thereafter
        private decimal CalculateIncomeTaxHelper(decimal rateLocal, int hoursLocal)
        {
            decimal salary = rateLocal * hoursLocal;
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

        //Given the employee is located in Ireland, a Universal social charge of 7%
        // is applied for the first €500 euro and 8% thereafter
        private decimal CalculateUSCHelper(decimal rateLocal, int hoursLocal)
        {
            decimal salary = rateLocal * hoursLocal;
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
