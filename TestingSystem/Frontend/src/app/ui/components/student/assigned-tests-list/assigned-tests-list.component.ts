import { Component, OnInit } from '@angular/core';
import {AssignedTestsGridItem} from "../../../../core/models/assigned-tests-grid-item";
import {TestsService} from "../../../../core/services/tests.service";
import {TestStatuses} from "../../../../core/models/test-statuses";

@Component({
  selector: 'app-assigned-tests-list',
  templateUrl: './assigned-tests-list.component.html',
  styleUrls: ['./assigned-tests-list.component.css']
})
export class AssignedTestsListComponent implements OnInit {

  public assignedTests: AssignedTestsGridItem[] = [];
  public StatusAssigned = TestStatuses.Assigned;
  public StatusDone = TestStatuses.Done;

  constructor(private testsService: TestsService) { }

  ngOnInit(): void {
    this.refreshTests();
  }

  private refreshTests() {
    this.testsService.getAssignedTests().subscribe((tests) => {
      this.assignedTests = [...tests];
    })
  }
}
