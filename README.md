# SplitMate - Shared Expense Calculator

A simplified Splitwise-style shared expense calculator built with Angular and TypeScript. Manage groups, track expenses, and calculate who owes whom with an optimized settlement algorithm.

## Features

- **Group Management**: Create and manage multiple expense groups
- **Member Management**: Add/remove members within groups
- **Expense Tracking**: Record shared expenses with custom split amounts
- **Balance Calculation**: Automatic calculation of who owes whom
- **Settlement Optimization**: Minimizes number of transactions needed
- **LocalStorage Persistence**: All data stored in browser (no backend required)

## Installation

1. Install dependencies:
```bash
npm install
```

2. Start the development server:
```bash
npm start
```

3. Open your browser to `http://localhost:4200`

## Architecture

The application follows an MVC-style architecture:

- **Models**: TypeScript interfaces (Group, Expense, Balance, Settlement)
- **Services**: Business logic (GroupService, ExpenseService, BalanceService, SettlementService)
- **Components**: UI views (Dashboard, GroupDetail)

## Usage

1. Create a group (e.g., "Goa Trip")
2. Add members to the group
3. Add expenses with custom contributions
4. View balances to see who owes whom
5. Use settlements tab to mark debts as paid

## Technology Stack

- Angular 17
- TypeScript
- LocalStorage for data persistence
- Standalone components
- Reactive forms

## Project Structure

```
src/
├── app/
│   ├── models/          # TypeScript interfaces
│   ├── services/        # Business logic
│   ├── components/      # UI components
│   ├── app.component.ts
│   ├── app.routes.ts
│   └── app.config.ts
├── styles.css
├── index.html
└── main.ts
```
