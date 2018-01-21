import { Injectable } from '@angular/core';

@Injectable()
export class ScreenService {

  VisibilityContentList: boolean = false;

  /**
   * コンテントプレビュー画面の表示フラグ
   */
  VisibilityContentPreview: boolean = false;

  ContentListParameter: ContentListParameter = {
    categoryId: 0
  };

  ContentPreviewParameter: ContentPreviewParameter = {
    contentId: 0
  };

  constructor() { }
}

export interface ContentListParameter {
  categoryId: Number;
}

export interface ContentPreviewParameter {
  contentId: Number;
}