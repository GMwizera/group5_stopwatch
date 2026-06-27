using StopwatchApp;

namespace Timer
{
    /// <summary>Main window: wires the buttons to the stopwatch engine and shows the time.</summary>
    public partial class Form1 : Form
    {
        private readonly StopwatchEngine _engine;

        /// <summary>Sets up the window and the stopwatch engine.</summary>
        public Form1()
        {
            InitializeComponent();
            _engine = new StopwatchEngine();
            _engine.TimerTick += OnTimerTick;

            // Set initial display
            label1.Text = "00:00:00";
            UpdateButtonStates();
        }

        /// <summary>Updates the time label on each engine tick.</summary>
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

        /// <summary>Start button.</summary>
        private void Start_Click(object sender, EventArgs e)
        {
            _engine.Start();
            UpdateButtonStates();
        }

        /// <summary>Pause button.</summary>
        private void Pause_Click(object sender, EventArgs e)
        {
            _engine.Pause();
            UpdateButtonStates();
        }

        /// <summary>Resume button.</summary>
        private void Resume_Click(object sender, EventArgs e)
        {
            _engine.Resume();
            UpdateButtonStates();
        }

        /// <summary>Stop button.</summary>
        private void Stop_Click(object sender, EventArgs e)
        {
            string finalTime = _engine.Stop();
            label1.Text = finalTime;
            UpdateButtonStates();
        }

        /// <summary>Reset button.</summary>
        private void Reset_Click(object sender, EventArgs e)
        {
            _engine.Reset();
            label1.Text = "00:00:00";
            UpdateButtonStates();
        }

        /// <summary>Enables or disables buttons to match the current state.</summary>
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
        private void timer1_Tick(object sender, EventArgs e) { }
        private void timer2_Tick(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}