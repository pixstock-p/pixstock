import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NgZone } from '@angular/core';
import { Logger, Options as LoggerOptions, Level as LoggerLevel } from "angular2-logger/core";
import { HttpModule } from '@angular/http';
import { TreeModule } from 'angular-tree-component';

import { AppComponent } from './app.component';
import { DaoModule } from 'pixstock.nc.app.core/dest/src';
import { PixstockNetService } from 'pixstock.nc.app.core/dest/src/dao/pixstocknet.service';
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
export class AppModule {
  constructor(
    private _logger: Logger,
    private _ngZone: NgZone,
    private _pixstock: PixstockNetService
  ) {
    var parent: any = window.parent;

    _logger.info("Pioneerアプリケーション初期化 v1");

    window['angularComponentRef'] = {
      component: this,
      zone: _ngZone
    };

    _pixstock.initialize(parent.getIpc()); // IPCオブジェクト取得
  }
}
