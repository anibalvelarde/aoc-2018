using System;
using System.Collections.Generic;
using System.Linq;

namespace SleepingGuardHabits.lib
{
    public class Dashboard
    {
        private readonly Dictionary<int, Guard> _guards = new Dictionary<int, Guard>();
        private Guard _lastGuard;

        public Dictionary<int, Guard> Guards
        {
            get
            {
                return _guards;
            }
        }

        internal void AddGuard(Guard g)
        {
            if (!_guards.ContainsKey(g.id))
            {
                _guards.Add(g.id, g);
                _lastGuard = g;
            } else
            {
                AddGuardEvents(g);
            }
        }

        internal void AddGuardEvents(Guard g)
        {
            _lastGuard = _guards[g.id];
            foreach (var guardEvent in g.Events)
            {
                _lastGuard.AddEvent(guardEvent.Value);
            }
        }

        internal void AddEvent(string data)
        {
            if (_lastGuard is null)
            {
                if (this.Guards.Count.Equals(1))
                {
                    _lastGuard = this.Guards.Values.First();
                } else
                {
                    throw new ArgumentNullException("Cannot add event on a dashboard that has not had any Guards added to it.");
                }
            }
            _lastGuard.AddEvent(data);
        }

        public Guard GetSleepiestGuard()
        {
            Guard sleepyGuard = null;

            foreach (var g in Guards)
            {
                if (sleepyGuard is null)
                {
                    sleepyGuard = g.Value;
                } else
                {
                    if (sleepyGuard.CalcTimeAsleep()< g.Value.CalcTimeAsleep())
                    {
                        sleepyGuard = g.Value;
                    }
                }
            }

            return sleepyGuard;
        }

        public Guard GetGuardMostAsleepOnSameMinute()
        {
            Guard sleepyGuard = null;

            foreach (var g in Guards)
            {
                if (sleepyGuard is null)
                {
                    sleepyGuard = g.Value;
                }
                else
                {
                    if (sleepyGuard.CalcMinuteAsleepTheMost().frequencyOfSleep < g.Value.CalcMinuteAsleepTheMost().frequencyOfSleep)
                    {
                        sleepyGuard = g.Value;
                    }
                }
            }

            return sleepyGuard;
        }

        public int CalcStrategyOneAnswer()
        {
            return this.GetSleepiestGuard().id * this.GetSleepiestGuard().CalcMinuteAsleepTheMost().minuteId;
        }

        public int CalcStrategyTwoAnswer()
        {
            return this.GetGuardMostAsleepOnSameMinute().id * this.GetGuardMostAsleepOnSameMinute().CalcMinuteAsleepTheMost().minuteId;
        }

        public void DisplayAllInfo()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine();
            foreach (var g in _guards)
            {
                Console.WriteLine();
                DisplayInfo(g.Value);
            }
            Console.WriteLine("==============================================");
        }

        private void DisplayInfo(Guard g)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Sleepiest Guard: {g.id}");
            Console.WriteLine($"Sleepiest Minute: {g.CalcMinuteAsleepTheMost()}");
            Console.WriteLine($"Total Time Asleep:   {g.CalcTimeAsleep()}");
            Console.WriteLine("----------------------------------------------");
        }
    }
}