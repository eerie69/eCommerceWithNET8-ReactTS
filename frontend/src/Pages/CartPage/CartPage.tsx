import { Container } from '@mui/material'
import React from 'react'
import CartList from '../../Components/Cart/CartList/CartList'

type Props = {}

const CartPage = (props: Props) => {
  return (
    <div className='pt-8'>
      <Container>
        <CartList />
      </Container>
    </div>
  )
}

export default CartPage