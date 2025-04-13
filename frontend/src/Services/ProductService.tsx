import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { ProductGet, ProductPost } from "../Models/Product";




const api = "http://localhost:5171/api/Product/";

export const productPostAPI = async (
    symbol: string,
    description: string,
    price: number,
    image: string,
    featured: boolean,
    categoryList: string,
) => {
    try {
        const data = await axios.post<ProductPost>(api + `?symbol=${symbol}`, {
            description: description,
            price: price,
            image: image,
            featured: featured,
            categoryList: categoryList,
        });
        return data;
      } catch (error) {
        handleError(error);
      }
};


export const productGetAPI = async () => {
  try {
    const data = await axios.get<ProductGet[]>(api);
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const productGetByIdAPI = async (id: string | undefined) => {
  try {
    const data = await axios.get<ProductGet>(api + `${id}`,);
    return data;
  } catch (error) {
    handleError(error);
  }
};