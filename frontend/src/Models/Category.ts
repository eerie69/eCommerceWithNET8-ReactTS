import { ProductGet } from "./Product";

export type CategoryPost = {
    categoryName: string;
  };
  
export type CategoryGet = {
    id: number;
    categoryName: string;
    products: ProductGet[]; 
  };