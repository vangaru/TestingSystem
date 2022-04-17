import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {componentsList} from "./components-list";

@NgModule({
  declarations: [
    ...componentsList
  ],
  imports: [
    CommonModule
  ],
  exports: [
  ]
})
export class UiModule { }
