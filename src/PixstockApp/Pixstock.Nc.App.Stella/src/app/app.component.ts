import { Component } from '@angular/core';
import { Logger } from "angular2-logger/core";
import { Router } from '@angular/router';

import { PixstockNetService } from 'pixstock.nc.app.core/dest/src/dao/pixstocknet.service';
import { ContentDaoService } from 'pixstock.nc.app.core/dest/src/dao/contentdao.service';
import { CategoryDaoService } from 'pixstock.nc.app.core/dest/src/dao/categorydao.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  constructor(
    private _logger: Logger,
    private router: Router,
    //private contentDaoService: ContentDaoService,
    private categoryDaoService: CategoryDaoService,
    private _pixstock: PixstockNetService
  ) {
    _pixstock.echo.subscribe(prop => this.addTodo(prop));
    _pixstock.ShowContentPreview.subscribe(prop => this.OnShowContentPreview(prop));
  }

  addTodo(todo: string) {
    this._logger.info("[Stella][AppComponent][addTodo] : イベントから取得したメッセージ=" + todo);
  }

  OnShowContentPreview(args: string) {
    this._logger.info("[Stella][AppComponent][OnShowContentPreview] : イベントから取得したメッセージ=" + args);

    this.router.navigate(['/dashboard'], { queryParams: { id: args } });
  }

  create() {
    alert("create!21");
    this._logger.error('This is a priority level 1 error message...');
    this._logger.warn('This is a priority level 2 warning message...');
    this._logger.info('This is a priority level 3 warning message...');
    this._logger.debug('This is a priority level 4 debug message...');
    this._logger.log('This is a priority level 5 log message...');
  }

}