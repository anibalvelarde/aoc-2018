using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectedGraph.Lib
{
    public class Worker
    {
        private Dictionary<char,int> taskDuration = new Dictionary<char, int>();
        public int TicksBusy { get; private set; }
        public int TicksIdle { get; private set; }
        private int BusyTicks { get; set; }
        public Vertex CurrentTask { get; private set; }

        public Worker(int durationOffset = 1)
        {
            InitializeWorker(durationOffset);
            this.TicksBusy = 0;
            this.TicksIdle = 0;
        }

        public bool IsBusy()
        {
            return BusyTicks > 0;
        }

        public bool NotBusy()
        {
            return !IsBusy();
        }

        private void InitializeWorker(int durationOffset)
        {
            var k = "abcdefghijklmnopqrstuvwxyz"
                    .ToUpper()
                    .ToCharArray();
            for (int i = 0; i < 25; i++)
            {
                taskDuration.Add(k[i], (i + durationOffset + 1));
            }
        }

        public void AssignTask(Vertex task)
        {
            if (IsBusy())
            {
                throw new Exception("Can't work on a new task when busy.");
            }
            {
                this.CurrentTask = task;
                this.BusyTicks = SetDuration();
            }
        }

        public void Tick()
        {
            if (IsBusy())
            {
                TicksBusy++;
                BusyTicks--;
                if (NotBusy())
                {
                    CurrentTask.IsDone = true;
                    CurrentTask = null;
                }
            } else
            {
                TicksIdle++;
            }
        }

        private int SetDuration()
        { 
            taskDuration.TryGetValue(CurrentTask.Id, out int duration);
            return duration;
        }
    }
}
