import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {RegisterUserComponent, UsersListComponent} from "./ui/components";
import {AssignedTestsListComponent} from "./ui/components";
import {OwnedTestsListComponent} from "./ui/components";
import {LoginComponent} from "./ui/components";
import {AdminGuard} from "./core/guards/admin.guard";
import {StudentGuard} from "./core/guards/student.guard";
import {TeacherGuard} from "./core/guards/teacher.guard";

export const appRoutes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'admin', redirectTo: '/admin/users', pathMatch: 'full' },
  { path: 'admin/users', component: UsersListComponent, canActivate: [AdminGuard] },
  { path: 'admin/users/register', component: RegisterUserComponent, canActivate: [AdminGuard] },
  { path: 'student', redirectTo: '/student/tests', pathMatch: 'full' },
  { path: 'student/tests', component: AssignedTestsListComponent, canActivate: [StudentGuard] },
  { path: 'teacher', redirectTo: '/teacher/tests', pathMatch: 'full' },
  { path: 'teacher/tests', component: OwnedTestsListComponent, canActivate: [TeacherGuard] }
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
