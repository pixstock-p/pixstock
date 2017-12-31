import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ContentListComponent } from './content-list/content-list.component';

const appRoutes: Routes = [
    {
        path: 'contents',
        component: ContentListComponent
    }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
