using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katalyst_TDD_Starter.Test
{
    [TestClass]
    public class FizzBuzzTests
    {
        public FizzBuzz ToTest { get; set; }


        public FizzBuzzTests()
        {
            ToTest = new FizzBuzz();
        }

        [TestMethod]
        public void Convert_1_to_1()
        {
            var input = 1;

            string expected = "1";
            string actual = ToTest.Convert(input);

            Assert.AreEqual(expected, actual, $"Fizzbuzz({input}) does not return {expected}.");
        }

        [TestMethod]
        public void Convert_2_to_2()
        {
            var input = 2;

            string expected = "2";
            string actual = ToTest.Convert(input);

            Assert.AreEqual(expected, actual, $"Fizzbuzz({input}) does not return {expected}.");
        }
    }
}