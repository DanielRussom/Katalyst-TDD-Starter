﻿using Katalyst_TDD_Starter.PasswordValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katalyst_TDD_Starter.Test.PasswordValidation
{
    [TestClass]
    public class PasswordValidatorBuilderTests
    {
        public PasswordValidatorBuilder UnderTest { get; set; }

        public PasswordValidatorBuilderTests()
        {
            UnderTest = new PasswordValidatorBuilder();
        }

        [TestMethod ("Builder without any config set")]
        public void Building_the_validator_should_return_a_validator_object_with_default_settings()
        {
            var result = UnderTest.Build();

            Assert.AreEqual(result.InputLength, 9);
            Assert.IsTrue(result.RequireCapitalLetter);
            Assert.IsTrue(result.RequireLowercaseLetter);
            Assert.IsTrue(result.RequireUnderscore);
            Assert.IsTrue(result.RequireNumericCharacter);
        }

        [TestMethod ("Configured Input Length tests")]
        [DataRow (5, DisplayName = "Set InputLength to 5")]
        [DataRow (16, DisplayName = "Set InputLength to 16")]
        public void Building_with_input_length_should_have_value_set(int inputLength)
        {
            var result = UnderTest.WithInputLength(inputLength).Build();

            Assert.AreEqual(inputLength, result.InputLength);
        }

        [TestMethod("Configured Capital Letter check tests")]
        [DataRow(false, DisplayName = "Disable capital letter checks")]
        [DataRow(true, DisplayName = "Enable capital letter checks")]
        public void Building_with_capital_setting_should_have_value_set(bool input)
        {
            var result = UnderTest.WithRequiredCapital(input).Build();

            Assert.AreEqual(input, result.RequireCapitalLetter);
        }

        [TestMethod("Configured Lowercase Letter check tests")]
        [DataRow(false, DisplayName = "Disable lowercase letter checks")]
        [DataRow(true, DisplayName = "Enable lowercase letter checks")]
        public void Building_with_lowercase_setting_should_have_value_set(bool input)
        {
            var result = UnderTest.WithRequiredLowercase(input).Build();

            Assert.AreEqual(input, result.RequireLowercaseLetter);
        }

        [TestMethod("Configured Numeric Character check tests")]
        [DataRow(false, DisplayName = "Disable numeric character checks")]
        [DataRow(true, DisplayName = "Enable numeric character checks")]
        public void Building_with_numeric_setting_should_have_value_set(bool input)
        {
            var result = UnderTest.WithRequiredNumericCharacter(input).Build();

            Assert.AreEqual(input, result.RequireNumericCharacter);
        }

        [TestMethod("Configured Underscore check tests")]
        [DataRow(false, DisplayName = "Disable underscore character checks")]
        [DataRow(true, DisplayName = "Enable underscore character checks")]
        public void Building_with_underscore_setting_should_have_value_set(bool input)
        {
            var result = UnderTest.WithRequiredUnderscore(input).Build();

            Assert.AreEqual(input, result.RequireUnderscore);
        }
    }
}
