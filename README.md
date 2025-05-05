# Music Management System

A comprehensive ASP.NET Core MVC web application for managing personal music collections and playlists, featuring full CRUD operations, user authentication, RESTful APIs, AJAX interactivity, and responsive UI. I have left some songs and a few playlists in the database.

## Table of Contents

* [Features](#features)
* [Technology Stack](#technology-stack)
* [Getting Started](#getting-started)

* [Prerequisites](#prerequisites)
* [Setup & Run](#setup--run)
* [API Endpoints](#api-endpoints)
* [AJAX Interactions](#ajax-interactions)
* [Dashboard](#dashboard)
* [Accessibility & UX](#accessibility--ux)
* [AI Disclosure](#ai-disclosure)

## Features

* **User Authentication**: Register and log in with ASP.NET Identity.
* **Music Library**: Upload, view, and delete songs with metadata (title, artist, album, genre).
* **Playlists**: Create, edit, delete playlists; add/remove songs.
* **Media Player**: Central "Now Playing" panel with play and shuffle functionality.
* **RESTful API**: Full CRUD endpoints for Songs and Playlists under `/api/`.
* **AJAX Interactivity**: Add/remove songs and create playlists without page reloads.
* **Dashboard**: Summary of total songs, playlists, and registered users.
* **Responsive Design**: Built with Bootstrap for mobile-friendly layouts.
* **Privacy Policy**: Dedicated policy page outlining data handling practices.

## Technology Stack

* **Backend**: ASP.NET Core MVC, Entity Framework Core, ASP.NET Identity
* **Frontend**: Razor Views, Bootstrap 5, vanilla JavaScript (Fetch API)
* **Database**: SQL Server (LocalDB)

## Getting Started

### Prerequisites

* [.NET 7 SDK]
* Visual Studio 2022 (or VS Code)
* SQL Server LocalDB (installed with Visual Studio)

### Setup & Run

1. **Clone the repository**

   ```bash
   git clone https://github.com/yourusername/mms-csci3110-project.git
   cd music-management-system
   ```
2. **Configure connection string** in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SongsDB;Trusted_Connection=True;"
   }
   ```
3. **Apply migrations** and create the database:

   ```bash
   add-migration SongsDB
   ```
4. **Update the database**:

   ```bash
   update-database
   ```
5. **Run the application**

## API Endpoints

* **GET** `/api/songs` & `/api/songs/{id}`
* **POST** `/api/songs`, **PUT** `/api/songs/{id}`, **DELETE** `/api/songs/{id}`
* **GET** `/api/playlists` & `/api/playlists/{id}`
* **POST** `/api/playlists`, **PUT** `/api/playlists/{id}`, **DELETE** `/api/playlists/{id}`

## AJAX Interactions

* **Add Song** to playlist without reload
* **Remove Song** from playlist without reload
* **Create Playlist** inline on Home page

## Dashboard

Navigate to **Dashboard** via the top nav to view total counts of songs, playlists, and users.

## Accessibility & UX

* Built with Bootstrap 5 for responsive layouts
* ARIA labels on interactive elements
* Keyboard-focusable controls

## AI Disclosure

* **Tool**: ChatGPT was used to brainstorm features and refactor code snippets.
* **Usage**: Assisted with project structuring, UI/UX suggestions, and documentation outlines.

---

**Enjoy managing your music!**
