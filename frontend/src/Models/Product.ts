import { CommentGet } from "./Comment";

export type ProductPost = {
    symbol: string;
    description: string;
    price: number;
    image: string;
    featured: boolean;
    categoryList: string;
};
export type ProductGet = {
    id: number;
    title: string;
    description: string;
    price: number;
    image: string;
    featured: boolean;
    createBy: string;
    quantity: number;
    cartQty: number;
    categoryName: string;
    reviews: CommentGet[];
};
