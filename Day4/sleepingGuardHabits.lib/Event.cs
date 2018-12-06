using System;

namespace SleepingGuardHabits.lib
{
    public class Event
    {
        public Action act { get; private set; }
        public DateTime dateTimeStamp { get; private set; }
        public long Ticks
        {
            get
            {
                return this.dateTimeStamp.Ticks;
            }
        }

        public Event(Action act, DateTime dateTimeStamp)
        {
            this.act = act;
            this.dateTimeStamp = dateTimeStamp;
        }
    }
}