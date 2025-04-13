import React, { useCallback, useEffect, useState } from 'react'
import { ProductGet } from '../../../Models/Product'
import { formatPrice } from '../../../Helpers/FormatPrice';
import { Link } from 'react-router-dom';
import { truncateText } from '../../../Helpers/truncateText';
import SetQuantity from '../../ProductCard/ProductCardDetails/SetQuantity/SetQuantity';
import { useCart } from '../../../Context/useCart';

interface Props {
  product: ProductGet;
}

const CartListItem: React.FC<Props> = ({product}) => {
    const {handleAddProductToCart, handleReduceOrRemoveItem, handleRemoveItem, cartProducts} = useCart()
    const cartProduct = cartProducts?.find((item) => item.id === product.id);
    const [cartQty, setCartQty] = useState(cartProduct ? cartProduct.cartQty : 0);
  
    useEffect(() => {
      setCartQty(
        cartProduct ? 
        cartProduct.cartQty : 0
      );
    }, [cartProducts, cartProduct]);
  
    const handleQtyIncrease = useCallback(() => {
      if (!product) return;
      setCartQty((prevQty) => prevQty + 1);
      handleAddProductToCart(product);
    }, [handleAddProductToCart, product]);
    
    const handleQtyDecrease = useCallback(() => {
      if (!product || cartQty <= 0) return;
      setCartQty((prevQty) => Math.max(prevQty - 1, 0));
      handleReduceOrRemoveItem(product.id);
    }, [handleReduceOrRemoveItem, product]);

    const handleRemoveProduct = useCallback(() => {
      if (!product) return;
      handleRemoveItem(product.id);
    }, [handleRemoveItem, product])
  
    if (!product) {
      return <div>Loading...</div>;
    }

   
   

  return (
    <div className='grid grid-cols-5 text-xs md:text-sm gap-4 border-t-[1.5px] border-slate-200 py-4 items-center'>
      <div className='col-span-2 justify-self-start flex gap-2 md:gap-4'>
        <Link to='/'>
          <div className='relative w-[70px] aspect-square'>
            <img src={product.image} alt={product.title} className='object-contain'/>
          </div>
        </Link>
        <div className='flex flex-col justify-between'>
          <Link to='/'>
            <div>{truncateText(product.title)}</div>
          </Link>
          <div className='w-[70px]'>
              <button className='text-slate-500 underline' onClick={handleRemoveProduct}>
                Remove
              </button>
            </div>
        </div>
      </div>
      <div className='justify-self-center'>
        {formatPrice(product.price)}
      </div>
      <div className='justify-self-center'>
        <SetQuantity 
          cartCounter={true} 
          cartQty={cartQty}
          handleQtyIncrease={handleQtyIncrease} 
          handleQtyDecrease={handleQtyDecrease} />
      </div>
      <div className='justify-self-end font-semibold'>
        {formatPrice(product.price * product.cartQty)}
      </div>
    </div>
  )
}

export default CartListItem