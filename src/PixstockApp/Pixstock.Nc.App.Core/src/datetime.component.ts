import { Component } from '@angular/core';

@Component({
  selector: 'datetime',
  template: '<p>{{date}}</p>'
})
export class DateTimeComponent {
  date: string = new Date().toString();
}