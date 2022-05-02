import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AppConfigService} from "../configuration/app-config.service";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private accountUrl: string = "account";

  constructor(private httpClient: HttpClient, private config: AppConfigService) { }

}
