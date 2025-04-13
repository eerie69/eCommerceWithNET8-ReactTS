import React from 'react'
import { Link } from 'react-router-dom'
import FooterList from './FooterList/FooterList'
import Container from '../Container/Container'
import {MdFace, MdFacebook} from 'react-icons/md'
import {AiFillInstagram, AiFillTwitterCircle, AiFillYoutube} from 'react-icons/ai'

type Props = {}

const Footer = (props: Props) => {
  return (
    <footer className='bg-slate-700 text-slate-200 text-sm mt-16'>
      <Container>
        <div className='flex flex-col md:flex-row justify-between pt-16 pb-8'>
          <FooterList>
            <h3 className='text-base font-bold'>Shop Categories</h3>
            <Link to="/">Phones</Link>
            <Link to="/">Laptops</Link>
            <Link to="/">Desktops</Link>
            <Link to="/">Watches</Link>
            <Link to="/">Accessories</Link>
          </FooterList>
          <FooterList>
            <h3 className='text-base font-bold'>Customer Service</h3>
            <Link to="/">Contact Us</Link>
            <Link to="/">Shipping Policy</Link>
            <Link to="/">Returns & Exchanges</Link>
            <Link to="/">Watches</Link>
            <Link to="/">FAQs</Link>
          </FooterList>
          <div className='w-full md:w-1/3 mb-6 md:bm-0'>
            <h3 className='text-base font-bold mb-2'>About Us</h3>
            <p className='mb-2'>
              Lorem ipsum dolor, sit amet consectetur 
              adipisicing elit. Laudantium placeat fugit sequi cumque 
              quae distinctio facilis quisquam et sit optio dolore, 
              a adipisci accusamus voluptatibus consequatur 
              labore iure doloremque deleniti!
            </p>
            <p>&copy; {new Date().getFullYear()}.
              E~Shop. All rights reserved
            </p>
          </div>
          <FooterList>
            <h3 className='text-base font-bold mb-2'>Follow Us</h3>
            <div className='flex gap-2'>
            <Link to="/"><MdFacebook size={24}/></Link>
            <Link to="/"><AiFillTwitterCircle size={24}/></Link>
            <Link to="/"><AiFillInstagram size={24}/></Link>
            <Link to="/"><AiFillYoutube size={24}/></Link>
            </div>
          </FooterList>
        </div>
      </Container>
    </footer>
      
  )
}

export default Footer

