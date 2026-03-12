import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { GroupService } from '../../services/group.service';
import { Group } from '../../models/group.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  groups: Group[] = [];
  showCreateForm = false;
  newGroupName = '';

  constructor(
    private groupService: GroupService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.groupService.groups$.subscribe((groups: Group[]) => {
      this.groups = groups;
    });
  }

  toggleCreateForm(): void {
    this.showCreateForm = !this.showCreateForm;
    this.newGroupName = '';
  }

  createGroup(): void {
    if (this.newGroupName.trim()) {
      this.groupService.createGroup(this.newGroupName.trim());
      this.newGroupName = '';
      this.showCreateForm = false;
    }
  }

  openGroup(groupId: string): void {
    this.router.navigate(['/group', groupId]);
  }

  deleteGroup(groupId: string, event: Event): void {
    event.stopPropagation();
    if (confirm('Are you sure you want to delete this group?')) {
      this.groupService.deleteGroup(groupId);
    }
  }

  getGroupStatus(group: Group): string {
    if (group.members.length === 0) {
      return 'status-empty';
    }
    if (group.expenses.length === 0) {
      return 'status-empty';
    }
    
    // Check if all balances are settled
    if (this.isGroupSettled(group)) {
      return 'status-settled';
    }
    
    return 'status-active';
  }

  getGroupStatusText(group: Group): string {
    if (group.members.length === 0) {
      return 'No Members';
    }
    if (group.expenses.length === 0) {
      return 'No Expenses';
    }
    
    // Check if all balances are settled
    if (this.isGroupSettled(group)) {
      return 'Settled';
    }
    
    return 'Active';
  }

  // Check if group is fully settled (all balances are zero or near zero)
  isGroupSettled(group: Group): boolean {
    if (group.expenses.length === 0) {
      return false;
    }

    // Calculate balances for each member
    const balanceMap = new Map<string, number>();

    // Initialize balances
    group.members.forEach(member => {
      balanceMap.set(member, 0);
    });

    // Calculate from expenses
    group.expenses.forEach(expense => {
      const sharePerPerson = expense.amount / expense.splitAmong.length;

      // Add contributions (what each person paid)
      Object.entries(expense.contributionMap).forEach(([member, amount]) => {
        const currentBalance = balanceMap.get(member) || 0;
        balanceMap.set(member, currentBalance + amount);
      });

      // Subtract shares (what each person owes)
      expense.splitAmong.forEach(member => {
        const currentBalance = balanceMap.get(member) || 0;
        balanceMap.set(member, currentBalance - sharePerPerson);
      });
    });

    // Adjust for completed settlements
    group.settlements.forEach(settlement => {
      if (settlement.completed) {
        const fromBalance = balanceMap.get(settlement.from) || 0;
        balanceMap.set(settlement.from, fromBalance + settlement.amount);

        const toBalance = balanceMap.get(settlement.to) || 0;
        balanceMap.set(settlement.to, toBalance - settlement.amount);
      }
    });

    // Check if all balances are near zero (within 0.01 tolerance)
    const allSettled = Array.from(balanceMap.values()).every(balance => 
      Math.abs(balance) < 0.01
    );

    return allSettled;
  }

  getTotalMembers(): number {
    const uniqueMembers = new Set<string>();
    this.groups.forEach(group => {
      group.members.forEach(member => uniqueMembers.add(member));
    });
    return uniqueMembers.size;
  }

  getTotalExpenses(): number {
    return this.groups.reduce((total, group) => total + group.expenses.length, 0);
  }
}