# SplitMate - Setup Instructions

## Project Structure

This project consists of two parts:
1. **Angular Frontend** - Located in the root directory
2. **ASP.NET Core Web API Backend** - Located in `SplitMateAPI/` folder

## Prerequisites

- Node.js (v18 or higher)
- .NET SDK 9.0
- Angular CLI (`npm install -g @angular/cli`)

## Running the Application

### Step 1: Start the ASP.NET Core Backend

Open a terminal and navigate to the backend folder:

```bash
cd SplitMateAPI
dotnet run
```

The API will start on: **http://localhost:5024**

You should see output like:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5024
```

### Step 2: Start the Angular Frontend

Open a **new terminal** (keep the backend running) and navigate to the project root:

```bash
ng serve
```

The Angular app will start on: **http://localhost:4200**

### Step 3: Open the Application

Open your browser and navigate to:
```
http://localhost:4200
```

## API Endpoints

The backend provides the following REST API endpoints:

### Groups
- `GET /api/groups` - Get all groups
- `GET /api/groups/{id}` - Get a specific group
- `POST /api/groups` - Create a new group
- `DELETE /api/groups/{id}` - Delete a group
- `POST /api/groups/{id}/members` - Add a member to a group
- `DELETE /api/groups/{id}/members/{memberName}` - Remove a member

### Expenses
- `POST /api/groups/{groupId}/expenses` - Add an expense
- `DELETE /api/groups/{groupId}/expenses/{expenseId}` - Delete an expense

### Balances
- `GET /api/groups/{groupId}/balances` - Calculate member balances

### Settlements
- `GET /api/groups/{groupId}/settlements` - Get optimal settlements
- `POST /api/groups/{groupId}/settlements` - Mark a settlement as complete

## Architecture

### Backend (ASP.NET Core)
- **Controllers**: Handle HTTP requests and responses
- **Services**: Business logic for data management, balance calculation, and settlement optimization
- **Models**: Data structures for Group, Expense, Settlement, Balance, and Transaction
- **Storage**: In-memory storage (data persists only while the API is running)

### Frontend (Angular)
- **Components**: Dashboard and Group Detail views
- **Services**: HTTP client services that communicate with the backend API
- **Models**: TypeScript interfaces matching the backend models

## Data Flow

1. User interacts with Angular UI
2. Angular service makes HTTP request to ASP.NET Core API
3. API controller receives request
4. Service layer processes business logic
5. Response sent back to Angular
6. Angular updates the UI

## Development Notes

- **CORS**: The backend is configured to allow requests from Angular dev server (ports 4200, 4201, 4202)
- **Data Persistence**: Currently using in-memory storage. Data will be lost when the API stops.
- **Port Configuration**: 
  - Backend: http://localhost:5024
  - Frontend: http://localhost:4200

## Quick Start Scripts

For convenience, you can use the provided PowerShell scripts:

### Start Backend
```powershell
.\start-backend.ps1
```

### Start Frontend (in a new terminal)
```powershell
.\start-frontend.ps1
```

## Troubleshooting

### Backend won't start
- Ensure .NET SDK 9.0 is installed: `dotnet --version`
- Check if port 5024 is already in use

### Frontend won't start
- Ensure Node.js is installed: `node --version`
- Install dependencies: `npm install`
- Check if port 4200 is already in use

### API connection errors
- Verify the backend is running on http://localhost:5024
- Check browser console for CORS errors
- Ensure `environment.ts` has correct API URL

## Technology Stack

### Backend
- ASP.NET Core 9.0
- C# 12
- Minimal API with Controllers
- In-memory data storage

### Frontend
- Angular 19
- TypeScript
- RxJS for reactive programming
- HttpClient for API communication
- Standalone components

## Future Enhancements

- Add database persistence (SQL Server, SQLite, or PostgreSQL)
- Implement authentication and authorization
- Add data export features (PDF, CSV)
- Add email notifications for settlements
- Implement file upload for receipts
