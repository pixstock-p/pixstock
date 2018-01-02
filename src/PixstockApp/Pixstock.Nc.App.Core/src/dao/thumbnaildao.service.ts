import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Thumbnail } from './../model/thumbnail';

@Injectable()
export class ThumbnailDaoService {
    private controllerUrl = "/cli/Sample01";

    constructor(private http: Http) { }

    getThumbnail(thumbnailHash : string) : Promise<Thumbnail> {
        return this.http.get(this.controllerUrl + "/ThumbnailList",
            {
                params: {
                    ThumbnailHash: thumbnailHash
                }
            })
            .toPromise()
            .then(response => response.json() as Thumbnail)
    }
}