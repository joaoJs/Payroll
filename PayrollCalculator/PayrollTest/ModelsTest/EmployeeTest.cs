using System;
using NUnit.Framework;
using PayrollCalculator.Exceptions;
using PayrollCalculator.Models;

namespace PayrollTest.ModelsTest
{
    public class EmployeeTest
    {
        private decimal rate;
        private int hours;
        private string location;

        [SetUp]
        public void init()
        {
            rate = 10.0m;
            hours = 4;
            location = "IRELAND";
        }

        [Test]
        public void TestEmployeeNotNull()
        {
            Employee employee = new Employee(rate, hours, location);
            Assert.NotNull(employee);
        }

        [Test]
        public void TestRateCannotBeNegative()
        {
            Assert.That(() => new Employee(-10.0m, hours, location),
                Throws.TypeOf<InvalidValueException>());
        }

        [Test]
        public void TestHoursCannotBeNegative()
        {
            Assert.That(() => new Employee(rate, -5, location),
                Throws.TypeOf<InvalidValueException>());
        }

        [Test]
        public void TestLocationNeedsToBeValid()
        {
            Assert.That(() => new Employee(rate, hours, "x"),
                Throws.TypeOf<InvalidValueException>());
        }
    }
}
