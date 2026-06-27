using System;

namespace StopwatchApp
{
    /// <summary>Stopwatch logic: tracks elapsed time and state. No UI code, so it can be unit-tested anywhere.</summary>
    public class StopwatchEngine
    {
        private readonly System.Timers.Timer _timer;
        private int _elapsedSeconds;
        private StopwatchState _state;

        /// <summary>Seconds counted so far.</summary>
        public int ElapsedSeconds => _elapsedSeconds;

        /// <summary>Current state of the stopwatch.</summary>
        public StopwatchState State => _state;

        /// <summary>Raised each second with the time formatted as "HH:MM:SS".</summary>
        public event EventHandler<string>? TimerTick;

        /// <summary>Creates a stopwatch that is stopped at zero.</summary>
        public StopwatchEngine()
        {
            _elapsedSeconds = 0;
            _state = StopwatchState.Stopped;

            _timer = new System.Timers.Timer(1000) { AutoReset = true };
            _timer.Elapsed += OnTimerElapsed;
        }

        /// <summary>Starts the stopwatch from zero.</summary>
        public void Start()
        {
            if (_state != StopwatchState.Stopped)
                return;

            _elapsedSeconds = 0;
            _state = StopwatchState.Running;
            _timer.Start();
            RaiseTimerTick();
        }

        /// <summary>Pauses a running stopwatch.</summary>
        public void Pause()
        {
            if (_state != StopwatchState.Running)
                return;

            _timer.Stop();
            _state = StopwatchState.Paused;
            RaiseTimerTick();
        }

        /// <summary>Resumes a paused stopwatch.</summary>
        public void Resume()
        {
            if (_state != StopwatchState.Paused)
                return;

            _state = StopwatchState.Running;
            _timer.Start();
        }

        /// <summary>Sets the elapsed time back to zero.</summary>
        public void Reset()
        {
            _elapsedSeconds = 0;
            RaiseTimerTick();
        }

        /// <summary>Stops the stopwatch and returns the final time as "HH:MM:SS".</summary>
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

        /// <summary>Formats a number of seconds as "HH:MM:SS".</summary>
        public static string FormatTime(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _elapsedSeconds++;
            RaiseTimerTick();
        }

        private void RaiseTimerTick()
        {
            TimerTick?.Invoke(this, FormatTime(_elapsedSeconds));
        }
    }

    /// <summary>The states a stopwatch can be in.</summary>
    public enum StopwatchState
    {
        Stopped,
        Running,
        Paused
    }
}
