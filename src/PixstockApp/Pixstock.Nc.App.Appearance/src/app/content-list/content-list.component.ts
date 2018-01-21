import { Component, OnInit, OnDestroy, Input } from '@angular/core';
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

  parentUrl: string = "";

  /**
   * 読み込むコンテントの検索条件に使用するカテゴリを設定します.
   * 
   * 変更通知を受けるため、Input属性のプロパティとして公開する。
   * 
   * @param value
   */
  @Input() set categoryId(value: Number) {
    this._logger.debug("[ContentListComponent][set categoryId] - IN");
    this._categoryId = value;
    if (this._isInitialized) this.getContents(this._categoryId);
    this._logger.debug("[ContentListComponent][set categoryId] - OUT");
  }

  /**
   * 読み込むコンテントの検索条件に使用するカテゴリを取得します.
   * 
   * @returns
   */
  get categoryId(): Number {
    return this._categoryId;
  }

  private _isInitialized: boolean = false;
  private _categoryId: Number;

  /**
   * コンストラクタ.
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
    this.contents = new Array<ListItem>();

    this.getContents(this.categoryId);

    this._isInitialized = true;
  }

  ngOnDestroy() {
    this._isInitialized = false;
  }

  /**
   * リストアイテムのコンテントをコンテントプレビュー画面で表示します
   * 
   * @param item 対象項目
   */
  showContentPreview(item: ListItem) {
    this._logger.debug("[ContentListComponent][showContentPreview] - IN コンポーネントクラスでイベント受取 = " + item);

    // コンテントプレビュー画面に表示切り替え
    //this.router.navigate(['/preview'], { queryParams: { id: item.content.Id } });
    this._pixstock.ShowScreenContentPreview(item.content.Id);
  }

  private getContents(categoryId: Number): void {
    this.categoryDaoService.getCategory(categoryId).subscribe(category => {
      var l = new Array<ListItem>();
      category.Contents.forEach(element => {
        var item = new ListItem();
        item.content = element;
        if (item.content.ThumbnailKey != null) {
          this.getThumbnail(item, item.content.ThumbnailKey);
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