import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AlertModule } from 'ngx-bootstrap';
import { ButtonsModule } from 'ngx-bootstrap';
import { HttpModule } from '@angular/http';
import { MyDateLibModule } from 'pixstock.nc.app.core/dest/src';

import { AppComponent } from './app.component';

import { routing } from './app.routing';
import { ContentListComponent } from './content-list/content-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ContentListComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    routing,
    AlertModule.forRoot(),
    ButtonsModule.forRoot(),
    MyDateLibModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
