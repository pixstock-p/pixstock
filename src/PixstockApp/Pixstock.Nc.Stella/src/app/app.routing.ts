import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContentListComponent } from './content-list/content-list.component';
import { ContentPreviewComponent } from './content-preview/content-preview.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const appRoutes: Routes = [
    {
        path: 'contents',
        component: ContentListComponent
    },
    {
        path: 'dashboard',
        component: DashboardComponent
    },
    {
        path: 'preview',
        component: ContentPreviewComponent
    },
    {
        path: '',
        redirectTo: '/dashboard',
        pathMatch: 'full'
    },
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
