import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { GroupService } from '../../services/group.service';
import { ExpenseService } from '../../services/expense.service';
import { BalanceService } from '../../services/balance.service';
import { SettlementService } from '../../services/settlement.service';
import { Group, Expense, Balance, Transaction } from '../../models/group.model';

@Component({
  selector: 'app-group-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './group-detail.component.html',
  styleUrls: ['./group-detail.component.css']
})
export class GroupDetailComponent implements OnInit {
  group: Group | undefined;
  activeTab: 'expenses' | 'balances' | 'settlements' = 'expenses';
  
  // Member management
  showAddMember = false;
  newMemberName = '';
  
  // Expense management
  showAddExpense = false;
  newExpense = {
    title: '',
    remark: '',
    amount: 0,
    paidBy: '',
    splitAmong: [] as string[],
    contributionMap: {} as { [key: string]: number }
  };
  
  // Expense details
  expandedExpenseId: string | null = null;
  
  balances: Balance[] = [];
  settlements: Transaction[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private groupService: GroupService,
    private expenseService: ExpenseService,
    private balanceService: BalanceService,
    private settlementService: SettlementService
  ) {}

  ngOnInit(): void {
    const groupId = this.route.snapshot.paramMap.get('id');
    if (groupId) {
      this.loadGroup(groupId);
      this.groupService.groups$.subscribe(() => {
        this.loadGroup(groupId);
      });
    }
  }

  loadGroup(groupId: string): void {
    this.group = this.groupService.getGroupById(groupId);
    if (this.group) {
      console.log('Loading group:', this.group.groupName);
      console.log('Expenses:', this.group.expenses);
      
      // Load balances asynchronously
      this.balanceService.calculateBalances(groupId).subscribe({
        next: (balances) => {
          this.balances = balances;
          console.log('Balances:', this.balances);
        },
        error: (error) => {
          console.error('Error loading balances:', error);
        }
      });
      
      // Load settlements asynchronously
      this.settlementService.calculateOptimalSettlements(groupId).subscribe({
        next: (settlements) => {
          this.settlements = settlements;
          console.log('Settlements:', this.settlements);
        },
        error: (error) => {
          console.error('Error loading settlements:', error);
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  setActiveTab(tab: 'expenses' | 'balances' | 'settlements'): void {
    this.activeTab = tab;
  }

  // Helper methods for UI
  getInitials(name: string): string {
    return name.split(' ').map(n => n[0]).join('').toUpperCase().substring(0, 2);
  }

  // Member Management
  toggleAddMember(): void {
    this.showAddMember = !this.showAddMember;
    this.newMemberName = '';
  }

  addMember(): void {
    if (this.group && this.newMemberName.trim()) {
      this.groupService.addMember(this.group.groupId, this.newMemberName.trim());
      this.newMemberName = '';
      this.showAddMember = false;
    }
  }

  removeMember(memberName: string): void {
    if (this.group && confirm(`Remove ${memberName} from group?`)) {
      this.groupService.removeMember(this.group.groupId, memberName);
    }
  }

  // Expense Management
  toggleAddExpense(): void {
    this.showAddExpense = !this.showAddExpense;
    this.resetExpenseForm();
  }

  resetExpenseForm(): void {
    this.newExpense = {
      title: '',
      remark: '',
      amount: 0,
      paidBy: '',
      splitAmong: [],
      contributionMap: {}
    };
  }

  toggleParticipant(member: string, event?: Event): void {
    if (event) {
      event.stopPropagation();
    }
    
    const index = this.newExpense.splitAmong.indexOf(member);
    if (index > -1) {
      this.newExpense.splitAmong.splice(index, 1);
      delete this.newExpense.contributionMap[member];
    } else {
      this.newExpense.splitAmong.push(member);
      this.newExpense.contributionMap[member] = 0;
    }
    
    console.log('Participants selected:', this.newExpense.splitAmong);
  }

  isParticipant(member: string): boolean {
    return this.newExpense.splitAmong.includes(member);
  }

  addExpense(): void {
    if (!this.group) return;

    if (!this.newExpense.title.trim()) {
      alert('Please enter expense title');
      return;
    }

    if (this.newExpense.amount <= 0) {
      alert('Please enter valid amount');
      return;
    }

    if (this.newExpense.splitAmong.length === 0) {
      alert('Please select participants');
      return;
    }

    const totalContribution = Object.values(this.newExpense.contributionMap).reduce((a, b) => a + b, 0);
    if (Math.abs(totalContribution - this.newExpense.amount) > 0.01) {
      alert(`Total contributions (${totalContribution}) must equal expense amount (${this.newExpense.amount})`);
      return;
    }

    console.log('Adding expense:', this.newExpense);
    this.expenseService.addExpense(this.group.groupId, this.newExpense);
    console.log('Expense added, resetting form');
    this.resetExpenseForm();
    this.showAddExpense = false;
  }

  deleteExpense(expenseId: string): void {
    if (this.group && confirm('Delete this expense?')) {
      this.expenseService.deleteExpense(this.group.groupId, expenseId);
    }
  }

  formatDate(timestamp: number): string {
    return new Date(timestamp).toLocaleDateString();
  }

  settleUp(transaction: Transaction): void {
    if (this.group && confirm(`Mark settlement: ${transaction.from} pays ${transaction.to} ₹${transaction.amount}?`)) {
      this.settlementService.settleUp(this.group.groupId, transaction.from, transaction.to, transaction.amount);
    }
  }

  // Expense detail methods
  toggleExpenseDetails(expenseId: string): void {
    if (this.expandedExpenseId === expenseId) {
      this.expandedExpenseId = null; // Collapse if already expanded
    } else {
      this.expandedExpenseId = expenseId; // Expand this expense
    }
  }

  isExpenseExpanded(expenseId: string): boolean {
    return this.expandedExpenseId === expenseId;
  }

  getContributorsList(expense: Expense): string[] {
    return Object.entries(expense.contributionMap)
      .filter(([_, amount]) => amount > 0)
      .map(([member, _]) => member);
  }

  getContributionAmount(expense: Expense, member: string): number {
    return expense.contributionMap[member] || 0;
  }

  getSharePerPerson(expense: Expense): number {
    return expense.amount / expense.splitAmong.length;
  }
}