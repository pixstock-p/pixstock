import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { PixstockNetService } from '../service/pixstock-net.service';

import { Thumbnail } from './../model/thumbnail';

@Injectable()
export class ThumbnailDaoService {

  constructor(private _pixstock: PixstockNetService) { }

  getThumbnail(thumbnailHash : string) : Observable<Thumbnail> {
    return Observable.create(observer => {
        let result = this._pixstock.ipcRenderer.sendSync("EAV_GETTHUMBNAIL", thumbnailHash);
        observer.next(JSON.parse(result) as Thumbnail);
    });
}
}
