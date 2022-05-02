import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {AppConfigService} from "../configuration/app-config.service";
import {Observable} from "rxjs";
import {Token} from "../models/token";
import {UserRoles} from "../models/user-roles";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private accountUrl: string = "account";
  private loginUrl: string = "login";

  private tokenKey: string = "token";
  private refreshTokenKey: string = "refreshToken";
  private userNameKey: string = "userName";
  private inRoleUrl: string = "in-role"

  constructor(private httpClient: HttpClient, private router: Router, private config: AppConfigService) { }

  public login(userName: string, password: string): Observable<Token> {
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    const requestBody = JSON.stringify({
      'userName': userName,
      'password': password
    });

    return this.httpClient.post<Token>
    (`${this.config.apiBaseUrl}/${this.accountUrl}/${this.loginUrl}`, requestBody, {headers: headers});
  }

  public saveUserInfo(token: Token, userName: string) {
    localStorage.setItem(this.tokenKey, token.token);
    localStorage.setItem(this.refreshTokenKey, token.refreshToken);
    localStorage.setItem(this.userNameKey, userName);
  }

  public getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  public getRefreshToken(): string | null {
    return localStorage.getItem(this.refreshTokenKey);
  }

  public getUserName(): string | null {
    return localStorage.getItem(this.userNameKey);
  }

  public loggedIn(): boolean {
    return !!localStorage.getItem(this.tokenKey) && !!localStorage.getItem(this.refreshTokenKey);
  }

  public logout() {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.refreshTokenKey);
    localStorage.removeItem(this.userNameKey);
    this.router.navigate(['/login']);
  }

  public userInRole(role: string): Observable<boolean> {
    const userName: string | null = this.getUserName();

    if (userName === null) {
      throw Error("Username cannot be null");
    }

    return this.httpClient.get<boolean>(`${this.config.apiBaseUrl}/${this.accountUrl}/${userName}/${this.inRoleUrl}/${role}`);
  }

  public isAdmin(): Promise<boolean> {
    return this.userInRole(UserRoles.Admin).toPromise();
  }

  public isStudent(): Promise<boolean> {
    return this.userInRole(UserRoles.Student).toPromise();
  }

  public isTeacher(): Promise<boolean> {
    return this.userInRole(UserRoles.Teacher).toPromise();
  }
}
