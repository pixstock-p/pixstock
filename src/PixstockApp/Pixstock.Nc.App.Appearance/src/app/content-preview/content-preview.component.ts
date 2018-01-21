import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Logger } from 'angular2-logger/core';
import { ContentDaoService } from '../shared/dao/content-dao.service';

@Component({
  selector: 'app-content-preview',
  templateUrl: './content-preview.component.html',
  styleUrls: ['./content-preview.component.scss']
})
export class ContentPreviewComponent implements OnInit, OnDestroy {
  /**
   * [VM] プレビューデータを取得するURL
   */
  previewUrl: string;

  /**
   * 表示コンテント情報を設定します.
   * 
   * @param value
   */
  @Input() set contentId(value: Number) {
    this._logger.debug("[ContentPreviewComponent][set contentId] - IN");
    this._contentId = value;
    if (this._isInitialized) this.updateContentPreviewAtRouting();
    this._logger.debug("[ContentPreviewComponent][set contentId] - OUT");
  }

  get contentId(): Number {
    return this._contentId;
  }

  private _isInitialized: boolean = false;
  private _contentId: Number;
  private _parentUrl: string = "";

  /**
   * 
   * @param _logger 
   * @param _contentDao 
   */
  constructor(
    private _logger: Logger,
    private _contentDao: ContentDaoService
  ) { 
    // iframeの親ウィンドウが、ASP.NETのHostingのため、
    // そのURLを取得する。
    var parent: any = window.parent; // JSのWindowオブジェクト
    this._parentUrl = parent.location;
  }

  ngOnInit() {
    this._isInitialized = true;

    this.updateContentPreviewAtRouting();
  }

  ngOnDestroy() {
    this._isInitialized = false;
  }

  private updateContentPreviewAtRouting() {
    this._contentDao.getContentPreview(this.contentId).subscribe(prop => {
      this.previewUrl = this._parentUrl + prop;
    });
  }
}
