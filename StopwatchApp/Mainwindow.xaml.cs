using System.Windows;
using System.Windows.Controls;

namespace StopwatchApp
{
    public partial class MainWindow : Window
    {
        private readonly StopwatchEngine _engine;

        public MainWindow()
        {
            InitializeComponent();

            _engine = new StopwatchEngine();
            _engine.TimerTick += OnTimerTick;
        }

        private void OnTimerTick(object? sender, string formattedTime)
        {
            TimerDisplay.Text = formattedTime;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _engine.Start();

            StatusLabel.Text = "Running...";
            SetButtonStates(running: true);
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            _engine.Pause();

            StatusLabel.Text = $"Paused at {TimerDisplay.Text}";
            SetButtonStates(paused: true);
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            _engine.Resume();

            StatusLabel.Text = "Running...";
            SetButtonStates(running: true);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _engine.Reset();

            StatusLabel.Text = _engine.State == StopwatchState.Running
                ? "Reset — Running..."
                : "Reset — Paused";
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            string lastTime = _engine.Stop();

            StatusLabel.Text = $"Stopped. Last time: {lastTime}";
            SetButtonStates(stopped: true);
        }

        private void SetButtonStates(bool running = false, bool paused = false, bool stopped = false)
        {
            if (running)
            {
                StartButton.IsEnabled  = false;
                PauseButton.IsEnabled  = true;
                ResumeButton.IsEnabled = false;
                ResetButton.IsEnabled  = true;
                StopButton.IsEnabled   = true;
            }
            else if (paused)
            {
                StartButton.IsEnabled  = false;
                PauseButton.IsEnabled  = false;
                ResumeButton.IsEnabled = true;
                ResetButton.IsEnabled  = true;
                StopButton.IsEnabled   = true;
            }
            else if (stopped)
            {
                StartButton.IsEnabled  = true;
                PauseButton.IsEnabled  = false;
                ResumeButton.IsEnabled = false;
                ResetButton.IsEnabled  = false;
                StopButton.IsEnabled   = false;
            }
        }
    }
}