import React from 'react'
import ProductCardListItem from '../ProductCardListItem/ProductCardListItem'
import { ProductGet } from '../../../Models/Product';
import { Link } from 'react-router-dom';

type Props = {
    products: ProductGet[];
}

const ProductCardList = ({products}: Props) => {

  if (!products || products.length === 0) {
    return <div>No products found</div>;
  }

  return (
    <div className='grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 x1:grid-cols-5 2x1:grid-cols-6 gap-8'>
      {products
          ? products.map((product) => {
              return <ProductCardListItem key={product.id} product={product}  />  ;
            })
          : ""}
    </div>
  )
}

export default ProductCardList