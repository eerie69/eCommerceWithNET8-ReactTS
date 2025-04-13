import React from 'react'
import { CartContextProvider } from '../Context/useCart';

interface Props {
    children: React.ReactNode;
}

const CartProvider: React.FC<Props> = ({children}) => {
  return <CartContextProvider>{children}</CartContextProvider>
}

export default CartProvider