import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NgZone } from '@angular/core';
import { Logger, Options as LoggerOptions, Level as LoggerLevel } from "angular2-logger/core";

import { TreeModule } from 'angular-tree-component';

// ngx-bootstrap
import { AlertModule } from 'ngx-bootstrap';
import { ButtonsModule } from 'ngx-bootstrap';
import { CarouselModule } from 'ngx-bootstrap/carousel';

// Pixstock
import { environment } from '../environments/environment';

import { SharedModule } from './shared/shared.module';
import { PixstockNetService } from './shared/service/pixstock-net.service';

import { AppComponent } from './app.component';
import { CategoryExplorerComponent } from './category-explorer/category-explorer.component';
import { ContentListComponent } from './content-list/content-list.component';
import { ContentPreviewComponent } from './content-preview/content-preview.component';


@NgModule({
  declarations: [
    AppComponent,
    CategoryExplorerComponent,
    ContentListComponent,
    ContentPreviewComponent
  ],
  imports: [
    BrowserModule,
    TreeModule,
    SharedModule,
    AlertModule.forRoot(),
    ButtonsModule.forRoot(),
    CarouselModule.forRoot(),
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
    _logger.info("アプリケーションの初期化 v0.0.1#4");

    window['angularComponentRef'] = {
      component: this,
      zone: _ngZone
    };

    var parent: any = window.parent; // JSのWindowオブジェクト
    _logger.info("ParentLocation = " + parent.location);

    let flag = parent.getFirstLoad();
    if (flag == false) {
      _logger.info("AApp初期読み込み判定");
      parent.setFirstLoad();
      _pixstock.initialize(parent.getIpc(),true, this._logger); // IPCオブジェクト取得
    } else {
      _logger.info("AApp初期化済み判定");
      _pixstock.initialize(parent.getIpc(),false, this._logger); // IPCオブジェクト取得
    }
  }
}
