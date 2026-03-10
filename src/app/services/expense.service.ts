import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { GroupService } from './group.service';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private groupService: GroupService
  ) {}

  addExpense(groupId: string, expenseData: any): void {
    const expense = {
      title: expenseData.title,
      remark: expenseData.remark,
      amount: expenseData.amount,
      paidBy: expenseData.paidBy,
      splitAmong: expenseData.splitAmong,
      contributionMap: expenseData.contributionMap
    };

    this.http.post(`${this.apiUrl}/groups/${groupId}/expenses`, expense).subscribe({
      next: () => {
        this.groupService.loadGroups();
      },
      error: (error) => {
        console.error('Error adding expense:', error);
      }
    });
  }

  deleteExpense(groupId: string, expenseId: string): void {
    this.http.delete(`${this.apiUrl}/groups/${groupId}/expenses/${expenseId}`).subscribe({
      next: () => {
        this.groupService.loadGroups();
      },
      error: (error) => {
        console.error('Error deleting expense:', error);
      }
    });
  }
}
