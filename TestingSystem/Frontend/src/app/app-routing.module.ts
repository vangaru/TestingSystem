import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {UsersListComponent} from "./ui/components/admin/users-list/users-list.component";
import {AssignedTestsListComponent} from "./ui/components/student/assigned-tests-list/assigned-tests-list.component";
import {OwnedTestsListComponent} from "./ui/components/teacher/owned-tests-list/owned-tests-list.component";

export const appRoutes: Routes = [
  { path: 'admin', redirectTo: '/admin/users', pathMatch: 'full' },
  { path: 'admin/users', component: UsersListComponent },
  { path: 'student', redirectTo: '/student/tests', pathMatch: 'full' },
  { path: 'student/tests', component: AssignedTestsListComponent },
  { path: 'teacher', redirectTo: '/teacher/tests', pathMatch: 'full' },
  { path: 'teacher/tests', component: OwnedTestsListComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
