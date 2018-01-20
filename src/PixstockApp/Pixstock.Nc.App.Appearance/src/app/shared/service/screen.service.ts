import { Injectable } from '@angular/core';

@Injectable()
export class ScreenService {

  VisibilityContentList:boolean = false;

  ContentListParameter:ContentListParameter = new ContentListParameter();

  constructor() { }
}

export class ContentListParameter {
  categoryId: Number;
}