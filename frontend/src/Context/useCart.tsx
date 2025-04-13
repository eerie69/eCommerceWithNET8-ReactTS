import { createContext, useCallback, useContext, useEffect, useState } from "react";
import { ProductGet } from "../Models/Product";
import { CartPostAPI, CartTotalItemsAPI, ReduceOrRemoveItemAPI, RemoveItemAPI, UserCartGetAPI } from "../Services/CartService";
import { toast } from "react-toastify";

type CartContextType = {
    cartTotalQty: number;
    cartTotalAmount: number;
    cartProducts: ProductGet[] | null;
    handleAddProductToCart: (product: ProductGet) => void;
    handleReduceOrRemoveItem: (productId: number) => void;
    handleRemoveItem: (productId: number) => void;
}

export const CartContext = createContext<CartContextType | null>(null)

interface Props {
    [propName: string]: any;
}

export const CartContextProvider = (props: Props) => {
    const [cartTotalQty, setCartTotalQty] = useState(0);
    const [cartTotalAmount, setCartTotalAmount] = useState(0);
    const [cartProducts, setCartProducts] = useState<ProductGet[] | null>(null);
    

    //Получаем товары пользователя
    const fetchUserCart = useCallback(async () => {
        try {
            const response = await UserCartGetAPI();
            if (response?.data && response.data.cartDetails) {
                const userCart = response.data; 
                const products = userCart.cartDetails.map(detail => ({
                    ...detail.product, 
                    cartQty: detail.quantity
                }));
    
                setCartProducts(products);
            } else {
                setCartProducts([]);
            }
        } catch (error) {
            toast.error("Ошибка загрузки корзины! Попробуйте позже.");
        }
    }, []);

    // Получаем кол-во и сумму с сервера
    const fetchCartTotals = useCallback(async () => {
        try {
          const res = await CartTotalItemsAPI();
          if (res?.data) {
            setCartTotalQty(res.data.cartQty);
            setCartTotalAmount(res.data.cartTotal);
          }
        } catch (error) {
          toast.error("Ошибка получения суммы корзины");
        }
      }, []);

    // Добавить товар в корзину
    const handleAddProductToCart = useCallback(async (product: ProductGet) => {
        try {
            await CartPostAPI(product.id, 1, 0); 

            fetchUserCart();
            fetchCartTotals();
        } catch (error) {
            toast.error("Ошибка при добавлении в корзину!");
        }
    }, [fetchUserCart, fetchCartTotals]);

    // Уменьшить или удалить товар
    const handleReduceOrRemoveItem = useCallback(async (productId: number) => {
        try {
            await ReduceOrRemoveItemAPI(productId);

            fetchUserCart();
            fetchCartTotals();
        } catch (error) {
            toast.error("Ошибка при изменении количества товара");
        }
    }, [fetchUserCart, fetchCartTotals]);

    // Удалить товар
    const handleRemoveItem = useCallback(async (productId: number) => {
        try {
            await RemoveItemAPI(productId);

            fetchUserCart();
            fetchCartTotals();
        } catch (error) {
            toast.error("Ошибка при удалении товара");
        }
    }, [fetchUserCart, fetchCartTotals]);

    // Загружаем при первом рендере
    useEffect(() => {
        fetchUserCart();
        fetchCartTotals();
    }, [])

    const value = {
        cartTotalQty,
        cartTotalAmount,
        cartProducts,
        handleAddProductToCart,
        handleReduceOrRemoveItem,
        handleRemoveItem
    };

    return <CartContext.Provider value={value} {...props} />;
};

export const useCart = () =>{
    const context = useContext(CartContext);

    if (context === null) {
        throw new Error ("useCart must be used within a CartContextProvider");
    }

    return context;
};