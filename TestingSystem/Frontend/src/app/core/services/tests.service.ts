import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {AppConfigService} from "../configuration/app-config.service";
import {Observable} from "rxjs";
import {CreatedTestsGridItem} from "../models/created-tests-grid-item";
import {CreateTestModel} from "../models/create-test-model";
import {TestResultsGridItem} from "../models/test-results-grid-item";

@Injectable({
  providedIn: 'root'
})
export class TestsService {

  private testsUrl: string = "tests";
  private teacherUrl: string = "teacher";

  constructor(private httpClient: HttpClient, private config: AppConfigService) { }

  public getCreatedTests(): Observable<CreatedTestsGridItem[]> {
    return this.httpClient.get<CreatedTestsGridItem[]>(`${this.config.apiBaseUrl}/${this.testsUrl}/${this.teacherUrl}`);
  }

  public deleteTest(id: string): Observable<any> {
    return this.httpClient.delete<any>(`${this.config.apiBaseUrl}/${this.testsUrl}/${this.teacherUrl}/${id}`);
  }

  public createTest(test: CreateTestModel): Observable<any> {
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    const requestBody = JSON.stringify(test);

    return this.httpClient.post<any>(`${this.config.apiBaseUrl}/${this.testsUrl}/${this.teacherUrl}`, requestBody, {headers: headers});
  }

  public getTestResults(id: string): Observable<TestResultsGridItem[]> {
    return this.httpClient.get<TestResultsGridItem[]>(`${this.config.apiBaseUrl}/${this.testsUrl}/${this.teacherUrl}/${id}`);
  }
}
