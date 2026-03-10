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
    return 'status-active';
  }

  getGroupStatusText(group: Group): string {
    if (group.members.length === 0) {
      return 'No Members';
    }
    if (group.expenses.length === 0) {
      return 'No Expenses';
    }
    return 'Active';
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