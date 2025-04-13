import React from 'react'
import { ProductGet } from '../../../../Models/Product';

interface Props {
    cartCounter?: boolean;
    cartQty: number;
    handleQtyIncrease: () => void;
    handleQtyDecrease: () => void;
}

const btnStyles = 'border-[1.2px] border-slate-300 px-2 rounded';

const SetQuantity: React.FC<Props> = ({
    cartCounter,
    cartQty,
    handleQtyIncrease,
    handleQtyDecrease,
}) => {
  return (
    <div className='flex gap-8 items-center'>
        {cartCounter ? null : <div className='font-semibold'>QUANTITY:</div>}
        <div className='flex gap-4 items-center text-base'>
            <button onClick={handleQtyDecrease} className={btnStyles}>
              -
            </button>
            <div>{cartQty}</div>
            <button onClick={handleQtyIncrease} className={btnStyles}>
              +
            </button>
        </div>
    </div>
  )
}

export default SetQuantity