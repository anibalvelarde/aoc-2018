using System;
using CartsAndTracks.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CartsAndTracks.Tests
{
    [TestClass]
    public class MultiLoopTrackTests
    {
        [TestMethod]
        public void should_just_work()
        {
            // arrange
            int ticksLimit = 100000;
            var t = new Track(@"TestData\MultipleLoopsMultipleCarts.txt");
            t.Load();

            // act
            t.Simulate(ticksLimit);

            // assert
            t.Render();
            Assert.AreEqual(2, t.Carts.Count);
        }
    }
}
