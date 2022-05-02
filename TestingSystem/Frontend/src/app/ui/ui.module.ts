import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {componentsList} from "./components-list";
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {ReactiveFormsModule} from "@angular/forms";
import {DropdownModule} from "primeng/dropdown";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

@NgModule({
  declarations: [
    ...componentsList
  ],
  imports: [
    CommonModule,
    InputTextModule,
    ButtonModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    DropdownModule
  ],
  exports: [
  ]
})
export class UiModule { }
