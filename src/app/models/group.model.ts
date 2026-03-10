export interface Group {
  groupId: string;
  groupName: string;
  members: string[];
  expenses: Expense[];
  settlements: Settlement[];
}

export interface Expense {
  expenseId: string;
  title: string;
  remark: string;
  amount: number;
  paidBy: string;
  splitAmong: string[];
  contributionMap: { [member: string]: number };
  timestamp: number;
}

export interface Settlement {
  settlementId: string;
  from: string;
  to: string;
  amount: number;
  timestamp: number;
  completed: boolean;
}

export interface Balance {
  member: string;
  totalPaid: number;
  totalOwed: number;
  netBalance: number;
}

export interface Transaction {
  from: string;
  to: string;
  amount: number;
}