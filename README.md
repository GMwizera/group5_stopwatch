# Group 5 Stopwatch (C#)

A desktop stopwatch in C# with Start, Pause, Resume, Reset and Stop. The time
shows as `HH:MM:SS`, and each button enables or disables based on the state.

Built for Programming in C# (ALU, Trimester 1).

## Structure

- `StopwatchApp/`: stopwatch logic (plain, cross-platform C#).
- `StopwatchApp.Tests/`: MSTest unit tests for the logic.
- `Windows Forms App/Timer/`: the Windows Forms UI.

The UI runs on Windows only. The logic and tests run anywhere.

## Run the app (Windows)

Open `Windows Forms App/Timer.slnx` in Visual Studio and press F5.

## Run the tests (any OS)

```bash
dotnet test StopwatchApp.Tests/StopwatchApp.Tests.csproj
```

All 15 tests pass.
