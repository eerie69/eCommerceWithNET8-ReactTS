import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import LoginPage from "../Pages/LoginPage/LoginPage";
import HomePage from "../Pages/HomePage/HomePage";
import RegisterPage from "../Pages/RegisterPage/RegisterPage";
import ProductPage from "../Pages/ProductPage/ProductPage";
import ProtectedRoute from "./ProtectedRoute";
import CartPage from "../Pages/CartPage/CartPage";



export const router = createBrowserRouter([
    {
      path: "/",
      element: <App />,
      children: [
        { path: "", element: <HomePage /> },
        { path: "login", element: <LoginPage /> },
        { path: "register", element: <RegisterPage /> },
        { path: "userCart", element: <CartPage /> },
        { path: "product/:id", element: ( 
          <ProtectedRoute>
          <ProductPage />
          </ProtectedRoute>
        ) 
       },
      ],
    },
  ]);