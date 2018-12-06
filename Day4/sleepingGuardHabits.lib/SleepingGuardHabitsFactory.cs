using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SleepingGuardHabits.lib
{
    public enum Action { None, beginShift, fallAsleep, weakUp };

    public class SleepingGuardHabitsFactory
    {
        Dashboard _dashboard = null;

        public SleepingGuardHabitsFactory(Dashboard d = null)
        {
            if (d is null)
            {
                _dashboard = new Dashboard();
            } else
            {
                _dashboard = d;
            }
        }
        public Dashboard GetDashboard()
        {
            return _dashboard;
        }

        public void AddDataPoint(string data)
        {
            if (_dashboard is null)
            {
                _dashboard = new Dashboard(); 
            }
            if (data.Contains("begins shift"))
            {
                var g = this.MakeGuardInfo(data);
                if (_dashboard.Guards.ContainsKey(g.id))
                {
                    _dashboard.AddGuardEvents(g);
                } else
                {
                    _dashboard.AddGuard(g);
                }
            } else
            {
                _dashboard.AddEvent(data);
            }


        }

        private Guard MakeGuardInfo(string data)
        {
            var pattern = MakeGuardPattern(data);
            int year = int.Parse(pattern.Groups["year"].Value);
            int month = int.Parse(pattern.Groups["month"].Value);
            int day = int.Parse(pattern.Groups["day"].Value);
            int hour = int.Parse(pattern.Groups["hour"].Value);
            int minute = int.Parse(pattern.Groups["minutes"].Value);
            int id = int.Parse(pattern.Groups["id"].Value);

            var g = new Guard(id);
            g.AddEvent(Action.beginShift, new DateTime(year, month, day, hour, minute, 0));

            return g;
        }

        private Match MakeGuardPattern(string input)
        {
            Regex pattern = new Regex($@"{Regex.Escape("[")}(?<year>\d+)-(?<month>\d+)-(?<day>\d+) (?<hour>\d+):(?<minutes>\d+){Regex.Escape("]")} Guard #(?<id>\d+) begins shift$");
            return pattern.Match(input);
        }
    }
}
