using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using checksum.lib;

namespace checksum.tests
{
    [TestClass]
    public class IdCheckerTests
    {
        [TestMethod]
        public void should_initialize_correctly()
        {
            // Arrange

            // Act
            var checker = new IdChecker();

            // Assert
            Assert.AreEqual(0, checker.GetCheckSum());
        }

        [TestMethod]
        public void should_find_match_for_no_repeats()
        {
            // Arrange
            var sample = "abcdef";
            var checker = new IdChecker();

            // Act
            checker.CheckIdForRepeat(sample, 2);
            checker.CheckIdForRepeat(sample, 3);

            // Assert
            Assert.AreEqual(0, checker.GetCheckSum());
        }

        [TestMethod]
        public void should_find_match_for_2_repeats()
        {
            // Arrange
            var sample = "abbcde";
            var checker = new IdChecker();

            // Act
            checker.CheckIdForRepeat(sample, 2);
            checker.CheckIdForRepeat(sample, 3);

            // Assert
            Assert.AreEqual(1, checker.GetCountForRepeats(2));
            Assert.AreEqual(0, checker.GetCountForRepeats(3));
            Assert.AreEqual(0, checker.GetCheckSum());
        }

        [TestMethod]
        public void should_find_match_for_3_repeats()
        {
            // Arrange
            var sample = "abbcdeb";
            var checker = new IdChecker();

            // Act
            checker.CheckIdForRepeat(sample, 2);
            checker.CheckIdForRepeat(sample, 3);

            // Assert
            Assert.AreEqual(1, checker.GetCountForRepeats(3));
            Assert.AreEqual(0, checker.GetCountForRepeats(2));
            Assert.AreEqual(0, checker.GetCheckSum());
        }

        [TestMethod]
        public void should_find_match_for_2_and_3_repeats_once()
        {
            // Arrange
            var sample = "ebccdebc";
            var checker = new IdChecker();

            // Act
            checker.CheckIdForRepeat(sample, 2);
            checker.CheckIdForRepeat(sample, 3);

            // Assert
            Assert.AreEqual(1, checker.GetCountForRepeats(3));
            Assert.AreEqual(1, checker.GetCountForRepeats(2));
            Assert.AreEqual(1, checker.GetCheckSum());
        }

        [TestMethod]
        public void should_find_match_for_2_and_3_repeats()
        {
            // Arange
            var sample = "bababc";
            var checker = new IdChecker();

            // Act
            checker.CheckIdForRepeat(sample, 2);
            checker.CheckIdForRepeat(sample, 3);

            // Assert
            Assert.AreEqual(1, checker.GetCountForRepeats(2));
            Assert.AreEqual(1, checker.GetCountForRepeats(3));
            Assert.AreEqual(1, checker.GetCheckSum());
        }

        [TestMethod]
        public void should_find_match_for_2_and_3_repeats_for_multiple_samples()
        {
            // Arange
            var sample1 = "abcdef";
            var sample2 = "bababc";
            var sample3 = "abbcde";
            var sample4 = "abcccd";
            var sample5 = "aabcdd";
            var sample6 = "abcdee";
            var sample7 = "ababab";
            var checker = new IdChecker();

            // Act
            checker.CheckIdForRepeat(sample1, 2);
            checker.CheckIdForRepeat(sample1, 3);
            checker.CheckIdForRepeat(sample2, 2);
            checker.CheckIdForRepeat(sample2, 3);
            checker.CheckIdForRepeat(sample3, 2);
            checker.CheckIdForRepeat(sample3, 3);
            checker.CheckIdForRepeat(sample4, 2);
            checker.CheckIdForRepeat(sample4, 3);
            checker.CheckIdForRepeat(sample5, 2);
            checker.CheckIdForRepeat(sample5, 3);
            checker.CheckIdForRepeat(sample6, 2);
            checker.CheckIdForRepeat(sample6, 3);
            checker.CheckIdForRepeat(sample7, 2);
            checker.CheckIdForRepeat(sample7, 3);

            // Assert
            Assert.AreEqual(4, checker.GetCountForRepeats(2));
            Assert.AreEqual(3, checker.GetCountForRepeats(3));
            Assert.AreEqual(12, checker.GetCheckSum());
        }
    }
}
