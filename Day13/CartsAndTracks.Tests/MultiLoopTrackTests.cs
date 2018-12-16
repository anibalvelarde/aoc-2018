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
            var t = new Track(@"TestData\MultipleLoopsMultipleCarts.txt", cleanUpCrashSetting: false, ticksLimit: ticksLimit);
            t.Load();

            // act
            t.Simulate();

            // assert
            Console.WriteLine(t.Render());
            Assert.AreEqual(2, t.Carts.Count);
            Assert.AreEqual(new Coordinates(7, 3), t.Carts[0].CurrentPosition);
            Assert.IsTrue(t.Carts[0].IsCrashed);
            Assert.AreEqual(new Coordinates(7, 3), t.Carts[1].CurrentPosition);
            Assert.IsTrue(t.Carts[1].IsCrashed);
        }

        [TestMethod]
        public void should_just_work_part_ii()
        {
            // arrange
            int ticksLimit = 100000;
            var t = new Track(@"TestData\MultipleLoopsMultipleCartsPart2.txt", ticksLimit: ticksLimit);
            t.Load();

            // act
            t.Simulate();

            // assert
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(t.CrashReport());
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(t.Carts[0].ToString());
            Console.WriteLine($"  Total Ticks: {t.TotalTicks}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("");
            Console.WriteLine(t.Render());
            Assert.AreEqual(1, t.Carts.Count);
        }
    }
}
