import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Logger, Options as LoggerOptions, Level as LoggerLevel } from "angular2-logger/core";
import { HttpModule } from '@angular/http';
import { TreeModule } from 'angular-tree-component';

import { AppComponent } from './app.component';
import { DaoModule } from 'pixstock.nc.app.core/dest/src';
import { CategoryExplorerComponent } from './category-explorer/category-explorer.component';
import { routing } from './app.routing';

@NgModule({
  declarations: [
    AppComponent,
    CategoryExplorerComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    TreeModule,
    DaoModule,
    routing
  ],
  providers: [
    { provide: LoggerOptions, useValue: { level: LoggerLevel.DEBUG } },
    Logger,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
