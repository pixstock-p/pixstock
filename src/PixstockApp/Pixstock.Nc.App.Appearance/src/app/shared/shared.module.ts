import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PixstockNetService } from './service/pixstock-net.service';
import { ScreenService } from './service/screen.service';
import { CategoryDaoService } from './dao/category-dao.service';
import { ThumbnailDaoService } from './dao/thumbnail-dao.service';

@NgModule({
  imports: [
    CommonModule
  ],
  exports:[
    CommonModule,
  ],
  providers: [
    PixstockNetService,
    CategoryDaoService,
    ThumbnailDaoService,
    ScreenService
  ]
})
export class SharedModule { }
