import { Injectable, EventEmitter, NgZone } from '@angular/core';
import { Logger } from "angular2-logger/core";

@Injectable()
export class PixstockNetService {

  /**
   * ElectronNETのRendererで使用するIPCオブジェクト
   */
  ipcRenderer: any;

  _logger: Logger;

  // Event

  echo: EventEmitter<string> = new EventEmitter();
  ShowContentPreview: EventEmitter<string> = new EventEmitter();
  ShowContentList: EventEmitter<string> = new EventEmitter();

  private onMSG_SHOW_CONTENTPREVIEW(event, args) {
    this.ShowContentPreview.emit(args);
  }

  private onMSG_SHOW_CONTENLIST(event, args) {
    this._logger.debug("Execute onMSG_SHOW_CONTENLIST");
    this.ShowContentList.emit(args);
  }

  /**
   * サービスの初期化
   * 
   * @param _ipcRenderer IPCオブジェクト
   * @param isRpcInitialize IPCオブジェクトのイベントハンドラ登録を行うかどうかのフラグ
   */
  initialize(_ipcRenderer: any, isRpcInitialize: boolean, _logger: Logger) {
    this.ipcRenderer = _ipcRenderer;
    this._logger = _logger;

    if (!window['angularComponentRef_PixstockNetService']) {
      window['angularComponentRef_PixstockNetService'] = {
        // NOTE: IPCイベントをすべて登録する
        componentFn_MSG_SHOW_CONTENTPREVIEW: (event, arg) => this.onMSG_SHOW_CONTENTPREVIEW(event, arg),
        componentFn_MSG_SHOW_CONTENLIST: (event, arg) => this.onMSG_SHOW_CONTENLIST(event, arg)
      };
    }
    
    if (isRpcInitialize) {
      _logger.info("IPCイベントの初期化");

      this.ipcRenderer.removeAllListeners(["MSG_SHOW_CONTENTPREVIEW", "MSG_SHOW_CONTENLIST"]);

      this.ipcRenderer.on('MSG_SHOW_CONTENTPREVIEW', (event, arg) => {
        var ntv_window: any = window;
        ntv_window.angularComponentRef.zone.run(() => {
          ntv_window.angularComponentRef_PixstockNetService.componentFn_MSG_SHOW_CONTENTPREVIEW(event, arg);
        });
      });

      this.ipcRenderer.on('MSG_SHOW_CONTENLIST', (event, arg) => {
        var ntv_window: any = window;
        ntv_window.angularComponentRef.zone.run(() => {
          ntv_window.angularComponentRef_PixstockNetService.componentFn_MSG_SHOW_CONTENLIST(event, arg);
        });
      });
    }
  }

  /**
   * [インターフェース] ...
   * 
   * @param screenId 
   */
  ShowScreenContentList(categoryId: Number) {
    this._logger.info("Execute EVT_TRNS_CONTENTLISTイベント送信");
    this.ipcRenderer.send("EVT_TRNS_CONTENTLIST", categoryId);
  }

}
