import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../../core/services/auth.service";
import {AccountService} from "../../../core/services/account.service";
import {UserRoles} from "../../../core/models/user-roles";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginFormGroup: FormGroup = new FormGroup({});
  public submitted: boolean = false;
  public errorMessage: string = "";

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.loginFormGroup = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  public fieldIsSet(field: string): boolean {
    return this.loginFormGroup.controls[field].errors?.required !== true;
  }

  public submitLogin() {
    this.submitted = true;
    if (!this.loginFormGroup.valid) {
      this.errorMessage = "Some required fields are not filled.";
      return;
    }

    const userName = this.loginFormGroup.controls['userName'].value;
    const password = this.loginFormGroup.controls['password'].value;
    this.authService.login(userName, password).subscribe(token => {
      this.authService.saveUserInfo(token, userName);

      this.accountService.userInRole(UserRoles.Student).subscribe(isStudent => {
        if (isStudent) {
          this.router.navigate(['/student']);
        }
      });

      this.accountService.userInRole(UserRoles.Teacher).subscribe(isTeacher => {
        if (isTeacher) {
          this.router.navigate(['/teacher']);
        }
      });

      this.accountService.userInRole(UserRoles.Admin).subscribe(isAdmin=> {
        if (isAdmin) {
          this.router.navigate(['/admin']);
        }
      });

      this.errorMessage = "User data is incorrect.";
    }, () => {
        this.errorMessage = "Failed to log in.";
      });
  }
}
