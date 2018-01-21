import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { PixstockNetService } from '../service/pixstock-net.service';

import { Content } from './../model/content';

import { WorkflowApi } from './../api/workflow-api';

@Injectable()
export class ContentDaoService {

  constructor(private _pixstock: PixstockNetService) { }

  /**
   * コンテントプレビュー情報を取得する
   * 
   * @param contentId 
   * @returns
   */
  getContentPreview(contentId): Observable<string> {
    console.info("[ContentDaoService][getContentPreview] IN");

    // 現時点では、情報といってもただのURL文字列を取得する

    return Observable.create(observer => {
      let result = this._pixstock.ipcRenderer.sendSync(WorkflowApi.EAV_GET_CONTENTPREVIEW, contentId);
      observer.next(result);
    });
  }
}
