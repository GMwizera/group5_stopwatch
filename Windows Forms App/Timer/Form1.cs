using StopwatchApp;

namespace Timer
{
    /// <summary>
    /// The main application window. It connects the buttons to the
    /// <see cref="StopwatchEngine"/> and keeps the on-screen time display and the
    /// button states in sync with the engine.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>The stopwatch logic this window controls and displays.</summary>
        private readonly StopwatchEngine _engine;

        /// <summary>
        /// Builds the window, creates the stopwatch engine, subscribes to its
        /// tick event and shows the initial "00:00:00" display.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            _engine = new StopwatchEngine();
            _engine.TimerTick += OnTimerTick;

            // Set initial display
            label1.Text = "00:00:00";
            UpdateButtonStates();
        }

        /// <summary>
        /// Runs on every engine tick and updates the time label. Because the
        /// engine's timer may fire on a background thread, the update is marshalled
        /// back onto the UI thread when required.
        /// </summary>
        /// <param name="sender">The engine that raised the event.</param>
        /// <param name="formattedTime">The current time, already formatted as "HH:MM:SS".</param>
        private void OnTimerTick(object? sender, string formattedTime)
        {
            // Marshal to UI thread in case of cross-thread calls
            if (InvokeRequired)
            {
                Invoke(() => label1.Text = formattedTime);
            }
            else
            {
                label1.Text = formattedTime;
            }

            UpdateButtonStates();
        }

        /// <summary>Handles the Start button: starts the stopwatch from zero.</summary>
        private void Start_Click(object sender, EventArgs e)
        {
            _engine.Start();
            UpdateButtonStates();
        }

        /// <summary>Handles the Pause button: pauses a running stopwatch.</summary>
        private void Pause_Click(object sender, EventArgs e)
        {
            _engine.Pause();
            UpdateButtonStates();
        }

        /// <summary>Handles the Resume button: continues counting after a pause.</summary>
        private void Resume_Click(object sender, EventArgs e)
        {
            _engine.Resume();
            UpdateButtonStates();
        }

        /// <summary>Handles the Stop button: stops the stopwatch and shows the final time.</summary>
        private void Stop_Click(object sender, EventArgs e)
        {
            string finalTime = _engine.Stop();
            label1.Text = finalTime;
            UpdateButtonStates();
        }

        /// <summary>Handles the Reset button: sets the display back to "00:00:00".</summary>
        private void Reset_Click(object sender, EventArgs e)
        {
            _engine.Reset();
            label1.Text = "00:00:00";
            UpdateButtonStates();
        }

        /// <summary>
        /// Enables or disables each button so that only the actions that make sense
        /// for the current state are available. Marshals to the UI thread if needed.
        /// </summary>
        private void UpdateButtonStates()
        {
            if (InvokeRequired)
            {
                Invoke(UpdateButtonStates);
                return;
            }

            StopwatchState state = _engine.State;

            Start.Enabled = state == StopwatchState.Stopped;
            Pause.Enabled = state == StopwatchState.Running;
            Resume.Enabled = state == StopwatchState.Paused;
            Stop.Enabled = state == StopwatchState.Running || state == StopwatchState.Paused;
            Reset.Enabled = state == StopwatchState.Paused || state == StopwatchState.Stopped;
        }

        // Empty handlers kept only so the Windows Forms designer does not error.
        /// <summary>Unused designer handler.</summary>
        private void timer1_Tick(object sender, EventArgs e) { }
        private void timer2_Tick(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}