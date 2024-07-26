import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private baseUrl = 'http://web-api.eastasia.azurecontainer.io:8080/api/admin';

  constructor(private http: HttpClient, private authService: AuthService) {}

  private getHeaders(): HttpHeaders {
    const storedUser = localStorage.getItem('currentUser');
    let token = '';

    if (storedUser) {
      try {
        const user = JSON.parse(storedUser);
        token = user.token; // Предположим, что токен хранится в свойстве 'token'
      } catch (e) {
        console.error('Ошибка при парсинге пользователя из localStorage', e);
      }
    }

    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getUsers(): Observable<GetUsersDTO[]> {
    return this.http.get<GetUsersDTO[]>(`${this.baseUrl}/users`, { headers: this.getHeaders() });
  }

  deleteUser(userId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/users/${userId}`, { headers: this.getHeaders() });
  }

  changeUserRole(userId: string, changeUserRoleDTO: ChangeUserRoleDTO): Observable<any> {
    return this.http.put(`${this.baseUrl}/users/${userId}/role`, changeUserRoleDTO, { headers: this.getHeaders() });
  }
}

export interface GetUsersDTO {
  userId: string;
  userName: string;
  email: string;
  phoneNumber: string;
  roles: string[];
}

export interface ChangeUserRoleDTO {
  newRole: string;
}
