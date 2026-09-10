# 🏋️ Workout Tracker

A full-stack workout tracking application built with **ASP.NET Core Web API, Angular, and SQL Server**.

Workout Tracker allows users to securely register and log in, record their workouts, manage existing workouts, and track their progress over time.

---

## ✨ Features

- 🔐 User registration and login
- 🔑 JWT-based authentication
- 🏋️ Create, edit, and delete workouts
- 📋 View recorded workouts
- 🏃 Three workout types:
  - Cardio
  - Strength
  - Flexibility

- ⏱️ Track workout duration
- 🔥 Track calories burned
- 💪 Rate workout intensity from 1–10
- 😓 Rate subjective fatigue from 1–10
- 📝 Add optional workout notes
- 📅 Record workout date and time
- 📊 View monthly progress
- 📈 Weekly workout statistics
- ✅ Frontend and backend validation
- 🛡️ Protected routes and API endpoints
- 💬 Confirmation dialog before deleting workouts

---

## 🛠️ Tech Stack

### Backend

| Technology               | Purpose               |
| ------------------------ | --------------------- |
| ⚙️ .NET 10               | Application framework |
| 🌐 ASP.NET Core Web API  | REST API              |
| 🗄️ Entity Framework Core | ORM / database access |
| 💾 SQL Server            | Database              |
| 🔐 JWT                   | Authentication        |
| 🔒 BCrypt                | Password hashing      |
| ✅ FluentValidation      | Request validation    |

### Frontend

| Technology            | Purpose              |
| --------------------- | -------------------- |
| 🅰️ Angular            | Frontend framework   |
| 📘 TypeScript         | Programming language |
| 🎨 Angular Material   | UI components        |
| 🌬️ Tailwind CSS       | Styling              |
| 🌐 Angular HttpClient | API communication    |

---

## 🏗️ Architecture

The application uses a **layered architecture** with separation of concerns between controllers, business logic, data access, and the database.

### Backend request flow

```text
Angular
   ↓
Controller
   ↓
Service
   ↓
Repository
   ↓
DbContext
   ↓
SQL Server
```

### Backend structure

```text
backend/
├── Controllers/
├── Data/
├── DTOs/
├── Entities/
├── Migrations/
├── Repositories/
│   └── Interfaces/
├── Services/
│   └── Interfaces/
├── Validators/
├── Program.cs
├── appsettings.json
└── WorkoutTracker.csproj
```

### Frontend structure

```text
frontend/
└── src/
    └── app/
        ├── core/
        │   ├── guards/
        │   ├── interceptors/
        │   └── services/
        │
        ├── features/
        │   ├── auth/
        │   ├── workouts/
        │   └── progress/
        │
        └── shared/
            ├── confirm-dialog/
            ├── layout/
            └── navbar/
```

---

## 🔐 Authentication

Authentication is implemented using **JWT tokens**.

The authentication flow is:

1. 👤 User registers or logs in.
2. 🔍 Backend validates the credentials.
3. 🔑 Backend generates a JWT token.
4. 💾 Frontend stores the token in `localStorage`.
5. 🌐 Angular HTTP interceptor attaches the token to protected requests.
6. 🛡️ Auth guard protects authenticated routes.

Passwords are hashed using **BCrypt** before being stored in the database.

---

## ✅ Validation

Workout data is validated on both the frontend and backend.

The application validates:

- ⏱️ Duration must be greater than `0`
- 🔥 Calories burned cannot be negative
- 💪 Intensity must be between `1` and `10`
- 😓 Fatigue must be between `1` and `10`
- 📝 Notes are limited to `500` characters
- 📅 Workout date cannot be in the future
- 🏃 Workout type must be a valid workout type

Backend request validation is implemented using **FluentValidation**.

---

## 📊 Progress Tracking

Users can select a month and view their workout progress grouped by week.

For each week, the application displays:

| Metric               | Description                |
| -------------------- | -------------------------- |
| ⏱️ Total Duration    | Total workout duration     |
| 🏋️ Workout Count     | Number of workouts         |
| 💪 Average Intensity | Average workout intensity  |
| 😓 Average Fatigue   | Average subjective fatigue |

Progress data is calculated by the backend based on the authenticated user's workouts.

---

## 🌐 API Endpoints

### 🔐 Authentication

```http
POST /api/auth/register
POST /api/auth/login
```

### 🏋️ Workouts

```http
GET    /api/workout
GET    /api/workout/{id}
POST   /api/workout
PUT    /api/workout/{id}
DELETE /api/workout/{id}
```

### 📊 Progress

```http
GET /api/workout/progress?year={year}&month={month}
```

Workout and progress endpoints require authentication.

---

## 🚀 Getting Started

### 📋 Prerequisites

Make sure you have the following installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/)
- npm
- SQL Server
- Angular CLI

### 1️⃣ Clone the repository

```bash
git clone https://github.com/MarkoBjelica98/WorkoutTracker.git
cd WorkoutTracker
```

### 2️⃣ Configure the backend

Navigate to the backend:

```bash
cd backend
```

Create a local:

```text
appsettings.Development.json
```

This file is excluded from Git because it contains local configuration and the JWT secret.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_SQL_SERVER_CONNECTION_STRING"
  },
  "Jwt": {
    "Key": "YOUR_LOCAL_JWT_SECRET"
  }
}
```

### 3️⃣ Create the database

From the `backend` directory:

```bash
dotnet ef database update
```

### 4️⃣ Run the backend

```bash
dotnet run
```

### 5️⃣ Install frontend dependencies

Open another terminal:

```bash
cd frontend
npm install
```

### 6️⃣ Run the Angular application

```bash
ng serve
```

The application will be available at:

```text
http://localhost:4200
```

---

## 🧪 Testing & Development

The project includes Angular unit test files for the main components, services, guards, and interceptors.

The backend API can be tested using **Swagger** or the included `.http` file.

---

## 🎥 Demo https://drive.google.com/file/d/1-kyminGPxplyGCijXEH_BxKA7O-1sd_Z/view?usp=sharing

The project includes a short demo covering:

- 🔐 Registration and login
- 🏋️ Adding a workout
- ✅ Form validation
- ✏️ Editing a workout
- 🗑️ Deleting a workout
- 📊 Viewing monthly progress
- 🏗️ Project architecture

> 🎬 Demo video: **[]**

---

## 🎯 Project Goals

This project was developed with a focus on:

- 🧩 Separation of concerns
- 🏗️ Layered architecture
- 📐 SOLID principles
- 🔄 Maintainable and extensible code
- 🔐 Secure authentication
- 🌐 RESTful API design
- ♻️ Reusable Angular services and components
- ✅ Proper validation
- 🔗 Clear frontend/backend separation

The recommended **.NET + Angular** stack was used to demonstrate practical full-stack development and clean project organization.

---

## 👨‍💻 Author

**Marko Bjelica**

[GitHub](https://github.com/MarkoBjelica98)
