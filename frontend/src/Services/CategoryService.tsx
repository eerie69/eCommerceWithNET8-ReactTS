import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { CategoryGet, CategoryPost } from "../Models/Category";

const api = "http://localhost:5171/api/Category/";

export const categoryPostAPI = async (
    categoryName: string,
) => {
    try {
        const data = await axios.post<CategoryPost>(api, {
            categoryName: categoryName,
        });
        return data;
      } catch (error) {
        handleError(error);
      }
};


export const categoryGetAPI = async () => {
  try {
    const data = await axios.get<CategoryGet[]>(api);
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const categoryGetByIdAPI = async (id: string | undefined) => {
  try {
    const data = await axios.get<CategoryGet>(api + `${id}`,);
    return data;
  } catch (error) {
    handleError(error);
  }
};