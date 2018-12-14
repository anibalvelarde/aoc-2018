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
            Assert.AreEqual(7, t.Width);
            Assert.AreEqual(1, t.Length);
            Assert.AreEqual(2, t.Carts.Count);
            Assert.AreEqual(new Coordinates(0, 1), t.Carts[0].CurrentPosition);
            Assert.AreEqual(Heading.South, t.Carts[0].CurrentHeading);
            Assert.AreEqual(new Coordinates(0, 5), t.Carts[1].CurrentPosition);
            Assert.AreEqual(Heading.North, t.Carts[1].CurrentHeading);
        }
    }
}
