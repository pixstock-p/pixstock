import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { ContentDaoService } from './../dao/contentdao.service';
import { CategoryDaoService } from './categorydao.service';

@NgModule({
    imports: [
        HttpModule,
    ],
    providers: [
        ContentDaoService,
        CategoryDaoService
    ]
})
export class DaoModule {}
