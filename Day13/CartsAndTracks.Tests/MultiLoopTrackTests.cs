﻿using System;
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

        [TestMethod]
        public void should_just_work_part_ii()
        {
            // arrange
            int ticksLimit = 100000;
            var t = new Track(@"TestData\MultipleLoopsMultipleCartsPart2.txt");
            t.Load();

            // act
            t.Simulate(ticksLimit);

            // assert
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(t.CrashReport());
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(t.Carts[0].ToString());
            Console.WriteLine($"  Total Ticks: {t.TotalTicks}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("");
            Assert.AreEqual(1, t.Carts.Count);
        }
    }
}
