# Group 5 Stopwatch Application (C#)

A desktop stopwatch written in C#. It has Start, Pause, Resume, Reset and Stop
buttons, and shows the elapsed time as `HH:MM:SS`. Each button enables or disables
itself so that only the valid actions are available at any time.

The project was built for Programming in C# (ALU, Trimester 1).

## Features

- Counts elapsed time and shows it as `HH:MM:SS`.
- Start, Pause, Resume, Reset and Stop controls.
- Buttons enable and disable themselves based on the current state
  (Stopped, Running, Paused).
- The stopwatch logic is kept separate from the interface, so it can be
  unit-tested on any operating system.

## Project structure

| Path | What it is |
| --- | --- |
| `StopwatchApp/` | The stopwatch logic (`StopwatchEngine`). Plain, cross-platform C#, and the part the tests run against. |
| `StopwatchApp.Tests/` | Unit tests for the logic, written with MSTest. |
| `Windows Forms App/Timer/` | The Windows desktop interface (Windows Forms) that uses the logic. |
| `Windows Forms App/Timer.slnx` | The Visual Studio solution that ties the projects together. |

The logic lives in its own project so it can be tested without opening a window.
The Windows Forms interface only runs on Windows, but the logic and its tests run
on Windows, macOS and Linux.

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) 8.0 or newer.
- For the desktop window only: Windows, since Windows Forms is Windows-only.
  Visual Studio 2022 with the ".NET desktop development" workload is recommended.

## Running the application (Windows)

1. Open `Windows Forms App/Timer.slnx` in Visual Studio.
2. Press F5 (or the green play button) to build and run.
3. The stopwatch window opens. Use the buttons to control it.

From the command line on Windows:

```bash
dotnet run --project "Windows Forms App/Timer/Timer.csproj"
```

## Running the tests (any operating system)

The tests cover the logic only, so they run on Windows, macOS or Linux:

```bash
dotnet test StopwatchApp.Tests/StopwatchApp.Tests.csproj
```

All 15 tests should pass.

## Testing approach (TDD)

The logic is written test-first (Test-Driven Development): a test describes the
behaviour we want, then we write the code to make it pass. The tests cover three
areas:

- Time formatting, including zero, seconds, minutes and hours, and checking that
  values do not roll over early (59 seconds stays `00:00:59`).
- State changes, so that Start, Pause, Resume and Stop move the stopwatch
  correctly between the Stopped, Running and Paused states.
- Guard conditions, so that invalid actions are ignored (for example starting
  when already running, or pausing when stopped).

## Team (Group 5)

Built by Group 5, with the work split three ways:

- the Windows Forms interface and button wiring
- the stopwatch engine and timing logic
- the unit tests, XML documentation and project assets
