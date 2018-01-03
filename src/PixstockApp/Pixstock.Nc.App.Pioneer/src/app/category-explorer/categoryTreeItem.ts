import { Category } from 'pixstock.nc.app.core/dest/src/model/category';

export interface CategoryTreeItem {
    label:string;
    category?: Category;
    hasChildren: boolean;
    children: CategoryTreeItem[];
}