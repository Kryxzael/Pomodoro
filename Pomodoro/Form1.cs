using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bars;
using CircleControl;

namespace Pomodoro
{
    public partial class Form1 : Form
    {
        private CircleProgressControl _progress;
        private Timer _updateTimer = new Timer() { Interval = 1000 };
        private static Random _rng = new Random();

        public bool Running
        {
            get
            {
                return _updateTimer.Enabled;
            }
            set
            {
                _updateTimer.Enabled = value;
            }
        }
        public bool Started { get; private set; }


        public int CycleCount { get; set; }
        public PomodoroCycle CurrentCycle { get; private set; }
        public Queue<PomodoroCycle> WaitingCycles { get; } = new Queue<PomodoroCycle>();

        public Form1()
        {
            InitializeComponent();
            _lastColorIndex = _rng.Next(360);
            _progress = new CircleProgressControl()
            {
                Dock = DockStyle.Fill,
                MaxValue = 25,
                Value = 0,
                ForeColor = Properties.Resources.Spectre.GetPixel(_lastColorIndex, 0)
            };

            Controls.Add(_progress);
            lblStatus.Parent = _progress;
            lblStatus.BackColor = Color.Transparent;
            _updateTimer.Tick += TimerTick;
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width, Screen.PrimaryScreen.WorkingArea.Height - Height);
        }

        public void CreateCycles()
        {
            for (int i = 0; i < 3; i++)
            {
                WaitingCycles.Enqueue(new PomodoroCycle(25f, 5f));
            }

            WaitingCycles.Enqueue(new PomodoroCycle(25f, 30f));
        }


        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!Started)
            {                
                Started = true;
                Running = true;
                _updateTimer.Start();
                lblStatus.Text = "Starting";
            }
            else
            {
                if (Running)
                {
                    lblStatus.Text = "Paused";
                    Running = false;
                }
                else
                {
                    lblStatus.Text = "Continuing";
                    Running = true;
                }
            }

        }

        private int _lastColorIndex;
        private void TimerTick(object sender, EventArgs e)
        {

            if (CurrentCycle == null || CurrentCycle.Tick())
            {

                if (!WaitingCycles.Any())
                {
                    CreateCycles();
                }

                CurrentCycle = WaitingCycles.Dequeue();

                CycleCount++;

                _progress.Value = 0f;
                _progress.MaxValue = 25f;
            }

            

            if (CurrentCycle.InBreak)
            {
                _lastColorIndex = (_lastColorIndex + 3) % 360;


                _progress.MaxValue = CurrentCycle.MaxBreakMinutes;

                _progress.ForeColor = Properties.Resources.Spectre.GetPixel(_lastColorIndex, 0);
                _progress.Value = _progress.MaxValue - CurrentCycle.SecondProgress / 60f;

                Text = new TimeSpan(0, 0, (int)(CurrentCycle.MaxBreakMinutes * 60) - CurrentCycle.SecondProgress).ToString("mm':'ss") + " - Break";
                lblStatus.Text = new TimeSpan(0, 0, (int)(CurrentCycle.MaxBreakMinutes * 60) - CurrentCycle.SecondProgress).ToString("mm':'ss") + "\r\n" + CycleCount.ToString();
            }
            else
            {

                if (CurrentCycle.SecondProgress % 60 == 0)
                {
                    _lastColorIndex = (_lastColorIndex + 1) % 360;
                    _progress.ForeColor = Properties.Resources.Spectre.GetPixel(_lastColorIndex, 0);
                }

                _progress.MaxValue = CurrentCycle.MaxWorkMinutes;
                _progress.Value = CurrentCycle.SecondProgress / 60f;

                Text = new TimeSpan(0, 0, (int)(CurrentCycle.MaxWorkMinutes * 60) - CurrentCycle.SecondProgress).ToString("mm':'ss") + " - Pomodoro";
                lblStatus.Text = new TimeSpan(0, 0, (int)(CurrentCycle.MaxWorkMinutes * 60) - CurrentCycle.SecondProgress).ToString("mm':'ss") + "\r\n" + CycleCount.ToString();
            }
        }
    }
}
