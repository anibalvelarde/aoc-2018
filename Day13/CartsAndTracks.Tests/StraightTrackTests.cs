using System;
using CartsAndTracks.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CartsAndTracks.Tests
{
    [TestClass]
    public class StraightTrackTests
    {
        [TestMethod]
        public void should_read_track_data_with_initial_cart_position()
        {
            // arrange
            var t = new Track(@"TestData\StraightLineTrack.txt");

            // act
            t.Load();

            // assert
            t.Render();
            Assert.AreEqual(7, t.Length);
            Assert.AreEqual(1, t.Width);
            Assert.AreEqual(2, t.Carts.Count);
            Assert.IsTrue(t.Carts[0].CurrentPosition.Equals(new Coordinates(1, 0)));
            Assert.AreEqual(Heading.South, t.Carts[0].CurrentHeading);
            Assert.IsTrue(t.Carts[1].CurrentPosition.Equals(new Coordinates(5, 0)));
            Assert.AreEqual(Heading.North, t.Carts[1].CurrentHeading);
        }
    }
}
