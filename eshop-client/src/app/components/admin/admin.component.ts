import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { GetUsersDTO, ChangeUserRoleDTO } from '../../services/admin.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  users: GetUsersDTO[] = [];
  newRole: string = '';

  constructor(
    private adminService: AdminService, 
    private authService: AuthService, 
    private router: Router
  ) {}

  ngOnInit(): void {
    const currentUser = this.authService.getCurrentUser();
    if (!currentUser || !currentUser.roles.includes('Admin')) {
      this.router.navigate(['/welcome-page']);
      return;
    }
    this.loadUsers();
  }

  loadUsers(): void {
    this.adminService.getUsers().subscribe(users => {
      this.users = users;
    });
  }

  deleteUser(userId: string): void {
    this.adminService.deleteUser(userId).subscribe(() => {
      this.loadUsers(); // Reload the users list after deletion
    });
  }

  changeUserRole(userId: string): void {
    const changeUserRoleDTO: ChangeUserRoleDTO = { newRole: this.newRole };
    this.adminService.changeUserRole(userId, changeUserRoleDTO).subscribe(() => {
      this.loadUsers(); // Reload the users list after role change
    });
  }
}
