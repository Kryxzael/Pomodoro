﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CircleControl;

namespace Pomodoro
{
    public partial class Form1 : Form
    {
        private CircleProgressControl _progress;
        private Timer _updateTimer = new Timer() { Interval = 1000 };
        private Timer _blinkTimer = new Timer() { Interval = 500 };
        private static Random _rng = new Random();
        private bool use10minBreak = false;

        public bool Running
        {
            get
            {
                return _updateTimer.Enabled;
            }
            set
            {
                if (value)
                {
                    _blinkTimer.Stop();
                    SetForeColors(Properties.Resources.Spectre.GetPixel(_lastColorIndex, 0));                    
                }
                else
                {
                    SetForeColors(Color.Gray);
                }
                
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
            };

            Controls.Add(_progress);
            lblStatus.Parent = _progress;
            lblStatus.BackColor = Color.Transparent;
            _updateTimer.Tick += TimerTick;
            _blinkTimer.Tick += BlinkTimerTick;
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width, Screen.PrimaryScreen.WorkingArea.Height - Height);
            SetForeColors(Properties.Resources.Spectre.GetPixel(_lastColorIndex, 0));
        }

        public void CreateCycles()
        {
            for (int i = 0; i < 3; i++)
            {
                WaitingCycles.Enqueue(new PomodoroCycle(25f, use10minBreak ? 10f : 5f));
            }

            WaitingCycles.Enqueue(new PomodoroCycle(25f, 30f));
        }

        bool _inBreakBuffer;
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

                SetForeColors(Properties.Resources.Spectre.GetPixel(_lastColorIndex, 0));
                _progress.Value = _progress.MaxValue - CurrentCycle.SecondProgress / 60f;

                Text = new TimeSpan(0, 0, (int)(CurrentCycle.MaxBreakMinutes * 60) - CurrentCycle.SecondProgress).ToString("mm':'ss") + " - Break";
                lblStatus.Text = new TimeSpan(0, 0, (int)(CurrentCycle.MaxBreakMinutes * 60) - CurrentCycle.SecondProgress).ToString("mm':'ss") + "\r\n" + CycleCount.ToString();
            }
            else
            {

                if (CurrentCycle.SecondProgress % 60 == 0)
                {
                    _lastColorIndex = (_lastColorIndex + 1) % 360;
                    SetForeColors(Properties.Resources.Spectre.GetPixel(_lastColorIndex, 0));
                }

                _progress.MaxValue = CurrentCycle.MaxWorkMinutes;
                _progress.Value = CurrentCycle.SecondProgress / 60f;

                Text = new TimeSpan(0, 0, (int)(CurrentCycle.MaxWorkMinutes * 60) - CurrentCycle.SecondProgress).ToString("mm':'ss") + " - Pomodoro";
                lblStatus.Text = new TimeSpan(0, 0, (int)(CurrentCycle.MaxWorkMinutes * 60) - CurrentCycle.SecondProgress).ToString("mm':'ss") + "\r\n" + CycleCount.ToString();
            }

            if (CurrentCycle.InBreak != _inBreakBuffer)
            {
                _inBreakBuffer = CurrentCycle.InBreak;

                Running = false;
                if (CurrentCycle.InBreak)
                {
                    lblStatus.Text = "Break Time";
                }
                else
                {
                    lblStatus.Text = "Break Over";
                }

                _blinkTimer.Start();
            }
        }

        private void BlinkTimerTick(object sender, EventArgs e)
        {
            _progress.Value = _progress.Value == 0 ? _progress.MaxValue : 0;

            if (_progress.Value != 0)
            {
                SetForeColors(Properties.Resources.Spectre.GetPixel(_rng.Next(360), 0));
            }
            
        }

        private void OnMouseButtonUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
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
            else if (e.Button == MouseButtons.Right)
            {
                if (Started)
                {
                    return;
                }

                if (use10minBreak = !use10minBreak)
                {
                    lblStatus.Text = "Start\n10m breaks";
                }
                else
                {
                    lblStatus.Text = "Start\n5m breaks";
                }
            }
        }

        public void SetForeColors(Color color)
        {
            lblStatus.ForeColor = _progress.ForeColor = txtTodoLater.ForeColor = color;
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            btnNotes.Location = new Point(ClientRectangle.Right - btnNotes.Width, ClientRectangle.Bottom - btnNotes.Height);
        }

        private void OnNoteButtonClicked(object sender, EventArgs e)
        {
            txtTodoLater.Visible = !txtTodoLater.Visible;
        }
    }
}
