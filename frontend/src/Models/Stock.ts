export type StockPut = {
    productId: number;
    quantity: number;
  };
  
export type StockGet = {
    id: number;
    productId: number;
    quantity: number;
    productName: string;
  };