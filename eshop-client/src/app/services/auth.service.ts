import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://web-api.eastasia.azurecontainer.io:8080/api/Authentication';
  private currentUser: LoginResponseDTO | null = null;
  
  constructor(private http: HttpClient) {}

  login(loginUserDTO: LoginUserDTO): Observable<LoginResponseDTO> {
    return this.http.post<LoginResponseDTO>(`${this.apiUrl}/login`, loginUserDTO).pipe(
      tap(response => {
        this.currentUser = response;
        localStorage.setItem('currentUser', JSON.stringify(response));
      })
    );
  }

  register(registerUserDTO: RegisterUserDTO): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/register`, registerUserDTO, { responseType: 'text' as 'json' })
  }

  getCurrentUser(): LoginResponseDTO | null {
    if (this.currentUser) {
      return this.currentUser;
    }

    const storedUser = localStorage.getItem('currentUser');
    if (storedUser) {
      this.currentUser = JSON.parse(storedUser);
      return this.currentUser;
    }

    return null;
  }

  logout() {
    this.currentUser = null;
    localStorage.removeItem('currentUser');
  }
}



export interface RegisterUserDTO {
  username: string,
  email: string,
  password: string,
  role: string
}

export interface LoginUserDTO {
  email: string,
  password: string
}

interface LoginResponseDTO {
  username: string,
  email: string,
  token: string,
  roles: string[]
}
