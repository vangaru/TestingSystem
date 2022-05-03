import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AppConfigService} from "../configuration/app-config.service";
import {Observable} from "rxjs";
import {CreatedTestsGridItem} from "../models/created-tests-grid-item";

@Injectable({
  providedIn: 'root'
})
export class TestsService {

  private testsUrl: string = "tests";

  constructor(private httpClient: HttpClient, private config: AppConfigService) { }

  public getCreatedTests(): Observable<CreatedTestsGridItem[]> {
    return this.httpClient.get<CreatedTestsGridItem[]>(`${this.config.apiBaseUrl}/${this.testsUrl}`);
  }

  public deleteTest(id: string): Observable<any> {
    return this.httpClient.delete<any>(`${this.config.apiBaseUrl}/${this.testsUrl}/${id}`);
  }
}
