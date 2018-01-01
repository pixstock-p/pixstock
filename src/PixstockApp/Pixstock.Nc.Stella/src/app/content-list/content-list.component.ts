import { Component, OnInit } from '@angular/core';
import { Logger } from "angular2-logger/core";
import { Content } from 'pixstock.nc.app.core/dest/src/model/content';
import { CategoryDaoService } from 'pixstock.nc.app.core/dest/src/dao/categorydao.service';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit {

  contents: Content[];

  constructor(
    private _logger: Logger,
    private categoryDaoService: CategoryDaoService
  ) { }

  ngOnInit() {
    this.getContents(3);
  }

  private getContents(categoryId: Number): void {
    this._logger.debug("カテゴリ情報の読み込み=" + categoryId);
    this.categoryDaoService.getCategory(categoryId).then(category => {
        this._logger.debug("コンテンツ数=" + category.contents.length);
        this.contents = category.contents;
    });
  }
}
