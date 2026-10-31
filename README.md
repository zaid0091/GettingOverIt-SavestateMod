# Getting Over It with Bennett Foddy - Practice & Savestate Mod

[![Platform](https://img.shields.io/badge/Platform-Windows-blue.svg)](https://github.com)
[![Framework](https://img.shields.io/badge/Framework-BepInEx%205-orange.svg)](https://github.com/BepInEx/BepInEx)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A lightweight, seamless practice and checkpoint mod for **Getting Over It with Bennett Foddy**. 
Practice difficult sections (The Chimney, The Orange, Devil's Chimney, The Anvil, The Snake) repeatedly without losing your mountain climbing progress.

---

## Features
- **Instant Save Checkpoint ([F5])**: Captures Diogenes' exact position, pot rotation, camera focus, and physics state anywhere on the mountain.
- **Instant Teleport Back ([F6])**: Warps Diogenes back to your saved checkpoint and automatically zeroes out downward fall velocity so you land smoothly without slingshotting.
- **On-Screen HUD Reminder ([F7])**: Clean in-game banner showing altitude and confirmation on saving/loading. Can be toggled on or off.

---

## Controls

| Key | Action | Description |
| :---: | :--- | :--- |
| **`[F5]`** | **Save Checkpoint** | Saves your current location and altitude on the mountain. |
| **`[F6]`** | **Load Checkpoint** | Warps you back to the checkpoint and cancels any falling speed. |
| **`[F7]`** | **Toggle HUD** | Shows or hides the top-left on-screen controls banner. |

---

## Installation

### Method 1: All-in-One Drag & Drop (Recommended)
1. Download **`GOI-Savestate-Practice-Mod-v1.0.0.zip`** from the [Releases](https://github.com) tab.
2. Extract all contents into your *Getting Over It* directory (where `GettingOverIt.exe` is located).
3. Start the game normally.

### Method 2: Plugin-Only (For existing BepInEx users)
1. Download **`SavestateMod-v1.0.0-PluginOnly.zip`**.
2. Drop `SavestateMod.dll` into your `BepInEx/plugins/` directory.

---

## Uninstallation
Delete `winhttp.dll`, `doorstop_config.ini`, and the `BepInEx` folder from your game directory.

---

## Building from Source
Requirements: [.NET SDK (8.0+)](https://dotnet.microsoft.com/)
```bash
dotnet build src/SavestateMod.csproj -c Release
```

---

## Community Note
This mod is designed for offline training and practice. Official speedrun submissions (e.g. Speedrun.com) require an unmodded client unless submitting to a designated modded/practice leaderboard.
