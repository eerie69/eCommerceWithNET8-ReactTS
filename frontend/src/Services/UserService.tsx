import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { EditUserProfile, UserGet } from "../Models/User";

const api = "http://localhost:5171/api/user/";

export const userPostAPI = async (
    categoryName: string,
) => {
    try {
        const data = await axios.post<EditUserProfile>(api, {
            categoryName: categoryName,
        });
        return data;
      } catch (error) {
        handleError(error);
      }
};


export const userGetAPI = async () => {
  try {
    const data = await axios.get<UserGet[]>(api);
    return data;
  } catch (error) {
    handleError(error);
  }
};


export const getUserByIdAPI = async (userId: string | undefined) => {
  try {
    const data = await axios.get<UserGet>(api + `${userId}`);
    return data;
  } catch (error) {
    handleError(error);
  }
};