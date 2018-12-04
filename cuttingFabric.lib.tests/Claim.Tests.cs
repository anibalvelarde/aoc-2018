using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cuttingFabric.lib.tests
{
    [TestClass]
    public class ClaimTests
    {
        [TestMethod]
        public void should_initialize_correctly_when_given_minimum_init_string()
        {
            // Arange...
            var inputString = "#1 @ 0,0: 1x1";
            var expOffset = new OffSet(0, 0);

            // Act...
            var c = new Claim(inputString);

            // Assert...
            Assert.AreEqual(1, c.id);
            Assert.AreEqual(1, c.width);
            Assert.AreEqual(1, c.length);
            Assert.AreEqual(expOffset.x, c.claimOffset.x);
            Assert.AreEqual(expOffset.y, c.claimOffset.y);
        }

        [TestMethod]
        public void should_initialize_correctly_when_given_wellformed_init_string()
        {
            // Arange...
            var inputString = "#1 @ 432,394: 29x14";
            var expOffset = new OffSet(394, 432);

            // Act...
            var c = new Claim(inputString);

            // Assert...
            Assert.AreEqual(1, c.id);
            Assert.AreEqual(29, c.width);
            Assert.AreEqual(14, c.length);
            Assert.AreEqual(expOffset.x, c.claimOffset.x);
            Assert.AreEqual(expOffset.y, c.claimOffset.y);
        }
    }
}
