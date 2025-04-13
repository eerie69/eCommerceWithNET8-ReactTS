import React, { useEffect } from 'react'
import { useCart } from '../../../Context/useCart';
import { Link } from 'react-router-dom';
import { MdArrowBack } from 'react-icons/md';
import Heading from '../../ProductCard/ProductCardDetails/Heading/Heading';
import Button from '../../ProductCard/ProductCardDetails/Button/Button';
import CartListItem from '../CartListItem/CartListItem';
import { formatPrice } from '../../../Helpers/FormatPrice';

type Props = {}

const CartList = (props: Props) => {
    const {cartProducts, cartTotalAmount} = useCart();

    if(!cartProducts || cartProducts.length === 0) {
        return (
            <div className='flex flex-col items-center'>
                <div className='text-2x1'>Your cart is empty</div>
                <div>
                    <Link to="/" className='text-slate-500 flex items-center gap-1 mt-2'>
                        <MdArrowBack />
                        <span>Start Shopping</span>
                    </Link>
                </div>
            </div>
        );
    }
    console.log(cartProducts?.length)
  return (
    <div>
        <Heading title='Shopping Cart' center />
        <div className='grid grid-cols-5 text-xs gap-4 pb-2 items-center mt-8'>
            <div className='col-span-2 justify-self-start'>Product</div>
            <div className='justify-self-center'>PRICE</div>
            <div className='justify-self-center'>QUANTITY</div>
            <div className='justify-self-end'>TOTAL</div>
        </div>
        <div>
            {cartProducts && cartProducts.map((item) => {
                return <CartListItem key={item.id} product={item} />
            })}
        </div>
        <div className='border-t-[1.5px] border-slate-200 py-4 flex justify-between gap-4'>
            <div className='w-[90px]'>
                <Button label='Clear Cart' onClick={() => {}} small outline/>
            </div>
            <div className='text-sm flex flex-col gap-1 items-start'>
                <div className='flex justify-between w-full text-base font-semibold'>
                    <span>Subtotal</span>
                    <span>{formatPrice(cartTotalAmount)}</span>
                </div>
                <p className='text-slate-500'>
                    Texes and Shopping calculate at checkout
                </p>
                <Button label='Checkout' onClick={() => {}} small outline/>
                <Link to="/" className='text-slate-500 flex items-center gap-1 mt-2'>
                    <MdArrowBack />
                    <span>Continue Shopping</span>
                </Link>
            </div>
        </div>
    </div>
  )
}

export default CartList