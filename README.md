# Summoner

Small desktop tool for managing application placement between monitors. Solving the "hidden" but "running" problem.

Select any open application window and "summon" it to the top-left corner of a chosen monitor — perfect for when Windows forgets where a window should be, or when you've lost a window off-screen.

## Features

- Lists all visible application windows and connected monitors
- Move any window to any monitor with one click
- Handles maximized windows correctly (restores, moves, re-maximizes)
- Filters out cloaked (DWM-hidden), tool, and background windows — only taskbar-visible apps appear
- Refresh lists at any time

## Usage

1. Launch `summoner.exe`
2. Select a window from the **Programs** list
3. Select a target monitor from the **Monitors** list
4. Click **Summon Window**

The window will appear at the top-left corner of the selected monitor. If it was maximized, it stays maximized on the new monitor.

## Build

Requires the [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0).

```powershell
dotnet build -c Release
```

## Publish (self-contained single file)

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

## Requirements

- Windows OS (uses `user32.dll` and `dwmapi.dll` P/Invoke)
- .NET 9 (runtime for framework-dependent deployment)
