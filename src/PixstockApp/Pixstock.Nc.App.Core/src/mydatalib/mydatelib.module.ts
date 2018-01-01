import { NgModule } from '@angular/core';
import { DateTimeComponent } from './datetime.component';

@NgModule({
  declarations: [
    DateTimeComponent
  ],
  exports: [
    DateTimeComponent
  ]
})
export class MyDateLibModule {}