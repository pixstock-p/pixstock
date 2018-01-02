import { Component, OnInit } from '@angular/core';
import { Logger } from "angular2-logger/core";
import { Content } from 'pixstock.nc.app.core/dest/src/model/content';
import { CategoryDaoService } from 'pixstock.nc.app.core/dest/src/dao/categorydao.service';
import { ThumbnailDaoService } from 'pixstock.nc.app.core/dest/src/dao/thumbnaildao.service';

import { ListItem } from './listitem';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit {

  contents: ListItem[];

  constructor(
    private _logger: Logger,
    private categoryDaoService: CategoryDaoService,
    private thumbnailDaoService: ThumbnailDaoService
  ) { }

  ngOnInit() {
    this.getContents(3);
  }

  private getContents(categoryId: Number): void {
    this._logger.debug("カテゴリ情報の読み込み=" + categoryId);
    this.categoryDaoService.getCategory(categoryId).then(category => {
      this._logger.debug("コンテンツ数=" + category.contents.length);

      var l = new Array<ListItem>();
      category.contents.forEach(element => {
        var item = new ListItem();
        item.content = element;
        if (item.content.thumbnailKey != null) {
          this.getThumbnail(item, item.content.thumbnailKey);
        }
        l.push(item);
      });

      this.contents = l;
    });
  }

  private getThumbnail(item: ListItem, thumbnailHash: string): void {
    this._logger.debug("サムネイル情報の読み込み=" + thumbnailHash);
    this.thumbnailDaoService.getThumbnail(thumbnailHash).then(thumbanil => {
      item.thumbnail = thumbanil;
    });
  }
}
