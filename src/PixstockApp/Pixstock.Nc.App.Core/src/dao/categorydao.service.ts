import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { Category } from './../model/category';
import { PixstockNetService } from './pixstocknet.service';

@Injectable()
export class CategoryDaoService {

    constructor(private _pixstock: PixstockNetService) { }

    /**
     * RequestCategoryAPIを呼び出す
     */
    getCategory(categoryId: Number): Observable<Category> {
        console.debug("[CategoryDaoService] getCategory(v2) = " + this._pixstock);

        return Observable.create(observer => {
            let result = this._pixstock.ipcRenderer.sendSync("EAV_GETCATEGORY", categoryId);
            observer.next(JSON.parse(result) as Category);
        });
    }

    /**
     * RequestCategory2APIを呼び出す
     */
    getSubCategory(categoryId: Number): Observable<Category[]> {
        console.debug("[CategoryDaoService] getSubCategory(v2) = " + this._pixstock);

        return Observable.create(observer => {
            let result = this._pixstock.ipcRenderer.sendSync("EAV_GETSUBCATEGORY", categoryId)
            observer.next(JSON.parse(result) as Category[]);
        });
    }
}