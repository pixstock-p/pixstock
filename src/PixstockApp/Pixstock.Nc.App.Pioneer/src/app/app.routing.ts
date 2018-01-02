import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryExplorerComponent } from './category-explorer/category-explorer.component';

const appRoutes: Routes = [
    {
        path: 'category',
        component: CategoryExplorerComponent
    }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
