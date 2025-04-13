import axios from "axios"
import { handleError } from "../Helpers/ErrorHandler";
import { UserProfileToken } from "../Models/User";

const api = "http://localhost:5171/api/";


export const loginAPI = async (email: string, password: string) => {
    try{
        const data = await axios.post<UserProfileToken>(api + "account/login", {
            email: email,
            password: password,
        });
        return data;
    } catch (error) {
        handleError(error);
    }
};

export const registerAPI = async (
    email: string,
    username: string,
    password: string,
    confirmPassword: string,
) => {
    try {
        const data = await axios.post<UserProfileToken>(api + "account/register", {
            Email: email,
            UserName: username,
            Password: password,
            ConfirmPassword: confirmPassword,
        });
        return data;
    } catch (error) {
        handleError(error);
    }
}