import React, { FC, useCallback, useEffect, useMemo, useState } from 'react'
import { ProductGet } from '../../../Models/Product';
import { Id } from 'react-toastify';
import { Link, useOutletContext } from 'react-router-dom';
import Button from './Button/Button';
import { useCart } from '../../../Context/useCart';
import Rating from '@mui/material/Rating';
import { number } from 'yup';
import SetQuantity from './SetQuantity/SetQuantity';
import { CartDetails, UserCartGet } from '../../../Models/Cart';
import { MdCheckCircle } from 'react-icons/md';
import { formatPrice } from '../../../Helpers/FormatPrice';

type Props = {
    product: ProductGet;
}

const Horizontal = () => {
    return <hr className='w-[30%] my-2' />
}

const ProductCardDetails = ({product}: Props) => {
    const {handleAddProductToCart, handleReduceOrRemoveItem, cartProducts, cartTotalQty} = useCart()
    const [isProductInCart, setIsProductInCart] = useState(false)
    const cartProduct = cartProducts?.find((item) => item.id === product.id);
    const [cartQty, setCartQty] = useState(cartProduct ? cartProduct.cartQty : 0);

    useEffect(() => {
        setCartQty(cartProduct ? cartProduct.cartQty : 0); // Обновляем `cartQty` при изменении `cartProducts`
        setIsProductInCart(false);

        if(cartProducts) {
            const existingIndex = cartProducts.findIndex((item) => item.id === product.id);
            if(existingIndex > -1) {
                setIsProductInCart(true)
            }
        }
        
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

    if (!product) {
        return <div>Loading...</div>;
    }

    const productRating = 
        product?.reviews?.length 
            ? product.reviews.reduce((acc: number, item: any) => 
                acc + item.rating, 0) / product.reviews.length : 0;
    
  return (
            <div key={product?.id} className='grid grid-cols-1 md:grid-cols-2 gap-12'>
                <div className='grid grid-cols-6 gap-2 h-full max-h-[500px] min-h-[300px] sm:min-h-[400px]'>
                    <div className='col-span-5 relative aspect-square'>
                        <img src={product?.image} alt='Product Image' className='h-full w-full object-contain max-h-[500px] min-h-[300px] sm:min-h-[400px]' />
                    </div>
                </div>
                <div className='flex flex-col gap-1 text-slate-500 text-sm'>
                    <h2 className='text-3x1 font-medium text-slate-700'>
                        {product?.title}
                    </h2>
                    <div className='flex items-center gap-2'>
                        <Rating value={productRating} readOnly /> 
                        <div>{product.reviews?.length ?? 0} reviews</div>
                    </div>
                    <Horizontal />
                    <div className='text-justify'>
                        {product?.description} 
                    </div>
                    <Horizontal />
                    <div className='flex gap-3 items-center'>
                        <span className='font-semibold'>
                            CATEGORY: 
                        </span>
                        <div className='flex gap-4 items-center text-base'>
                            {product?.categoryName}
                        </div> 
                    </div>
                    <div className={product?.quantity ? "text-teal-400" : "text-rose-400"}>
                        {product?.quantity ? 'In stock' : 'Out of stock'}
                    </div>
                    <Horizontal />
                    <div className='flex gap-6 items-center'>
                        <div className='font-semibold'>PRICE: </div> 
                        <div className='flex gap-4 items-center text-base'>
                            {formatPrice(product.price)}
                        </div>
                    </div>
                    <Horizontal />
                    {isProductInCart ? (
                        <>
                            <p className='mb-2 text-slate-500 flex items-center gap-1'>
                                <MdCheckCircle className='text-teal-400' size={20}/>
                                <span>Товар добавлен в корзину</span>
                            </p>
                            <SetQuantity
                                cartQty={cartQty}
                                handleQtyIncrease={handleQtyIncrease}
                                handleQtyDecrease={handleQtyDecrease} 
                            />
                            <Horizontal />
                            <Link to={`/userCart`} className='max-w-[300px]'>
                                <Button label='View Cart' outline onClick={() => {}}/>
                            </Link>
                        </>
                        ) : (
                        <>
                            <div className='max-w-[300px]'>
                                <Button label="Add To Cart" onClick={() => handleAddProductToCart(product)} />
                            </div>
                        </>
                    )}
                </div>
            </div>
  )
}

export default ProductCardDetails