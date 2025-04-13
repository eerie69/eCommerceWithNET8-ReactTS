import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { CartPost, CartTotalItems, UserCartGet } from "../Models/Cart";

const api = "http://localhost:5171/api/Cart/";

export const CartPostAPI = async (
    productId: number,
    qty: number,
    redirect: number,
) => {
    try {
        const data = await axios.post<CartPost>(api + `?productId=${productId}` + `&qty=${qty}` + `&redirect=${redirect}`);
        return data;
      } catch (error) {
        handleError(error);
      }
};


export const UserCartGetAPI = async () => {
  try {
    const data = await axios.get<UserCartGet>(api + `user-cart`);
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const CartTotalItemsAPI = async () => {
    try {
      const data = await axios.get<CartTotalItems>(api + `total-items`);
      return data;
    } catch (error) {
      handleError(error);
    }
  };

  export const ReduceOrRemoveItemAPI = async (id: number | undefined) => {
    try {
      const data = await axios.delete<CartTotalItems>(api + `reduceOrRemove-item?productId=${id}`);
      return data;
    } catch (error) {
      handleError(error);
    }
  };

  export const RemoveItemAPI = async (id: number | undefined) => {
    try {
      const data = await axios.delete<CartTotalItems>(api + `remove-item?productId=${id}`);
      return data;
    } catch (error) {
      handleError(error);
    }
  };

