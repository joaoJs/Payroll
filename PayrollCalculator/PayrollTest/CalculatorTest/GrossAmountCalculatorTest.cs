using System;
using NUnit.Framework;
using PayrollCalculator.Calculator;
using PayrollCalculator.Interfaces;

namespace PayrollTest.CalculatorTest
{
    public class GrossAmountCalculatorTest
    {
        private IGrossAmountCalculator calculator;
        private decimal rate;
        private int hours;
        private decimal grossAmount;

        [SetUp]
        public void init()
        {
            calculator = new GrossAmountCalculator();
            rate = 10.0m;
            hours = 4;
            grossAmount = rate * hours;
        }

        [Test]
        public void TestCalculatorNotNull()
        {
            Assert.NotNull(calculator);
        }

        [Test]
        public void TestCalculateGrossAmount()
        {
            decimal expected = rate * hours;
            Assert.AreEqual(expected, grossAmount);
        }
    }
}
