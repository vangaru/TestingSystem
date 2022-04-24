import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {AppConfigService} from "../configuration/app-config.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenEndpoint: string = "token";
  private accountControllerEndpoint = "account";

  private accessTokenKey: string = "accessToken";
  private refreshTokenKey: string = "refreshToken";

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private config: AppConfigService) { }
}
