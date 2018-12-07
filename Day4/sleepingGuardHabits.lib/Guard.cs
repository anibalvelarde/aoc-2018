using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SleepingGuardHabits.lib
{

    public class Guard
    {
        private readonly SortedDictionary<long, Event> _events = new SortedDictionary<long, Event>();
        public Guard(int id)
        {
            this.id = id;
        }

        public int id { get; private set; }

        public long CalcTimeAsleep()
        {
            Action lastAction = Action.None;
            long startTick = 0; long endTick = 0;
            long minutesAsleep = 0;

            foreach (var eventItem in _events)
            {
                switch (eventItem.Value.act)
                {
                    case Action.beginShift:
                    case Action.weakUp:
                        if (lastAction.Equals(Action.fallAsleep))
                        {
                            endTick = eventItem.Value.Ticks;
                            minutesAsleep += new TimeSpan(endTick - startTick).Minutes;
                            startTick = 0;endTick = 0;
                        }
                        break;
                    case Action.fallAsleep:
                        if (lastAction.Equals(Action.None) || lastAction.Equals(Action.beginShift) || lastAction.Equals(Action.weakUp))
                        {
                            startTick = eventItem.Value.Ticks;
                        } 
                        break;
                    default:
                        break;
                }
                lastAction = eventItem.Value.act;
            }

            return minutesAsleep;
        }

        public CommonMinute CalcMinuteAsleepTheMost()
        {
            int[] minutes = new int[60];
            Action lastAction = Action.None;
            int beginMin = 0; int endMin = 0;

            foreach (var eventItem in _events)
            {
                switch (eventItem.Value.act)
                {
                    case Action.beginShift:
                    case Action.weakUp:
                        if (lastAction.Equals(Action.fallAsleep))
                        {
                            endMin = eventItem.Value.dateTimeStamp.Minute-1;
                            for (int i = beginMin; i <= endMin; i++)
                            {
                                minutes[i] += 1;
                            }
                            beginMin = 0; endMin = 0;
                        }
                        break;
                    case Action.fallAsleep:
                        if (lastAction.Equals(Action.None) || lastAction.Equals(Action.beginShift) || lastAction.Equals(Action.weakUp))
                        {
                            beginMin = eventItem.Value.dateTimeStamp.Minute;
                        }
                        break;
                    default:
                        break;
                }
                lastAction = eventItem.Value.act;
            }

            return FindMostCommonMinuteToBeAsleep(minutes);
        }

        private CommonMinute FindMostCommonMinuteToBeAsleep(int[] minutes)
        {
            CommonMinute cm = new CommonMinute();

            for (int i = 0; i < 60; i++)
            {
                if (minutes[i] > cm.frequencyOfSleep)
                {

                    cm.frequencyOfSleep = minutes[i];
                    cm.minuteId = i;
                }
            }

            return cm;
        }

        internal void AddEvent(Action act, DateTime dateTimeStamp)
        {
           var newEvent = new Event(act, dateTimeStamp);
            this.AddEvent(newEvent);
        }

        internal void AddEvent(Event e)
        {
            if (!_events.ContainsKey(e.Ticks))
            {
                _events.Add(e.Ticks, e);
            }
        }

        internal void AddEvent(string eventData)
        {
            var pattern = MakeEventPattern(eventData);
            int year = int.Parse(pattern.Groups["year"].Value);
            int month = int.Parse(pattern.Groups["month"].Value);
            int day = int.Parse(pattern.Groups["day"].Value);
            int hour = int.Parse(pattern.Groups["hour"].Value);
            int minute = int.Parse(pattern.Groups["minutes"].Value);
            string action = pattern.Groups["action"].Value;
            Event e = null;

            switch (action)
            {
                case "falls asleep":
                    e = new Event(Action.fallAsleep, new DateTime(year, month, day, hour, minute, 0));
                    break;
                case "wakes up":
                    e = new Event(Action.weakUp, new DateTime(year, month, day, hour, minute, 0));
                    break;
                default:
                    throw new ArgumentException($"Invalid action [{action}] was detected for DateTimeStamp: [{new DateTime(year, month, day, hour, minute, 0).ToLongTimeString()}]");
            }

            _events.Add(e.Ticks, e);
        }

        public SortedDictionary<long, Event> Events
        {
            get
            {
                return _events;
            }
        }

        private Match MakeEventPattern(string input)
        {
            Regex pattern = null;
            pattern = new Regex($@"{Regex.Escape("[")}(?<year>\d+)-(?<month>\d+)-(?<day>\d+) (?<hour>\d+):(?<minutes>\d+){Regex.Escape("]")} (?<action>\w+ \w+)");
            return pattern.Match(input);
        }
    }
}