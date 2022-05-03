import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AppConfigService} from "../configuration/app-config.service";
import {Observable} from "rxjs";
import {User} from "../models/user";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private accountUrl: string = "account";
  private studentsUrl: string = "students";

  constructor(private httpClient: HttpClient, private config: AppConfigService) { }

  public getUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(`${this.config.apiBaseUrl}/${this.accountUrl}`);
  }

  public deleteUser(id: string): Observable<any> {
    return this.httpClient.delete(`${this.config.apiBaseUrl}/${this.accountUrl}/${id}`);
  }

  public getStudentNames(): Observable<string> {
    return this.httpClient.get<string>(`${this.config.apiBaseUrl}/${this.accountUrl}/${this.studentsUrl}`);
  }
}
