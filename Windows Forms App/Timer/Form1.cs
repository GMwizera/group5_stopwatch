using StopwatchApp;

namespace Timer
{
    public partial class Form1 : Form
    {
        private readonly StopwatchEngine _engine;

        public Form1()
        {
            InitializeComponent();
            _engine = new StopwatchEngine();
            _engine.TimerTick += OnTimerTick;

            // Set initial display
            label1.Text = "00:00:00";
            UpdateButtonStates();
        }

        // Runs on every engine tick — updates the display label
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

        private void Start_Click(object sender, EventArgs e)
        {
            _engine.Start();
            UpdateButtonStates();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            _engine.Pause();
            UpdateButtonStates();
        }

        private void Resume_Click(object sender, EventArgs e)
        {
            _engine.Resume();
            UpdateButtonStates();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            string finalTime = _engine.Stop();
            label1.Text = finalTime;
            UpdateButtonStates();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            _engine.Reset();
            label1.Text = "00:00:00";
            UpdateButtonStates();
        }

        // Enables/disables buttons based on the current engine state
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

        // To avoid designer errors
        private void timer1_Tick(object sender, EventArgs e) { }
        private void timer2_Tick(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}