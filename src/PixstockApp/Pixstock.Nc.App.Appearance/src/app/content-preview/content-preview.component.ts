import { Component, OnInit } from '@angular/core';
import { Logger } from 'angular2-logger/core';
import { ContentDaoService } from '../shared/dao/content-dao.service';

@Component({
  selector: 'app-content-preview',
  templateUrl: './content-preview.component.html',
  styleUrls: ['./content-preview.component.scss']
})
export class ContentPreviewComponent implements OnInit {
  /**
   * [VM] プレビューデータを取得するURL
   */
  previewUrl: string;

  constructor(
    private _logger: Logger,
    private _contentDao: ContentDaoService
  ) { }

  ngOnInit() {
  }

  private updateContentPreviewAtRouting() {
    let contentId = 0;

    this._contentDao.getContentPreview(contentId).subscribe(prop => {
      this.previewUrl = prop;
    });
  }
}
