#  Weather Dashboard

A small **Weather Dashboard** application built with:

- **Backend:** ASP.NET Core 8 (Web API, EF Core + SQLite, Clean Architecture, SOLID principles)  
- **Frontend:** Angular 17 (standalone components, NGXS for state management, Angular Material UI)  
- **Integration:** OpenWeather API for live weather data  


###  Backend (C# / .NET 8)
- Retrieves weather via **OpenWeather API**  
- Caches responses (**10 min TTL**)  
- Persists history in **SQLite** (for trend analysis)  
- User preferences stored per **UserId**  
- Clean architecture: `Domain`, `DataAccess`, `Services`, `WebApi` projects  
- Integration tests using **NUnit + WebApplicationFactory**

### Frontend (Angular 17)
- Dashboard showing **weather cards** for all 5 cities  
- Toggle **°C / °F** units (NGXS state persisted session-wide)  
- Toggle **sunrise/sunset** display  
- Forecast view per city  
- **Angular Material** for UI + icons  

---

## Tech Stack

### Backend
- .NET 8 Web API  
- Entity Framework Core (SQLite)  
- AutoMapper  
- System.Text.Json with `JsonStringEnumConverter`  
- NUnit + FluentAssertions (integration tests)  

### Frontend
- Angular 17 (standalone components)  
- NGXS (state management)  
- Angular Material  
- SCSS styling  

---

## Setup

### 1. Clone the repo
 https://github.com/Baljeet2126/WeatherDashboardApp.git

### 2. Run API: 
   ## Swagger UI: https://localhost:7085/swagger

### 3. Frontend (Angular 17):
cd WeatherApp.Frontend
npm install
npm start
## Angular app runs at: http://localhost:4200
 
## Preview

<img width="1915" height="657" alt="Weather Dashboard Screenshot" src="https://github.com/user-attachments/assets/242b0eeb-564d-496c-9cc8-5236daad2215" />
