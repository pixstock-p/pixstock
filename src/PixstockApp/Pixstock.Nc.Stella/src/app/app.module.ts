import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { Logger, Options as LoggerOptions, Level as LoggerLevel } from "angular2-logger/core";
import { AlertModule } from 'ngx-bootstrap';
import { ButtonsModule } from 'ngx-bootstrap';
import { HttpModule } from '@angular/http';
import { MyDateLibModule } from 'pixstock.nc.app.core/dest/src';
import { DaoModule } from 'pixstock.nc.app.core/dest/src';
import { PixstockNetService } from 'pixstock.nc.app.core/dest/src/dao/pixstocknet.service';

import { AppComponent } from './app.component';

import { routing } from './app.routing';
import { ContentListComponent } from './content-list/content-list.component';
import { ContentPreviewComponent } from './content-preview/content-preview.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    ContentListComponent,
    ContentPreviewComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    routing,
    AlertModule.forRoot(),
    ButtonsModule.forRoot(),
    MyDateLibModule,
    DaoModule
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
    private router: Router,
    private _ngZone: NgZone,
    private _pixstock: PixstockNetService
  ) {
    var parent: any = window.parent;

    _logger.info("Stellaアプリケーション初期化 v1");
    window['angularComponentRef'] = {
      component: this,
      componentFn_changeContentListByCategory: (value) => this.changeContentListByCategory(value),
      zone: _ngZone
    };

    _pixstock.initialize(parent.getIpc()); // IPCオブジェクト取得
  }

  /**
   * 外部公開API
   * @param categoryId カテゴリID
   */
  changeContentListByCategory(categoryId: Number) {
    this._logger.info("[Stella][changeContentListByCategory] : 外部公開APIの呼び出し categoryId=" + categoryId);
    this._ngZone.run(() => {
      this.router.navigate(['/contents'], { queryParams: { id: categoryId } }); // コンテントリスト表示（または、更新）
    });

  }
}
