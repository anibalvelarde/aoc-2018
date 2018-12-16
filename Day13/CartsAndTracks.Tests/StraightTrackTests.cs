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
            var t = new Track(@"TestData\StraightNorthSouthLineTrack.txt");

            // act
            t.Load();

            // assert
            t.Render();
            Assert.AreEqual(1, t.Columns);
            Assert.AreEqual(7, t.Rows);
            Assert.AreEqual(2, t.Carts.Count);
            Assert.IsTrue(t.Carts[0].CurrentPosition.Equals(new Coordinates(0, 1)));
            Assert.AreEqual(Heading.South, t.Carts[0].CurrentHeading);
            Assert.IsTrue(t.Carts[1].CurrentPosition.Equals(new Coordinates(0, 5)));
            Assert.AreEqual(Heading.North, t.Carts[1].CurrentHeading);
        }

        [TestMethod]
        public void should_move_carts_one_step_when_one_tick_happens()
        {
            // arrange
            var t = new Track(@"TestData\StraightNorthSouthLineTrack.txt", cleanUpCrashSetting: false);
            t.Load();

            // act
            t.Tick();

            // assert
            t.Render();
            Assert.IsTrue(t.Carts[0].CurrentPosition.Equals(new Coordinates(0, 2)));
            Assert.AreEqual(Heading.South, t.Carts[0].CurrentHeading);
            Assert.IsTrue(t.Carts[1].CurrentPosition.Equals(new Coordinates(0, 4)));
            Assert.AreEqual(Heading.North, t.Carts[1].CurrentHeading);
        }

        [TestMethod]
        public void should_move_carts_till_crash_is_detected_on_single_lane_North_Shouth_track()
        {
            // arrange
            var t = new Track(@"TestData\StraightNorthSouthLineTrack.txt", cleanUpCrashSetting: false, ticksLimit: 20);
            t.Load();

            // act
            t.Simulate();

            // assert
            t.Render();
            Assert.IsTrue(t.Carts[0].CurrentPosition.Equals(t.Carts[1].CurrentPosition));
            Assert.AreEqual(Heading.South, t.Carts[0].CurrentHeading);
            Assert.AreEqual(Heading.North, t.Carts[1].CurrentHeading);
        }

        [TestMethod]
        public void should_move_carts_till_crash_is_detected_on_single_lane_East_West_track()
        {
            // arrange
            var t = new Track(@"TestData\StraightEastWestLineTrack.txt", false, 20);
            t.Load();

            // act
            t.Simulate();

            // assert
            t.Render();
            Assert.IsTrue(t.Carts[0].CurrentPosition.Equals(t.Carts[1].CurrentPosition));
            Assert.AreEqual(new Coordinates(4, 0), t.Carts[0].CurrentPosition);
            Assert.AreEqual(Heading.East, t.Carts[0].CurrentHeading);
            Assert.AreEqual(Heading.West, t.Carts[1].CurrentHeading);
        }
    }
}
