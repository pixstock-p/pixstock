import { Content } from './../model/content';

export interface Category {
    Id: number;
    Name: string;
    Contents: Content[];
}