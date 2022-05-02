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
}
