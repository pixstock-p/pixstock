import { Injectable, EventEmitter, NgZone } from '@angular/core';

@Injectable()
export class PixstockNetService {
    ipcRenderer: any;

    echo: EventEmitter<string> = new EventEmitter();
    ShowContentPreview: EventEmitter<string> = new EventEmitter();

    on_asynchronous_reply(event, arg) {
        this.echo.emit(arg);
    }

    private onMSG_SHOW_CONTENTPREVIEW(event, args) {
        this.ShowContentPreview.emit(args);
    }

    initialize(_ipcRenderer: any) {
        console.debug("[PixstockNetService::ngOnInit] コンポーネント初期化");
        this.ipcRenderer = _ipcRenderer;

        if (!window['angularComponentRef_PixstockNetService']) {
            window['angularComponentRef_PixstockNetService'] = {
                componentFn_pixstocknet: (event, arg) => this.on_asynchronous_reply(event, arg),
                componentFn_MSG_SHOW_CONTENTPREVIEW: (event, arg) => this.onMSG_SHOW_CONTENTPREVIEW(event, arg)
            };
        }

        this.ipcRenderer.on('asynchronous-reply', (event, arg) => {
            var ntv_window: any = window;
            ntv_window.angularComponentRef.zone.run(() => {
                ntv_window.angularComponentRef_PixstockNetService.componentFn_pixstocknet(event, arg);
            });
        });

        this.ipcRenderer.on('MSG_SHOW_CONTENTPREVIEW', (event, arg) => {
            var ntv_window: any = window;
            ntv_window.angularComponentRef.zone.run(() => {
                ntv_window.angularComponentRef_PixstockNetService.componentFn_MSG_SHOW_CONTENTPREVIEW(event, arg);
            });
        });
    }
}