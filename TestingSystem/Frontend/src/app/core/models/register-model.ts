export class RegisterModel{
  constructor(
    public email: string,
    public userName: string,
    public password: string,
    public confirmPassword: string,
    public userRole: string
  ) {
  }
}
