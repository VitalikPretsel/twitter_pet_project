import { NgModule } from '@angular/core';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs'

const modules = [
  MatCardModule,
  MatInputModule,
  MatButtonModule,
  MatTabsModule
];

@NgModule({
  exports: modules,
  imports: modules
})
export class MaterialModule { }
