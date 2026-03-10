import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction } from '../models/group.model';
import { environment } from '../../environments/environment';
import { GroupService } from './group.service';

@Injectable({
  providedIn: 'root'
})
export class SettlementService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private groupService: GroupService
  ) {}

  calculateOptimalSettlements(groupId: string): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.apiUrl}/groups/${groupId}/settlements`);
  }

  settleUp(groupId: string, from: string, to: string, amount: number): void {
    const settlement = {
      from,
      to,
      amount
    };

    this.http.post(`${this.apiUrl}/groups/${groupId}/settlements`, settlement).subscribe({
      next: () => {
        this.groupService.loadGroups();
      },
      error: (error) => {
        console.error('Error settling up:', error);
      }
    });
  }
}
