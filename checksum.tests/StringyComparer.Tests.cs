using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using checksum.lib;

namespace checksum.tests
{
    [TestClass]
    public class StringyComparerTests
    {
        [TestMethod]
        public void should_return_1_when_2_strings_differ_by_exactly_one_character_in_the_same_position()
        {
            // Arange
            var string1 = "fghijmp";
            var string2 = "fguijmp";
            var checker = new StringyComparer(string1, string2);

            // Act
            var difference = checker.DifferentCharacterCount();

            // Assert
            Assert.AreEqual(1, difference);
        }

        [TestMethod]
        public void should_return_0_when_2_strings_differ_by_exactly_one_character_in_different_positions()
        {
            // Arange
            var string1 = "fghjpm";
            var string2 = "fgijmp";
            var checker = new StringyComparer(string1, string2);

            // Act
            var difference = checker.DifferentCharacterCount();

            // Assert
            Assert.AreEqual(1, difference);
        }

        [TestMethod]
        public void should_return_string_with_common_characters()
        {
            // Arange
            var string1 = "fghijmpd";
            var string2 = "fguijmpd";
            var checker = new StringyComparer(string1, string2);

            // Act
            var commons = checker.ExtractCommonCharacters();

            // Assert
            Assert.AreEqual("fgijmpd", commons);
        }
    }
}
