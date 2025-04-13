import axios from "axios";
import { StockGet, StockPut } from "../Models/Stock";
import { handleError } from "../Helpers/ErrorHandler";

const api = "http://localhost:5171/api/stock/";

export const stockPostAPI = async (
    categoryName: string,
) => {
    try {
        const data = await axios.post<StockPut>(api, {
            categoryName: categoryName,
        });
        return data;
      } catch (error) {
        handleError(error);
      }
};


export const stockGetAPI = async () => {
  try {
    const data = await axios.get<StockGet[]>(api);
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const stockGetBySlugAPI = async (Symbol: string | undefined) => {
    try {
      const data = await axios.get<StockGet[]>(api + `?sTerm=${Symbol}`);
      return data;
    } catch (error) {
      handleError(error);
    }
  };

export const stockGetByProductIdAPI = async (productId: string | undefined) => {
  try {
    const data = await axios.get<StockGet>(api + `${productId}`);
    return data;
  } catch (error) {
    handleError(error);
  }
};