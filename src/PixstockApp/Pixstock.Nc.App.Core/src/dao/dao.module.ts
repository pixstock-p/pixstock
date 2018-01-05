import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { ContentDaoService } from './contentdao.service';
import { CategoryDaoService } from './categorydao.service';
import { ThumbnailDaoService } from './thumbnaildao.service';

@NgModule({
    imports: [
        HttpModule,
    ],
    providers: [
        ContentDaoService,
        CategoryDaoService,
        ThumbnailDaoService,
    ]
})
export class DaoModule {}
