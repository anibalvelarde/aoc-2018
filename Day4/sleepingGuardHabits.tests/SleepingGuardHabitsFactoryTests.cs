using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SleepingGuardHabits.lib;

namespace SleepingGuardHabits.tests
{
    [TestClass]
    public class SleepingGuardHabitsFactoryTests
    {
        [TestMethod]
        public void should_create_well_formed_objects_for_beging_shift_input_data()
        {
            // arange
            var data = "[1518-11-01 00:00] Guard #10 begins shift";
            var factory = new SleepingGuardHabitsFactory();
            factory.AddDataPoint(data);

            // act
            var dashBoard = factory.GetDashboard();

            // assert
            Assert.AreEqual(1, dashBoard.Guards.Count);
        }

        [TestMethod]
        public void should_create_well_formed_objects_for_multiple_guard_beging_shift_input_data()
        {
            // arange
            var data1 = "[1518-11-01 00:00] Guard #10 begins shift";
            var sleep1 = "[1518-11-01 00:05] falls asleep";
            var weakup1 = "[1518-11-01 00:25] wakes up";
            var data2 = "[1518-11-01 02:00] Guard #11 begins shift";
            var sleep2 = "[1518-11-02 00:40] falls asleep";
            var weakup2 = "[1518-11-02 00:50] wakes up";
            var factory = new SleepingGuardHabitsFactory();
            factory.AddDataPoint(data1);
            factory.AddDataPoint(sleep1);
            factory.AddDataPoint(weakup1);
            factory.AddDataPoint(data2);
            factory.AddDataPoint(sleep2);
            factory.AddDataPoint(weakup2);

            // act
            var dashBoard = factory.GetDashboard();

            // assert
            Assert.AreEqual(2, dashBoard.Guards.Count);
            Assert.IsTrue(dashBoard.Guards.ContainsKey(10));
            Assert.IsTrue(dashBoard.Guards.ContainsKey(11));
            dashBoard.Guards.TryGetValue(10, out Guard g10);
            dashBoard.Guards.TryGetValue(11, out Guard g11);
            Assert.IsNotNull(g10);
            Assert.IsNotNull(g11);
            Assert.AreEqual(3, g10.Events.Count);
            Assert.AreEqual(3, g11.Events.Count);
        }

        [TestMethod]
        public void should_create_well_formed_objects_for_multiple_beging_shift_input_data()
        {
            // arange
            var data1 = "[1518-11-01 00:00] Guard #10 begins shift";
            var data2 = "[1518-11-01 02:00] Guard #11 begins shift";
            var data3 = "[1518-11-01 04:00] Guard #12 begins shift";
            var factory = new SleepingGuardHabitsFactory();
            factory.AddDataPoint(data1);
            factory.AddDataPoint(data2);
            factory.AddDataPoint(data3);

            // act
            var dashBoard = factory.GetDashboard();

            // assert
            Assert.AreEqual(3, dashBoard.Guards.Count);
            Assert.IsTrue(dashBoard.Guards.ContainsKey(10));
            Assert.IsTrue(dashBoard.Guards.ContainsKey(11));
            Assert.IsTrue(dashBoard.Guards.ContainsKey(12));
        }

        [TestMethod]
        public void should_process_the_sample_data_correctly()
        {
            // Arange
            var eventList = File.ReadAllLines(@"data\testData.txt");
            var factory = new SleepingGuardHabitsFactory();

            foreach (var testEvent in eventList)
            {
                factory.AddDataPoint(testEvent);
            }
            var db = factory.GetDashboard();

            Assert.AreEqual(2, db.Guards.Count);
            Assert.AreEqual(8, db.Guards[10].Events.Count);
            Assert.AreEqual(9, db.Guards[99].Events.Count);

            Console.WriteLine();
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Guard ID: {db.Guards[10].id} was asleep for [{db.Guards[10].CalcTimeAsleep()}] minutes.");
            Console.WriteLine($"Most Common Minute To Be Asleep: {db.Guards[10].CalcMinuteAsleepTheMost().minuteId}");
            Console.WriteLine($"Guard ID: {db.Guards[99].id} was asleep for [{db.Guards[99].CalcTimeAsleep()}] minutes.");
            Console.WriteLine($"Most Common Minute To Be Asleep: {db.Guards[99].CalcMinuteAsleepTheMost().minuteId}");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Sleepiest Guard: {db.GetSleepiestGuard().id}");
            Console.WriteLine($"Sleepiest Minute: {db.GetSleepiestGuard().CalcMinuteAsleepTheMost().minuteId}");
            Console.WriteLine($"Answer for Strategy #1:   {db.CalcStrategyOneAnswer()}");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Sleepiest Guard: {db.GetGuardMostAsleepOnSameMinute().id}");
            Console.WriteLine($"Sleepiest Minute: {db.GetGuardMostAsleepOnSameMinute().CalcMinuteAsleepTheMost().minuteId}");
            Console.WriteLine($"Answer for Strategy #1:   {db.CalcStrategyTwoAnswer()}");
        }
    }
}
