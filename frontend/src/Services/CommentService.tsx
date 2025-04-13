import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { CommentGet, CommentPost } from "../Models/Comment";


const api = "http://localhost:5171/Reviews/";

export const commentPostAPI = async (
    rating: number,
    content: string,
    symbol: string | undefined
  ) => {
    try {
      const data = await axios.post<CommentPost>(api + `${symbol}`, {
        rating: rating,
        content: content,
      });
      return data;
    } catch (error) {
      handleError(error);
    }
  };
  
  export const commentGetAPI = async (symbol: string | undefined) => {
    try {
      const data = await axios.get<CommentGet[]>(api + `?Title=${symbol}`);
      return data;
    } catch (error) {
      handleError(error);
    }
  };