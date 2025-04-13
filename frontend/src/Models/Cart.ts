import { ProductGet } from "./Product";

export type CartPost = {
    qty: number;
    redirect: number;
  };
  
export type UserCartGet = {
    id: number;
    userId: string;
    isDeleted: boolean;
    cartTotal: number;
    cartDetails: CartDetails[];
  };

export type CartDetails = {
    id: number;
    shoppingCartId: number;
    quantity: number
    unitPrice: number;
    product: ProductGet;
  };

export type CartTotalItems = {
    cartQty: number;
    cartTotal: number;
  };


