import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { Thumbnail } from './../model/thumbnail';
import { PixstockNetService } from './pixstocknet.service';

@Injectable()
export class ThumbnailDaoService {
    constructor(private _pixstock: PixstockNetService) { }

    getThumbnail(thumbnailHash : string) : Observable<Thumbnail> {
        console.debug("[ThumbnailDaoService] getThumbnail(v2) = " + this._pixstock);

        return Observable.create(observer => {
            let result = this._pixstock.ipcRenderer.sendSync("EAV_GETTHUMBNAIL", thumbnailHash);
            observer.next(JSON.parse(result) as Thumbnail);
        });
    }
}