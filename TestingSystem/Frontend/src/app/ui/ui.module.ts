import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {componentsList} from "./components-list";
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {DropdownModule} from "primeng/dropdown";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {TooltipModule} from "primeng/tooltip";
import {RouterModule} from "@angular/router";
import {TableModule} from "primeng/table";
import {FieldsetModule} from "primeng/fieldset";
import {MultiSelectModule} from "primeng/multiselect";
import {RadioButtonModule} from "primeng/radiobutton";
import {CheckboxModule} from "primeng/checkbox";
import { TakeTestComponent } from './components/student/take-test/take-test.component';
import { RadiobuttonQuestionComponent } from './components/student/radiobutton-question/radiobutton-question.component';
import { CheckboxQuestionComponent } from './components/student/checkbox-question/checkbox-question.component';
import { StringQuestionComponent } from './components/student/string-question/string-question.component';

@NgModule({
  declarations: [
    ...componentsList,
    TakeTestComponent,
    RadiobuttonQuestionComponent,
    CheckboxQuestionComponent,
    StringQuestionComponent,
  ],
  imports: [
    CommonModule,
    InputTextModule,
    ButtonModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    DropdownModule,
    TooltipModule,
    RouterModule,
    TableModule,
    FormsModule,
    FieldsetModule,
    MultiSelectModule,
    RadioButtonModule,
    CheckboxModule,
  ],
  exports: [
  ]
})
export class UiModule { }
