import { NgModule } from '@angular/core';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs'
import { MatMenuModule } from '@angular/material/menu';

const modules = [
  MatCardModule,
  MatInputModule,
  MatButtonModule,
  MatTabsModule,
  MatMenuModule
];

@NgModule({
  exports: modules,
  imports: modules
})
export class MaterialModule { }
