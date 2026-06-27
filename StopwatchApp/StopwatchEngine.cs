using System;

namespace StopwatchApp
{
    /// <summary>
    /// The core stopwatch logic. It tracks how much time has elapsed and what
    /// state the stopwatch is in, and raises an event once per second so a UI can
    /// update its display. It has no Windows-specific code, so it can be
    /// unit-tested on any operating system.
    /// </summary>
    public class StopwatchEngine
    {
        /// <summary>Fires once per second while the stopwatch is running.</summary>
        private readonly System.Timers.Timer _timer;

        /// <summary>The number of whole seconds counted so far.</summary>
        private int _elapsedSeconds;

        /// <summary>The current state (Stopped, Running or Paused).</summary>
        private StopwatchState _state;

        /// <summary>Gets the number of whole seconds counted so far.</summary>
        public int ElapsedSeconds => _elapsedSeconds;

        /// <summary>Gets the current state of the stopwatch.</summary>
        public StopwatchState State => _state;

        /// <summary>
        /// Raised every time the elapsed time changes. The string argument is the
        /// already-formatted time (for example "00:01:05"), ready to show in a label.
        /// </summary>
        public event EventHandler<string>? TimerTick;

        /// <summary>
        /// Creates a new stopwatch that is stopped and reading zero.
        /// </summary>
        public StopwatchEngine()
        {
            _elapsedSeconds = 0;
            _state = StopwatchState.Stopped;

            // A cross-platform timer that fires its Elapsed event every 1000 ms.
            _timer = new System.Timers.Timer(1000)
            {
                AutoReset = true
            };
            _timer.Elapsed += OnTimerElapsed;
        }

        /// <summary>
        /// Starts the stopwatch from zero. Has no effect unless the stopwatch is
        /// currently stopped.
        /// </summary>
        public void Start()
        {
            if (_state != StopwatchState.Stopped)
                return;

            _elapsedSeconds = 0;
            _state = StopwatchState.Running;
            _timer.Start();
            RaiseTimerTick();
        }

        /// <summary>
        /// Pauses a running stopwatch, keeping the elapsed time. Has no effect
        /// unless the stopwatch is currently running.
        /// </summary>
        public void Pause()
        {
            if (_state != StopwatchState.Running)
                return;

            _timer.Stop();
            _state = StopwatchState.Paused;
            RaiseTimerTick();
        }

        /// <summary>
        /// Resumes counting after a pause. Has no effect unless the stopwatch is
        /// currently paused.
        /// </summary>
        public void Resume()
        {
            if (_state != StopwatchState.Paused)
                return;

            _state = StopwatchState.Running;
            _timer.Start();
        }

        /// <summary>
        /// Sets the elapsed time back to zero. The state is left unchanged.
        /// </summary>
        public void Reset()
        {
            _elapsedSeconds = 0;
            RaiseTimerTick();
        }

        /// <summary>
        /// Stops the stopwatch and returns the final time as a formatted string.
        /// If the stopwatch is already stopped, the current time is returned
        /// without change.
        /// </summary>
        /// <returns>The final elapsed time formatted as "HH:MM:SS".</returns>
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

        /// <summary>
        /// Converts a number of seconds into an "HH:MM:SS" string with each part
        /// padded to two digits.
        /// </summary>
        /// <param name="totalSeconds">The total number of seconds to format.</param>
        /// <returns>The time formatted as "HH:MM:SS", for example "01:01:01".</returns>
        public static string FormatTime(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        /// <summary>
        /// Handles each timer tick by adding one second and notifying listeners.
        /// </summary>
        private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _elapsedSeconds++;
            RaiseTimerTick();
        }

        /// <summary>
        /// Raises the <see cref="TimerTick"/> event with the current time formatted
        /// as a string.
        /// </summary>
        private void RaiseTimerTick()
        {
            TimerTick?.Invoke(this, FormatTime(_elapsedSeconds));
        }
    }

    /// <summary>
    /// The three states a stopwatch can be in.
    /// </summary>
    public enum StopwatchState
    {
        /// <summary>Not counting; elapsed time is zero or was just stopped.</summary>
        Stopped,

        /// <summary>Actively counting up.</summary>
        Running,

        /// <summary>Counting is temporarily suspended but time is kept.</summary>
        Paused
    }
}
