import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {TestsService} from "../../../../core/services/tests.service";
import {TestResultsGridItem} from "../../../../core/models/test-results-grid-item";
import {TestStatuses} from "../../../../core/models/test-statuses";

@Component({
  selector: 'app-test-results',
  templateUrl: './test-results.component.html',
  styleUrls: ['./test-results.component.css']
})
export class TestResultsComponent implements OnInit {

  private testId: string = "";
  public testResults: TestResultsGridItem[] = []
  public StatusAssigned = TestStatuses.Assigned;
  public StatusDone = TestStatuses.Done;

  constructor(private activatedRoute: ActivatedRoute, private testsService: TestsService) { }

  ngOnInit(): void {
    this.testId = this.activatedRoute.snapshot.params["id"];
    this.refreshTestResults();
  }

  private refreshTestResults() {
    this.testsService.getTestResults(this.testId).subscribe((testResults) => {
      this.testResults = [...testResults];
    });
  }
}
