import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Group } from '../models/group.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private apiUrl = `${environment.apiUrl}/groups`;
  private groupsSubject = new BehaviorSubject<Group[]>([]);
  public groups$ = this.groupsSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadGroups();
  }

  loadGroups(): void {
    this.http.get<Group[]>(this.apiUrl).subscribe({
      next: (groups) => {
        this.groupsSubject.next(groups);
      },
      error: (error) => {
        console.error('Error loading groups:', error);
      }
    });
  }

  getGroupById(groupId: string): Group | undefined {
    return this.groupsSubject.value.find(g => g.groupId === groupId);
  }

  createGroup(groupName: string): void {
    this.http.post<Group>(this.apiUrl, { groupName }).subscribe({
      next: () => {
        this.loadGroups();
      },
      error: (error) => {
        console.error('Error creating group:', error);
      }
    });
  }

  deleteGroup(groupId: string): void {
    this.http.delete(`${this.apiUrl}/${groupId}`).subscribe({
      next: () => {
        this.loadGroups();
      },
      error: (error) => {
        console.error('Error deleting group:', error);
      }
    });
  }

  addMember(groupId: string, memberName: string): void {
    this.http.post(`${this.apiUrl}/${groupId}/members`, { memberName }).subscribe({
      next: () => {
        this.loadGroups();
      },
      error: (error) => {
        console.error('Error adding member:', error);
      }
    });
  }

  removeMember(groupId: string, memberName: string): void {
    this.http.delete(`${this.apiUrl}/${groupId}/members/${memberName}`).subscribe({
      next: () => {
        this.loadGroups();
      },
      error: (error) => {
        console.error('Error removing member:', error);
      }
    });
  }
}
