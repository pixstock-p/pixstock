import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { Category } from './../model/category';

@Injectable()
export class CategoryDaoService {

    private controllerUrl = "/cli/Sample01";

    constructor(private http: Http) { }

    /**
     * RequestCategoryAPIを呼び出す
     */
    getCategory(categoryId : Number): Promise<Category> {
        return this.http.get(this.controllerUrl + "/RequestCategory1",
            {
                params: {
                    CategoryId: categoryId
                }
            })
            .toPromise()
            .then(response => response.json() as Category)
    }
}