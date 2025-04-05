# School Management System

Modern School Management System built with React.js, Tailwind CSS, and ASP.NET Core Web API.

## Project Structure

The project is organized into two main parts:
- **Backend**: ASP.NET Core Web API with Entity Framework Core
- **Frontend**: React.js with Tailwind CSS

## Technology Stack

### Frontend
- React.js (with Hooks, Context API)
- React Router v6+
- Tailwind CSS
- Axios for API requests
- JWT authentication
- React Hook Form for validation

### Backend
- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- ASP.NET Identity with JWT
- Repository & Unit of Work pattern
- AutoMapper

## Core Modules
- Authentication & Authorization (Admin, Teacher, Student, Parent roles)
- Dashboard
- Student Management
- Teacher Management
- Class/Section/Subject Management
- Attendance System
- Exam & Grading
- Timetable
- Messaging & Announcements
- Notifications
- Fee Management
- Library System
- Transport Module
- Parent Portal
- Online Admission
- Events Calendar
- File Uploads
- Dark Mode & Multi-language support

## Setup Instructions

### Backend Setup
1. Navigate to the `Backend` directory
2. Update the connection string in `appsettings.json`
3. Run database migrations:
   ```
   dotnet ef database update
   ```
4. Start the API:
   ```
   dotnet run
   ```

### Frontend Setup
1. Navigate to the `Frontend` directory
2. Install dependencies:
   ```
   npm install
   ```
3. Start the development server:
   ```
   npm start
   ```

## Development
- Backend API runs on: `https://localhost:7000`
- Frontend dev server runs on: `http://localhost:3000` 