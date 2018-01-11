import { Component, EventEmitter } from '@angular/core';
import { PixstockNetService } from 'pixstock.nc.app.core/dest/src/dao/pixstocknet.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  constructor(
    private _pixstock: PixstockNetService
  ) {
    _pixstock.echo.subscribe(prop => this.onEcho(prop));
  }

  onEcho(todo: string) {
    console.info("イベントから取得したメッセージ=" + todo);
  }
}
