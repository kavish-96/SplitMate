# SplitMate - Project Summary

## Overview

SplitMate is a full-stack expense splitting application that helps users manage shared expenses among groups. The application demonstrates the integration of **Angular (Frontend)** and **ASP.NET Core Web API (Backend)**.

## Technology Distribution

- **Frontend (Angular)**: ~90% of the codebase
- **Backend (ASP.NET Core)**: ~10% of the codebase

This distribution satisfies the requirement of using ASP.NET Core while keeping Angular as the primary technology.

## Project Architecture

### Frontend - Angular 19
```
src/
├── app/
│   ├── components/
│   │   ├── dashboard/          # Group listing and creation
│   │   └── group-detail/       # Expense management, balances, settlements
│   ├── models/
│   │   └── group.model.ts      # TypeScript interfaces
│   ├── services/
│   │   ├── group.service.ts    # HTTP calls for group operations
│   │   ├── expense.service.ts  # HTTP calls for expense operations
│   │   ├── balance.service.ts  # HTTP calls for balance calculation
│   │   └── settlement.service.ts # HTTP calls for settlements
│   ├── app.component.ts
│   ├── app.config.ts           # HttpClient configuration
│   └── app.routes.ts
├── environments/
│   └── environment.ts          # API URL configuration
└── styles.css                  # Global styles
```

### Backend - ASP.NET Core 9.0
```
SplitMateAPI/
├── Controllers/
│   ├── GroupsController.cs     # Group CRUD operations
│   ├── ExpensesController.cs   # Expense management
│   ├── BalancesController.cs   # Balance calculation endpoint
│   └── SettlementsController.cs # Settlement operations
├── Services/
│   ├── DataService.cs          # In-memory data management
│   ├── BalanceService.cs       # Balance calculation logic
│   └── SettlementService.cs    # Settlement optimization algorithm
├── Models/
│   └── Group.cs                # Data models (Group, Expense, Settlement, etc.)
└── Program.cs                  # Application configuration
```

## Key Features

### 1. Group Management
- Create and delete groups
- Add and remove members
- View all groups in dashboard

### 2. Expense Management
- Add expenses with custom split amounts
- Specify who paid how much
- View expense details (participants, contributions)
- Delete expenses

### 3. Balance Calculation
- Automatic calculation of member balances
- Shows total paid, total owed, and net balance
- Real-time updates after expense changes

### 4. Settlement Optimization
- Optimized algorithm to minimize number of transactions
- Shows who owes whom and how much
- Mark settlements as complete
- Settlement history tracking

## API Endpoints

### Groups API
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/groups` | Get all groups |
| GET | `/api/groups/{id}` | Get specific group |
| POST | `/api/groups` | Create new group |
| DELETE | `/api/groups/{id}` | Delete group |
| POST | `/api/groups/{id}/members` | Add member |
| DELETE | `/api/groups/{id}/members/{name}` | Remove member |

### Expenses API
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/groups/{groupId}/expenses` | Add expense |
| DELETE | `/api/groups/{groupId}/expenses/{expenseId}` | Delete expense |

### Balances API
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/groups/{groupId}/balances` | Calculate balances |

### Settlements API
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/groups/{groupId}/settlements` | Get optimal settlements |
| POST | `/api/groups/{groupId}/settlements` | Mark settlement complete |

## Data Flow

```
User Action (Angular UI)
    ↓
Angular Service (HTTP Request)
    ↓
ASP.NET Core Controller
    ↓
Service Layer (Business Logic)
    ↓
In-Memory Data Store
    ↓
Response (JSON)
    ↓
Angular Service (Observable)
    ↓
Component (Update UI)
```

## ASP.NET Core Implementation Details

### 1. Controllers
- **RESTful API design** following HTTP conventions
- **Dependency Injection** for services
- **Action methods** with proper HTTP verbs
- **Route attributes** for clean URLs

### 2. Services
- **Singleton pattern** for in-memory storage
- **Business logic separation** from controllers
- **Algorithm implementation** for settlement optimization
- **Balance calculation** with settlement adjustments

### 3. Models
- **C# classes** with properties
- **Data annotations** for validation
- **Collections** (List, Dictionary) for complex data
- **Type safety** with strongly-typed models

### 4. Configuration
- **CORS policy** for Angular integration
- **Service registration** in Program.cs
- **Minimal API** with controllers
- **Development environment** configuration

## Angular Implementation Details

### 1. Services
- **HttpClient** for API communication
- **RxJS Observables** for async operations
- **BehaviorSubject** for state management
- **Error handling** with subscribe callbacks

### 2. Components
- **Standalone components** (Angular 19 feature)
- **Reactive forms** with FormsModule
- **Event handling** for user interactions
- **Conditional rendering** with *ngIf and *ngFor

### 3. Routing
- **Angular Router** for navigation
- **Route parameters** for group detail view
- **Programmatic navigation** in services

### 4. Styling
- **Global styles** with CSS
- **Component-specific styles** with scoped CSS
- **Responsive design** with flexbox and grid
- **Modern UI** with gradients and animations

## Key Algorithms

### Balance Calculation
1. Initialize balance for each member
2. For each expense:
   - Add contributions to totalPaid
   - Calculate share per person
   - Add share to totalOwed
3. Calculate netBalance = totalPaid - totalOwed
4. Adjust for completed settlements

### Settlement Optimization
1. Calculate all member balances
2. Separate into creditors (positive) and debtors (negative)
3. Sort creditors descending, debtors ascending
4. Match largest creditor with largest debtor
5. Create transaction for minimum of both amounts
6. Repeat until all balanced

## Learning Outcomes

### ASP.NET Core Concepts Demonstrated
- ✅ Web API development
- ✅ RESTful API design
- ✅ Dependency Injection
- ✅ Service layer architecture
- ✅ CORS configuration
- ✅ Controller actions and routing
- ✅ Model binding
- ✅ HTTP status codes

### Angular Concepts Demonstrated
- ✅ Component architecture
- ✅ Services and dependency injection
- ✅ HTTP client and observables
- ✅ Routing and navigation
- ✅ Forms and data binding
- ✅ State management
- ✅ Reactive programming with RxJS

### Full-Stack Integration
- ✅ Frontend-backend communication
- ✅ API consumption
- ✅ CORS handling
- ✅ Error handling
- ✅ Data synchronization
- ✅ Async operations

## Future Enhancements

### Backend
- [ ] Add SQL Server/SQLite database
- [ ] Implement Entity Framework Core
- [ ] Add authentication (JWT)
- [ ] Add data validation attributes
- [ ] Implement logging
- [ ] Add unit tests

### Frontend
- [ ] Add authentication UI
- [ ] Implement state management (NgRx)
- [ ] Add loading indicators
- [ ] Implement error notifications
- [ ] Add data export features
- [ ] Add unit and e2e tests

### Features
- [ ] User authentication and authorization
- [ ] Email notifications
- [ ] Receipt upload
- [ ] PDF/CSV export
- [ ] Multi-currency support
- [ ] Expense categories

## Conclusion

This project successfully demonstrates:
1. **Full-stack development** with Angular and ASP.NET Core
2. **RESTful API** design and implementation
3. **Modern web development** practices
4. **Clean architecture** with separation of concerns
5. **Real-world application** solving actual problems

The 90-10 split between Angular and ASP.NET Core satisfies the syllabus requirement while showcasing proficiency in both technologies.
