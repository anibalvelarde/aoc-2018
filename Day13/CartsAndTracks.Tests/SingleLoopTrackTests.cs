using System;
using CartsAndTracks.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CartsAndTracks.Tests
{
    [TestClass]
    public class SingleLoopTrackTests
    {
        [TestMethod]
        public void should_move_single_cart_around_single_loop_track()
        {
            // arrange
            int ticksLimit = 16;
            var t = new Track(@"TestData\SingleLoopTrackSingleCart.txt");
            t.Load();
            var expPosKey = t.Carts[0].CurrentPosition.ToKey();

            // act
            t.Simulate(ticksLimit);

            // assert
            t.Render();
            Assert.AreEqual(1, t.Carts.Count);
            Assert.AreEqual(Heading.West, t.Carts[0].CurrentHeading);
            Assert.AreEqual(expPosKey, t.Carts[0].CurrentPosition.ToKey());
            Assert.AreEqual(Heading.West, t.Carts[0].CurrentHeading);
            Assert.AreEqual(ticksLimit, t.TotalTicks);
        }

        [TestMethod]
        public void should_stop_running_when_tick_limit_is_met()
        {
            // arrange
            int ticksLimit = 16;
            var t = new Track(@"TestData\SingleLoopTrackNoCarts.txt");
            t.Load();

            // act
            t.Simulate(ticksLimit);

            // assert
            t.Render();
            Assert.AreEqual(ticksLimit, t.TotalTicks);
            Assert.AreEqual(0, t.Carts.Count);
            Assert.AreEqual(6, t.Width);
            Assert.AreEqual(4, t.Length);
        }
    }
}
