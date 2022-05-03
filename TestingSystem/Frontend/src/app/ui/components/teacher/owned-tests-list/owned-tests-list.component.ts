import { Component, OnInit } from '@angular/core';
import {CreatedTestsGridItem} from "../../../../core/models/created-tests-grid-item";
import {TestsService} from "../../../../core/services/tests.service";

@Component({
  selector: 'app-owned-tests-list',
  templateUrl: './owned-tests-list.component.html',
  styleUrls: ['./owned-tests-list.component.css']
})
export class OwnedTestsListComponent implements OnInit {

  public createdTests: CreatedTestsGridItem[] = [];

  constructor(private testsService: TestsService) { }

  ngOnInit(): void {
    this.refreshTests();
  }

  private refreshTests() {
    this.testsService.getCreatedTests().subscribe(tests => {
      this.createdTests = [...tests];
    })
  }

  public deleteTest(id: string) {
    this.testsService.deleteTest(id).subscribe(
      () => {
        this.createdTests = [...this.createdTests.filter(test => test.testId !== id)];
      },
      () => {
        alert("failed to delete test");
      })
  }
}
