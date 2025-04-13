import React from 'react'
import { ProductGet } from '../../../Models/Product';
import { truncateText } from '../../../Helpers/truncateText';
import { formatPrice } from '../../../Helpers/FormatPrice';
import { Link } from 'react-router-dom';
import { Rating } from '@mui/material';

type Props = {
    product: ProductGet;
  };

const ProductCardListItem = ({product}: Props) => {

  if (!product || !product.id) {
    return null;
  }
  
  return (
    <div className='col-span-1 cursor-pointer border-[1.2px] border-slate-200 
      bg-slate-50 rounded-sm p-2 transition hover:scale-105 text-center text-sm'>
        <Link to={`/product/${product.id}`}>
          <div key={product.id} className="flex flex-col items-center w-full gap-1">
                <div className="aspect-square overflow-hidden relative w-full">
                  <img
                    src={product.image}
                    alt="Product Image"
                    className="h-full w-full object-contain"
                  />
                </div>
                <div className="mt-4">
                  {truncateText(product.title)}
                </div>
                <div>
                    <Rating value={5} readOnly />
                </div>
                <div>{product.reviews?.length ?? 0} reviews</div>
                <div className='font-semibold'>
                  {formatPrice(product.price)}
                </div>
          </div>
        </Link>
    </div>
    
  );
}

export default ProductCardListItem