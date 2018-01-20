import { Component, OnInit, OnDestroy } from '@angular/core';
import { Logger } from "angular2-logger/core";
import { ActivatedRoute, Router, Params, NavigationStart, ActivationEnd } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

import { Content } from '../shared/model/content';
import { Thumbnail } from '../shared/model/thumbnail';
import { CategoryDaoService } from '../shared/dao/category-dao.service';
import { ThumbnailDaoService } from '../shared/dao/thumbnail-dao.service';
import { PixstockNetService } from '../shared/service/pixstock-net.service';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit, OnDestroy {

  /**
   * 
   */
  contents: ListItem[];

  parentUrl:string = "";

  private onShowContentListSubscription : Subscription;

    /**
   * コンストラクタ.
   * 
   * @param _logger 
   * @param categoryDaoService 
   * @param thumbnailDaoService 
   * @param router 
   * @param route 
   */
  constructor(
    private _logger: Logger,
    //private route: ActivatedRoute,
    private _pixstock: PixstockNetService,
    private categoryDaoService: CategoryDaoService,
    private thumbnailDaoService: ThumbnailDaoService
  ) {
    // iframeの親ウィンドウが、ASP.NETのHostingのため、
    // そのURLを取得する。
    var parent: any = window.parent; // JSのWindowオブジェクト
    this.parentUrl = parent.location;
  }


  ngOnInit() {
    // このコンポーネントでは、このイベントを受けないほうが良い。
    this.onShowContentListSubscription = this._pixstock.ShowContentList.subscribe(prop => this.onShowContentList(prop));

    this.contents = new Array<ListItem>();
  }

  ngOnDestroy() {
    this.onShowContentListSubscription.unsubscribe();
  }

  onShowContentList(categoryId:Number) {
    this._logger.debug("[ContentListComponent][onShowContentList] - IN");
    this._logger.info("カテゴリ(" + categoryId + ")を表示します。");

    this.contents = new Array<ListItem>();

    let l_categoryId = categoryId;
    this.getContents(l_categoryId);
  }

  /**
   * リストアイテムのコンテントをコンテントプレビュー画面で表示します
   * 
   * @param item 対象項目
   */
  showContentPreview(item:ListItem) {
    this._logger.debug("コンポーネントクラスでイベント受取 = " + item);
    
    // コンテントプレビュー画面に表示切り替え
    //this.router.navigate(['/preview'], { queryParams: { id: item.content.Id } });
  }

  private getContents(categoryId: Number): void {
    this.categoryDaoService.getCategory(categoryId).subscribe(category => {
      var l = new Array<ListItem>();
       category.Contents.forEach(element => {
         var item = new ListItem();
         item.content = element;
         if (item.content.ThumbnailKey != null) {
           //this.getThumbnail(item, item.content.ThumbnailKey);
         }
         l.push(item);
       });

      this.contents = l;
    });
  }

  private getThumbnail(item: ListItem, thumbnailHash: string): void {
    this.thumbnailDaoService.getThumbnail(thumbnailHash).subscribe(thumbanil => {
      item.thumbnail = thumbanil;
    });
  }
}

export class ListItem {
  content: Content;
  thumbnail: Thumbnail;
}