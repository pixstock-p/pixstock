import { Component } from '@angular/core';
import { Logger } from "angular2-logger/core";

import { PixstockNetService } from './shared/service/pixstock-net.service';
import { ScreenService } from './shared/service/screen.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  constructor(
    private _logger: Logger,
    private _pixstock: PixstockNetService,
    private _screen: ScreenService
  ) {
    _pixstock.echo.subscribe(prop => this.onEcho(prop));
    _pixstock.ShowContentPreview.subscribe(prop => this.OnShowContentPreview(prop));
    _pixstock.ShowContentList.subscribe(prop => this.OnShowContentList(prop));
  }

  onEcho(todo: string) {
    console.info("イベントから取得したメッセージ=" + todo);
  }

  OnShowContentPreview(args: Number) {
    this._logger.info("[Stella][AppComponent][OnShowContentPreview] : イベントから取得したメッセージ=" + args);
    this._screen.ContentPreviewParameter.contentId = args;

    this._screen.VisibilityContentList = false;
    this._screen.VisibilityContentPreview = true;
  }

  OnShowContentList(args: Number) {
    this._logger.info("[Stella][AppComponent][OnShowContentList] : イベントから取得したメッセージ=" + args);

    this._screen.ContentListParameter.categoryId = args;

    this._screen.VisibilityContentPreview = false;
    this._screen.VisibilityContentList = true;
  }
}
