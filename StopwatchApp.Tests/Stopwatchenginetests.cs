using Microsoft.VisualStudio.TestTools.UnitTesting;
using StopwatchApp;

namespace StopwatchApp.Tests
{
    [TestClass]
    public class StopwatchEngineTests
    {
        [TestMethod]
        public void FormatTime_ZeroSeconds_ReturnsZeroString()
        {
            string result = StopwatchEngine.FormatTime(0);
            Assert.AreEqual("00:00:00", result);
        }

        [TestMethod]
        public void FormatTime_65Seconds_ReturnsCorrectFormat()
        {
            string result = StopwatchEngine.FormatTime(65);
            Assert.AreEqual("00:01:05", result);
        }

        [TestMethod]
        public void FormatTime_3661Seconds_ReturnsCorrectFormat()
        {
            string result = StopwatchEngine.FormatTime(3661);
            Assert.AreEqual("01:01:01", result);
        }

        [TestMethod]
        public void FormatTime_3600Seconds_ReturnsOneHour()
        {
            string result = StopwatchEngine.FormatTime(3600);
            Assert.AreEqual("01:00:00", result);
        }

        [TestMethod]
        public void FormatTime_59Seconds_DoesNotRolloverToMinutes()
        {
            string result = StopwatchEngine.FormatTime(59);
            Assert.AreEqual("00:00:59", result);
        }

        [TestMethod]
        public void Engine_InitialState_IsStopped()
        {
            var engine = new StopwatchEngine();
            Assert.AreEqual(StopwatchState.Stopped, engine.State);
        }

        [TestMethod]
        public void Start_ChangesState_ToRunning()
        {
            var engine = new StopwatchEngine();
            engine.Start();
            Assert.AreEqual(StopwatchState.Running, engine.State);
        }

        [TestMethod]
        public void Pause_WhenRunning_ChangesState_ToPaused()
        {
            var engine = new StopwatchEngine();
            engine.Start();
            engine.Pause();
            Assert.AreEqual(StopwatchState.Paused, engine.State);
        }

        [TestMethod]
        public void Resume_WhenPaused_ChangesState_ToRunning()
        {
            var engine = new StopwatchEngine();
            engine.Start();
            engine.Pause();
            engine.Resume();
            Assert.AreEqual(StopwatchState.Running, engine.State);
        }

        [TestMethod]
        public void Stop_ChangesState_ToStopped()
        {
            var engine = new StopwatchEngine();
            engine.Start();
            engine.Stop();
            Assert.AreEqual(StopwatchState.Stopped, engine.State);
        }

        [TestMethod]
        public void Engine_InitialElapsedSeconds_IsZero()
        {
            var engine = new StopwatchEngine();
            Assert.AreEqual(0, engine.ElapsedSeconds);
        }

        [TestMethod]
        public void Reset_SetsElapsedSeconds_ToZero()
        {
            var engine = new StopwatchEngine();
            engine.Start();
            engine.Reset();
            Assert.AreEqual(0, engine.ElapsedSeconds);
        }

        [TestMethod]
        public void Start_WhenAlreadyRunning_DoesNothing()
        {
            var engine = new StopwatchEngine();
            engine.Start();
            engine.Start();
            Assert.AreEqual(StopwatchState.Running, engine.State);
        }

        [TestMethod]
        public void Pause_WhenStopped_DoesNothing()
        {
            var engine = new StopwatchEngine();
            engine.Pause();
            Assert.AreEqual(StopwatchState.Stopped, engine.State);
        }

        [TestMethod]
        public void Stop_ReturnsLastFormattedTime()
        {
            var engine = new StopwatchEngine();
            engine.Start();
            string lastTime = engine.Stop();
            Assert.AreEqual("00:00:00", lastTime);
        }
    }
}