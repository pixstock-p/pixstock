import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { ContentDaoService } from './contentdao.service';
import { CategoryDaoService } from './categorydao.service';
import { ThumbnailDaoService } from './thumbnaildao.service';
import { PixstockNetService } from './pixstocknet.service';

@NgModule({
    imports: [
        HttpModule,
    ],
    providers: [
        ContentDaoService,
        CategoryDaoService,
        ThumbnailDaoService,
        PixstockNetService,
    ]
})
export class DaoModule {}
