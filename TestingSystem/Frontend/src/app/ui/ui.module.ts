import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {componentsList} from "./components-list";
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {ReactiveFormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    ...componentsList
  ],
  imports: [
    CommonModule,
    InputTextModule,
    ButtonModule,
    ReactiveFormsModule
  ],
  exports: [
  ]
})
export class UiModule { }
