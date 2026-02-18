# Chi Vuol Essere Milionario? (Milionario)

A simple Windows WPF implementation of the "Who Wants to Be a Millionaire?" style quiz game written in C#.

This repository contains a local, single-player quiz game that:
- Loads questions from a CSV file grouped by difficulty
- Plays background music and short video intro
- Shows a per-question countdown timer
- Tracks player progress and score
- Saves a top-10 high score list (classifica)

---

## Table of contents

- [About](#about)
- [Features](#features)
- [Project structure](#project-structure)
- [File / data formats](#data-formats)
- [How the game works (brief)](#how-the-game-works)

---

## About

This is a small WPF (Windows) desktop application that mimics the "Millionaire" quiz show. It was implemented using XAML for UI and C# for logic. The UI and all user-visible strings are in Italian.

---

## Features

- Multiple difficulty levels (15 levels are prepared in the code)
- Questions loaded from `domande.csv`
- Top-10 scoreboard saved in `classifica.csv`
- Per-question countdown (60 seconds) implemented as a reusable `CTimer` UserControl
- Background music and short video intro via `MediaElement` and `MediaPlayer`
- Simple UI with image-based backgrounds and answer buttons

---

## Project structure

- `Milionario.sln` — Visual Studio solution
- `Milionario/` — WPF project folder
  - `MainWindow.xaml` / `MainWindow.xaml.cs` — main UI and game logic
  - `CTimer.xaml` / `CTimer.xaml.cs` — countdown timer control (raises an IsZero event)
  - `CDomanda.cs` — question model (parses a CSV line into question + answers + difficulty)
  - `CPlayer.cs` — player model and scoring helper
  - `File.cs` — file I/O helpers: loads `domande.csv` and `classifica.csv`, saves top-10
  - `domande.csv` — questions data (required)
  - `classifica.csv` — persistent top-10 scoreboard (created/updated by the app)
  - `img/` — images used by UI (logo, background, buttons, etc.)
  - `audio/` — audio files used by the app (background music, sigla, etc.)
  - `audio` and `img` folders are referenced by relative paths; these resources must be present next to the executable.

---

## Data formats

Two simple CSV files are used:

1. domande.csv
- Each line is a question record.
- Format expected by CDomanda.FromCSV:
  difficulty;question;correctAnswer;answer2;answer3;answer4
- `difficulty` is parsed as an integer and used to group questions into levels (0..14 based on code's array size).
- Example:
  2;Qual è la capitale d'Italia?;Roma;Milano;Napoli;Torino

2. classifica.csv
- Each line is a player score record used for the top-10 list.
- Format:
  playerName;score
- Example:
  Mario Rossi;300000

Notes:
- The code expects at least 6 fields per question line (so four answers).
- Files are read with simple `StreamReader` and split by `;` — there is no escaping/quoting handling.

---

## How the game works

- On startup the app:
  - Plays a short intro video (`.\img\sigla.mov`) via a `MediaElement`.
  - Loads questions using `File.GetDomande()`, which reads `domande.csv` and groups questions into an array of lists keyed by difficulty.
  - Loads scoreboard via `File.GetClassifica()` from `classifica.csv`.
- Player model (`CPlayer`) holds the name, current score and difficulty progress and provides `NextLvl()` to progress to the next prize level.
- Questions (`CDomanda`) store the text, the four answers and the difficulty. The CSV parser assigns `Risposte[0]` to the correct answer.
- `CTimer` is a UserControl that raises an `IsZero` event when time runs out (60 seconds per question by default).
- Answer buttons are wired to the `Risposta` handler in `MainWindow.xaml.cs` which checks correctness, plays audio, updates UI and moves to next level or ends the game.
- `File.SaveClassifica` writes the top-10 scores back to `classifica.csv`.
