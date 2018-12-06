using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepingGuardHabits.lib;

namespace sleepingGuardHabits
{
    class Program
    {
        static void Main(string[] args)
        {
            // Arange
            var eventList = File.ReadAllLines(@"data\OrderedData.txt");
            var factory = new SleepingGuardHabitsFactory();

            foreach (var testEvent in eventList)
            {
                factory.AddDataPoint(testEvent);
            }
            var db = factory.GetDashboard();

            db.DisplayAllInfo();

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Sleepiest Guard: {db.GetSleepiestGuard().id}");
            Console.WriteLine($"Sleepiest Minute: {db.GetSleepiestGuard().CalcMinuteAsleepTheMost()}");
            Console.WriteLine($"Answer for Strategy #1:   {db.CalcStrategyOneAnswer()}");
            Console.WriteLine($"Amount of time asleep on most common minute {db.GetSleepiestGuard().CalcMinuteAsleepTheMost().frequencyOfSleep}");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Sleepiest Guard: {db.GetGuardMostAsleepOnSameMinute().id}");
            Console.WriteLine($"Sleepiest Minute: {db.GetGuardMostAsleepOnSameMinute().CalcMinuteAsleepTheMost().minuteId}");
            Console.WriteLine($"Answer for Strategy #1:   {db.CalcStrategyTwoAnswer()}");
            Console.ReadKey();
        }
    }
}
