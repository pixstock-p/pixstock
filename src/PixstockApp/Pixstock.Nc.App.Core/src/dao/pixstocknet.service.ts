import { Injectable, EventEmitter, NgZone } from '@angular/core';

@Injectable()
export class PixstockNetService {
    ipcRenderer: any;

    submit: EventEmitter<string> = new EventEmitter();

    on_asynchronous_reply(event, arg) {
        this.submit.emit(arg);
    }

    initialize(_ipcRenderer: any) {
        console.debug("[PixstockNetService::ngOnInit] コンポーネント初期化");
        this.ipcRenderer = _ipcRenderer;

        if (!window['angularComponentRef_PixstockNetService']) {
            window['angularComponentRef_PixstockNetService'] = {
                componentFn_pixstocknet: (event, arg) => this.on_asynchronous_reply(event, arg)
            };
        }

        this.ipcRenderer.on('asynchronous-reply', (event, arg) => {
            var ntv_window: any = window;
            ntv_window.angularComponentRef.zone.run(() => {
                ntv_window.angularComponentRef_PixstockNetService.componentFn_pixstocknet(event, arg);
            });
        });
    }
}