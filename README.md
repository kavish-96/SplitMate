# 💰 SplitMate - Shared Expense Calculator

A full-stack expense splitting application built with **Angular** and **ASP.NET Core**. Track shared expenses, calculate balances, and settle debts with an optimized settlement algorithm.

[![Angular](https://img.shields.io/badge/Angular-19-red?logo=angular)](https://angular.io/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue?logo=.net)](https://dotnet.microsoft.com/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.0-blue?logo=typescript)](https://www.typescriptlang.org/)
[![C#](https://img.shields.io/badge/C%23-12-purple?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Architecture](#project-architecture)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Key Algorithms](#key-algorithms)
- [Learning Outcomes](#learning-outcomes)
- [Future Enhancements](#future-enhancements)
- [Troubleshooting](#troubleshooting)

---

## 🎯 Overview

SplitMate is a modern web application that simplifies the process of splitting expenses among groups. Whether it's a trip with friends, shared apartment costs, or office lunches, SplitMate helps you track who paid what and calculates the optimal way to settle debts.

### Technology Distribution
- **Frontend (Angular)**: ~90% of the codebase
- **Backend (ASP.NET Core)**: ~10% of the codebase

This distribution demonstrates full-stack development skills while emphasizing frontend expertise.

---

## ✨ Features

### 🏠 Group Management
- Create and manage multiple expense groups
- Add and remove members dynamically
- View all groups in an intuitive dashboard
- Real-time statistics (total groups, members, expenses)

### 💵 Expense Tracking
- Add expenses with custom split amounts
- Specify individual contributions (who paid how much)
- View detailed expense breakdowns
- Delete expenses with automatic recalculation
- Expandable expense cards showing participants and shares

### ⚖️ Balance Calculation
- Automatic calculation of member balances
- Shows total paid, total owed, and net balance
- Real-time updates after expense changes
- Visual indicators for positive/negative balances

### 🤝 Settlement Optimization
- Optimized algorithm to minimize transactions
- Shows who owes whom and exact amounts
- Mark settlements as complete
- Settlement history tracking
- Automatic balance adjustment after settlements

### 🎨 Modern UI/UX
- Clean, minimal design with gradient accents
- Glass morphism effects
- Responsive design for all devices
- Smooth animations and transitions
- Informative dashboard with statistics

---

## 🛠️ Technology Stack

### Frontend
- **Angular 19** - Modern web framework
- **TypeScript 5.0** - Type-safe JavaScript
- **RxJS** - Reactive programming
- **HttpClient** - API communication
- **Standalone Components** - Latest Angular architecture
- **CSS3** - Modern styling with gradients and animations

### Backend
- **ASP.NET Core 9.0** - Web API framework
- **C# 12** - Modern programming language
- **Minimal API** - Lightweight API architecture
- **Dependency Injection** - Service management
- **CORS** - Cross-origin resource sharing
- **In-Memory Storage** - Fast data access

---

## 🏗️ Project Architecture

### Frontend Structure
```
src/
├── app/
│   ├── components/
│   │   ├── dashboard/              # Group listing and creation
│   │   │   ├── dashboard.component.ts
│   │   │   ├── dashboard.component.html
│   │   │   └── dashboard.component.css
│   │   └── group-detail/           # Expense management
│   │       ├── group-detail.component.ts
│   │       ├── group-detail.component.html
│   │       └── group-detail.component.css
│   ├── models/
│   │   └── group.model.ts          # TypeScript interfaces
│   ├── services/
│   │   ├── group.service.ts        # Group HTTP operations
│   │   ├── expense.service.ts      # Expense HTTP operations
│   │   ├── balance.service.ts      # Balance calculations
│   │   └── settlement.service.ts   # Settlement operations
│   ├── app.component.ts
│   ├── app.config.ts               # HttpClient configuration
│   └── app.routes.ts               # Routing configuration
├── environments/
│   └── environment.ts              # API URL configuration
└── styles.css                      # Global styles
```

### Backend Structure
```
SplitMateAPI/
├── Controllers/
│   ├── GroupsController.cs         # Group CRUD operations
│   ├── ExpensesController.cs       # Expense management
│   ├── BalancesController.cs       # Balance calculation
│   └── SettlementsController.cs    # Settlement operations
├── Services/
│   ├── DataService.cs              # In-memory data store
│   ├── BalanceService.cs           # Balance calculation logic
│   └── SettlementService.cs        # Settlement optimization
├── Models/
│   └── Group.cs                    # Data models
└── Program.cs                      # App configuration
```

### Data Flow
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

---

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- **Node.js** (v18 or higher) - [Download](https://nodejs.org/)
- **.NET SDK 9.0** - [Download](https://dotnet.microsoft.com/download)
- **Angular CLI** - Install globally:
  ```bash
  npm install -g @angular/cli
  ```

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd SplitMate
   ```

2. **Install frontend dependencies**
   ```bash
   npm install
   ```

3. **Verify installations**
   ```bash
   node --version
   dotnet --version
   ng version
   ```

### Running the Application

#### Option 1: Using PowerShell Scripts (Recommended)

**Terminal 1 - Start Backend:**
```powershell
.\start-backend.ps1
```

**Terminal 2 - Start Frontend:**
```powershell
.\start-frontend.ps1
```

#### Option 2: Manual Start

**Terminal 1 - Start Backend:**
```bash
cd SplitMateAPI
dotnet run
```
Backend will run on: **http://localhost:5024**

**Terminal 2 - Start Frontend:**
```bash
ng serve
```
Frontend will run on: **http://localhost:4200**

### Access the Application

Open your browser and navigate to:
```
http://localhost:4200
```

You should see the SplitMate dashboard with statistics and group management features.

---

## 📡 API Documentation

### Groups API

| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| GET | `/api/groups` | Get all groups | - | `Group[]` |
| GET | `/api/groups/{id}` | Get specific group | - | `Group` |
| POST | `/api/groups` | Create new group | `{ groupName: string }` | `Group` |
| DELETE | `/api/groups/{id}` | Delete group | - | `204 No Content` |
| POST | `/api/groups/{id}/members` | Add member | `{ memberName: string }` | `200 OK` |
| DELETE | `/api/groups/{id}/members/{name}` | Remove member | - | `204 No Content` |

### Expenses API

| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| POST | `/api/groups/{groupId}/expenses` | Add expense | `Expense` | `Expense` |
| DELETE | `/api/groups/{groupId}/expenses/{expenseId}` | Delete expense | - | `204 No Content` |

### Balances API

| Method | Endpoint | Description | Response |
|--------|----------|-------------|----------|
| GET | `/api/groups/{groupId}/balances` | Calculate balances | `Balance[]` |

### Settlements API

| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| GET | `/api/groups/{groupId}/settlements` | Get optimal settlements | - | `Transaction[]` |
| POST | `/api/groups/{groupId}/settlements` | Mark settlement complete | `Settlement` | `Settlement` |

### Data Models

#### Group
```typescript
{
  groupId: string;
  groupName: string;
  members: string[];
  expenses: Expense[];
  settlements: Settlement[];
}
```

#### Expense
```typescript
{
  expenseId: string;
  title: string;
  remark: string;
  amount: number;
  paidBy: string;
  splitAmong: string[];
  contributionMap: { [member: string]: number };
  timestamp: number;
}
```

#### Balance
```typescript
{
  member: string;
  totalPaid: number;
  totalOwed: number;
  netBalance: number;
}
```

#### Transaction
```typescript
{
  from: string;
  to: string;
  amount: number;
}
```

---

## 🧮 Key Algorithms

### Balance Calculation Algorithm

```
1. Initialize balance for each member:
   - totalPaid = 0
   - totalOwed = 0
   - netBalance = 0

2. For each expense:
   a. Add contributions to totalPaid
      (from contributionMap)
   
   b. Calculate share per person:
      share = expense.amount / participants.length
   
   c. Add share to totalOwed for each participant

3. Calculate net balance:
   netBalance = totalPaid - totalOwed

4. Adjust for completed settlements:
   - Payer: netBalance += settlement.amount
   - Receiver: netBalance -= settlement.amount
```

### Settlement Optimization Algorithm

```
1. Calculate all member balances

2. Separate members:
   - Creditors: members with positive balance (should receive)
   - Debtors: members with negative balance (should pay)

3. Sort:
   - Creditors: descending by balance
   - Debtors: ascending by balance (most negative first)

4. Optimize transactions:
   while (creditors exist AND debtors exist):
     a. Take largest creditor and largest debtor
     b. Settlement amount = min(creditor.balance, debtor.balance)
     c. Create transaction: debtor → creditor (amount)
     d. Update balances
     e. Remove if balance becomes zero

5. Result: Minimum number of transactions
```

**Example:**
```
Initial Balances:
- Alice: +500 (should receive)
- Bob: -200 (should pay)
- Charlie: -300 (should pay)

Optimized Settlements:
1. Bob pays Alice ₹200
2. Charlie pays Alice ₹300

Total: 2 transactions (instead of potentially more)
```

---

## 📚 Learning Outcomes

### ASP.NET Core Concepts

✅ **Web API Development**
- RESTful API design principles
- HTTP methods and status codes
- Route attributes and URL patterns

✅ **Dependency Injection**
- Service registration (Singleton pattern)
- Constructor injection
- Service lifetime management

✅ **Service Layer Architecture**
- Separation of concerns
- Business logic isolation
- Controller-Service pattern

✅ **CORS Configuration**
- Cross-origin resource sharing
- Policy-based configuration
- Security considerations

✅ **Model Binding**
- Request body deserialization
- Route parameters
- Query string parameters

### Angular Concepts

✅ **Component Architecture**
- Standalone components
- Component lifecycle hooks
- Component communication

✅ **Services & Dependency Injection**
- Injectable services
- Singleton services
- Service hierarchy

✅ **HTTP Client & Observables**
- HttpClient module
- RxJS operators
- Async data handling
- Error handling

✅ **Routing & Navigation**
- Route configuration
- Route parameters
- Programmatic navigation
- Router outlet

✅ **Forms & Data Binding**
- Two-way data binding
- Template-driven forms
- Event handling
- Form validation

✅ **State Management**
- BehaviorSubject for state
- Observable subscriptions
- Data synchronization

### Full-Stack Integration

✅ **Frontend-Backend Communication**
- RESTful API consumption
- HTTP request/response cycle
- JSON data exchange

✅ **CORS Handling**
- Cross-origin requests
- Preflight requests
- Security headers

✅ **Error Handling**
- HTTP error responses
- User-friendly error messages
- Graceful degradation

✅ **Async Operations**
- Promise vs Observable
- Async/await patterns
- Loading states

---

## 🎓 Usage Guide

### Creating Your First Group

1. **Navigate to Dashboard**
   - Click "Create Group" button
   - Enter group name (e.g., "Weekend Trip")
   - Click "Create"

2. **Add Members**
   - Open the group
   - Click "Add Member"
   - Enter member names one by one
   - Minimum 2 members required for expenses

3. **Add an Expense**
   - Go to "Expenses" tab
   - Click "Add Expense"
   - Fill in details:
     - Title: "Hotel Booking"
     - Amount: 5000
     - Select participants
     - Enter contributions (who paid how much)
   - Click "Add Expense"

4. **View Balances**
   - Go to "Balances" tab
   - See who paid what and who owes what
   - Green = should receive money
   - Red = should pay money

5. **Settle Up**
   - Go to "Settlements" tab
   - See optimized transactions
   - Click "Settle Up" to mark as paid
   - Balance automatically updates

---

## 🔮 Future Enhancements

### Backend Improvements
- [ ] **Database Integration**
  - SQL Server or PostgreSQL
  - Entity Framework Core
  - Data persistence across restarts

- [ ] **Authentication & Authorization**
  - JWT token-based auth
  - User registration and login
  - Role-based access control

- [ ] **Advanced Features**
  - Data validation attributes
  - Logging and monitoring
  - Unit and integration tests
  - API versioning

### Frontend Improvements
- [ ] **State Management**
  - NgRx for complex state
  - Redux DevTools integration
  - Optimistic updates

- [ ] **UI/UX Enhancements**
  - Loading indicators
  - Toast notifications
  - Skeleton screens
  - Dark mode support

- [ ] **Testing**
  - Unit tests (Jasmine/Karma)
  - E2E tests (Cypress)
  - Component testing

### Feature Additions
- [ ] **User Management**
  - User profiles
  - Multi-user support
  - Group invitations

- [ ] **Export & Reports**
  - PDF expense reports
  - CSV data export
  - Email summaries

- [ ] **Advanced Functionality**
  - Receipt upload and OCR
  - Multi-currency support
  - Expense categories
  - Recurring expenses
  - Budget tracking

- [ ] **Notifications**
  - Email notifications
  - Push notifications
  - Settlement reminders

---

## 🐛 Troubleshooting

### Backend Issues

**Problem: Backend won't start**
```bash
# Check .NET SDK version
dotnet --version

# Should show 9.0.x or higher
# If not, download from: https://dotnet.microsoft.com/download
```

**Problem: Port 5024 already in use**
```bash
# Find process using port
netstat -ano | findstr :5024

# Kill the process (Windows)
taskkill /PID <process-id> /F

# Or change port in launchSettings.json
```

**Problem: CORS errors**
- Verify backend is running
- Check `Program.cs` CORS configuration
- Ensure Angular port is in allowed origins

### Frontend Issues

**Problem: Frontend won't start**
```bash
# Check Node.js version
node --version

# Should show v18 or higher
# Install dependencies
npm install

# Clear cache if needed
npm cache clean --force
```

**Problem: Port 4200 already in use**
```bash
# Use different port
ng serve --port 4201

# Or kill existing process
```

**Problem: API connection errors**
- Verify backend is running on http://localhost:5024
- Check browser console for errors
- Verify `environment.ts` has correct API URL:
  ```typescript
  apiUrl: 'http://localhost:5024/api'
  ```

### Common Issues

**Problem: Data not persisting**
- Remember: In-memory storage is used
- Data is lost when backend stops
- This is by design for development

**Problem: Build errors**
```bash
# Clean and rebuild
rm -rf node_modules
npm install
ng build
```

**Problem: TypeScript errors**
```bash
# Check TypeScript version
npm list typescript

# Update if needed
npm install typescript@latest
```

---

## 📝 Development Notes

### Port Configuration
- **Backend API**: http://localhost:5024
- **Frontend App**: http://localhost:4200
- **CORS**: Configured for ports 4200, 4201, 4202

### Data Persistence
- **Current**: In-memory storage
- **Behavior**: Data lost when API stops
- **Future**: Database integration planned

### Code Style
- **Backend**: C# conventions, PascalCase
- **Frontend**: TypeScript conventions, camelCase
- **Formatting**: Consistent indentation and spacing

### Git Workflow
```bash
# Create feature branch
git checkout -b feature/your-feature

# Commit changes
git add .
git commit -m "Add: your feature description"

# Push to remote
git push origin feature/your-feature
```

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

---

## 📄 License

This project is created for educational purposes as part of a .NET and Angular course curriculum.

---

## 👥 Authors

- **Your Name** - Initial work and development

---

## 🙏 Acknowledgments

- Inspired by Splitwise
- Built with Angular and ASP.NET Core
- Uses modern web development practices
- Demonstrates full-stack development skills

---

## 📞 Support

For issues, questions, or suggestions:
- Open an issue on GitHub
- Contact the development team
- Check the troubleshooting section

---

**Made with ❤️ using Angular and ASP.NET Core**
