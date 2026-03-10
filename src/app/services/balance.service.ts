import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Balance } from '../models/group.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BalanceService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  calculateBalances(groupId: string): Observable<Balance[]> {
    return this.http.get<Balance[]>(`${this.apiUrl}/groups/${groupId}/balances`);
  }
}
