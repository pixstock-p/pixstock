import { Component, OnInit, OnDestroy } from '@angular/core';
import { Logger } from "angular2-logger/core";
import { ActivatedRoute, Router, Params, NavigationStart, ActivationEnd } from '@angular/router';
import { Content } from 'pixstock.nc.app.core/dest/src/model/content';
import { CategoryDaoService } from 'pixstock.nc.app.core/dest/src/dao/categorydao.service';
import { ThumbnailDaoService } from 'pixstock.nc.app.core/dest/src/dao/thumbnaildao.service';
import { Observable } from 'rxjs/Observable';

import { ListItem } from './listitem';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit, OnDestroy {

  /**
   * ルーティングイベントの購読
   */
  routeEvent: Subscription;

  /**
   * 
   */
  contents: ListItem[];

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
    private categoryDaoService: CategoryDaoService,
    private thumbnailDaoService: ThumbnailDaoService,
    private router: Router,
    private route: ActivatedRoute,
  ) {
  }

  ngOnDestroy(): void {
    this._logger.debug("[ContentListComponent::ngOnDestroy] コンポーネント削除");
    this.routeEvent.unsubscribe(); // 購読中のルーティングイベントを破棄する
  }

  ngOnInit() {
    this._logger.debug("[ContentListComponent::ngOnInit] コンポーネント初期化");

    // ルーティングイベントを購読し、同一ルーティング時のパラメータ変化を取得する
    this.routeEvent = this.router.events.subscribe(event => {
      if (event instanceof ActivationEnd) {
        let activationEnd = event as ActivationEnd;
        if (activationEnd.snapshot.component['name'] === this.constructor.name) {
          this.updateContentListAtRouting();
        }
      }
    });

    this.updateContentListAtRouting(); // コンポーネントのインスタンス化時にコンテントリストの読み込みを行う
  }

  private updateContentListAtRouting() {
    // ルーティング情報からパラメータを取得し、コンテントリストを更新する。
    this._logger.debug("[updateContentListAtRouting] IN");
    let categoryId = 1;

    this.route.queryParams.forEach((params: Params) => {
      if (params['id'] != null) {
        categoryId = params['id'];
      }
    });

    this.getContents(categoryId);

    this._logger.debug("[updateContentListAtRouting] OUT");
  }

  /**
   * リストアイテムのコンテントをコンテントプレビュー画面で表示します
   * 
   * @param item 対象項目
   */
  showContentPreview(item:ListItem) {
    this._logger.debug("コンポーネントクラスでイベント受取 = " + item);
    
    // コンテントプレビュー画面に表示切り替え
    this.router.navigate(['/preview'], { queryParams: { id: item.content.Id } });
  }

  private getContents(categoryId: Number): void {
    this._logger.debug("カテゴリ情報の読み込み=" + categoryId);
    this.categoryDaoService.getCategory(categoryId).subscribe(category => {
      this._logger.debug("コンテンツ数=" + category.Contents.length);

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
    this._logger.debug("[Stella][ContentListComponent][getThumbnail] : サムネイル取得");
    this.thumbnailDaoService.getThumbnail(thumbnailHash).subscribe(thumbanil => {
      item.thumbnail = thumbanil;
    });
  }
}
