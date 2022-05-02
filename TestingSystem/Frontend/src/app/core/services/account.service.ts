import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AppConfigService} from "../configuration/app-config.service";
import {Observable} from "rxjs";
import {AuthService} from "./auth.service";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private accountUrl: string = "account";
  private inRoleUrl: string = "in-role"

  constructor(private httpClient: HttpClient, private config: AppConfigService, private authService: AuthService) { }

  public userInRole(role: string): Observable<boolean> {
    const userName: string | null = this.authService.getUserName();

    if (userName === null) {
      throw Error("Username cannot be null");
    }

    return this.httpClient.get<boolean>(`${this.config.apiBaseUrl}/${this.accountUrl}/${userName}/${this.inRoleUrl}/${role}`);
  }
}
