import React from 'react'
import { useCart } from '../../../Context/useCart';
import {CiShoppingCart} from 'react-icons/ci'
import { Link } from 'react-router-dom';

type Props = {}

const CartCount = (props: Props) => {
  const {cartTotalQty} = useCart();

  return (
    <Link to={`/userCart`} className='relative cursor-pointer'>
        <div className='text-3x1'>
            <CiShoppingCart size='2rem' />
        </div>
        <span className='absolute top-[-10px] right-[-10px] bg-slate-700 text-white h-6 w-6 rounded-full flex items-center justify-center text-sm'>
            {cartTotalQty}
        </span>
    </Link>
  );
};

export default CartCount;