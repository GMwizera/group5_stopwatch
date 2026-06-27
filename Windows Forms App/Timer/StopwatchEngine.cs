using System;
using System.Windows.Threading;

namespace StopwatchApp
{
    public class StopwatchEngine
    {
        private readonly DispatcherTimer _timer;
        private int _elapsedSeconds;
        private StopwatchState _state;

        public int ElapsedSeconds => _elapsedSeconds;
        public StopwatchState State => _state;

        public event EventHandler<string>? TimerTick;

        public StopwatchEngine()
        {
            _elapsedSeconds = 0;
            _state = StopwatchState.Stopped;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += OnTimerTick;
        }

        public void Start()
        {
            if (_state != StopwatchState.Stopped)
                return;

            _elapsedSeconds = 0;
            _state = StopwatchState.Running;
            _timer.Start();
            RaiseTimerTick();
        }

        public void Pause()
        {
            if (_state != StopwatchState.Running)
                return;

            _timer.Stop();
            _state = StopwatchState.Paused;
            RaiseTimerTick();
        }

        public void Resume()
        {
            if (_state != StopwatchState.Paused)
                return;

            _state = StopwatchState.Running;
            _timer.Start();
        }

        public void Reset()
        {
            _elapsedSeconds = 0;
            RaiseTimerTick();
        }

        public string Stop()
        {
            if (_state == StopwatchState.Stopped)
                return FormatTime(_elapsedSeconds);

            _timer.Stop();
            _state = StopwatchState.Stopped;
            string lastTime = FormatTime(_elapsedSeconds);
            RaiseTimerTick();
            return lastTime;
        }

        public static string FormatTime(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            _elapsedSeconds++;
            RaiseTimerTick();
        }

        private void RaiseTimerTick()
        {
            TimerTick?.Invoke(this, FormatTime(_elapsedSeconds));
        }
    }

    public enum StopwatchState
    {
        Stopped,
        Running,
        Paused
    }
}