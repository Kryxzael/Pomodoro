using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro
{
    public class PomodoroCycle
    {
        public int SecondProgress { get; private set; }
        public float MaxWorkMinutes { get; set; }
        public float MaxBreakMinutes { get; set; }
        public bool InBreak { get; private set; }

        private float MaxMinutes
        {
            get
            {
                if (InBreak)
                {
                    return MaxBreakMinutes;
                }
                else
                {
                    return MaxWorkMinutes;
                }
            }
        }

        public PomodoroCycle(float maxWorkMinutes, float maxBreakMinutes)
        {
            MaxWorkMinutes = maxWorkMinutes;
            MaxBreakMinutes = maxBreakMinutes;
        }

        public bool Tick()
        {
            if (++SecondProgress >= MaxMinutes * 60)
            {
                if (InBreak)
                {
                    return true;
                }
                else
                {
                    SecondProgress = 0;
                    InBreak = true;
                }
            }

            return false;
        }
    }
}
