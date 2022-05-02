import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {UsersListComponent} from "./ui/components";
import {AssignedTestsListComponent} from "./ui/components";
import {OwnedTestsListComponent} from "./ui/components";
import {LoginComponent} from "./ui/components/login/login.component";

export const appRoutes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
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
