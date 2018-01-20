import { Injectable } from '@angular/core';

@Injectable()
export class ScreenService {

  VisibilityContentList:boolean = false;

  ContentListParameter:ContentListParameter = {
    categoryId: 0
  };

  constructor() { }
}

export interface ContentListParameter {
  categoryId: Number;
}