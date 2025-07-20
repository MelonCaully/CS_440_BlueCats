# D&D Player Sheet Generator <img src="BlueCatsApp/Resources/Images/blue_cat.png" alt="drawing" width="50" style="position: relative; top: 10px;"/>

A cross-platform Dungeons & Dragons player sheet generator built with .NET MAUI and backed by SQLite. Designed to provide a clean, user-friendly interface for managing characters, stats, and inventories — all in one place.

This tool aims to simplify character management for DMs and players alike, offering a digital alternative to paper sheets, while maintaining flexibility and customizability for homebrew content.

## 🎯 Motivation

Managing a D&D campaign or character should be immersive — not administrative. With this app, you can:

- 📋 Create and manage character sheets with persistent data storage via SQLite.

- 🧙 Edit stats, abilities, and inventory directly in the UI.

- 💾 Use locally stored data without needing an internet connection.

- 🔧 Customize easily for different editions or homebrew campaigns.

Whether you're a Dungeon Master running multiple NPCs or a player tracking every spell slot, this app aims to make the experience smooth, reliable, and enjoyable.

## ⚙️ Installation & Setup

Prerequisites
Install the following before running the project:

- SQLite — for local database storage

- DB Browser for SQLite — for exploring/editing the database manually

- .NET SDK — for building and running the app

- [VS Code Extensions]
    
    - C#

    - C# Dev Kit

    - IntelliCode for C# Dev Kit

    - .NET MAUI

## 🛠️ Configuration

VS Code Setup

1. Open the project in Visual Studio Code 

1. Navigate to MauiProgram.cs

1. In the bottom right, select the startup project as BlueCatsApp

1. In the top right, click "Run project associated with this file" to launch the app

Database Access

- Launch DB Browser for SQLite

- Open dungeonBase.db from the root project directory to view/edit database tables

## 🧪 Testing

Testing is currently manual via interactive use, but future versions may support unit tests for database interactions and UI behavior.

## 🤝 Contributing
If you'd like to help improve this project:
```bash
git clone https://github.com/MelonCaully/CS_440_BlueCats
cd CS_440_BlueCats
```
Then fork and submit a pull request to the main branch. Contributions to UI design, database schema, or feature ideas are always welcome!

