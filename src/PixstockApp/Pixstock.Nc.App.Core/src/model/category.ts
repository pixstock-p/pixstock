import { Content } from './../model/content';

export interface Category {
    id: number;
    name: string;
    contents: Content[];
}