import { Injectable } from '@angular/core';
import {FormArray, FormGroup} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class FormGroupService {

  constructor() { }

  public getFormGroup(formGroupRoot: FormGroup, formGroupPath: string): FormGroup {
    return formGroupRoot.get(formGroupPath) as FormGroup;
  }

  public getFormArray(formGroupRoot: FormGroup, formGroupPath: string): FormArray {
    return formGroupRoot.get(formGroupPath) as FormArray;
  }
}
