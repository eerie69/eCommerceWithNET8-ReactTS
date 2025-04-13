import React, { useCallback, useState } from 'react'
import { AiFillCaretDown } from 'react-icons/ai'
import Avatar from '../../Comments/ProductCommentListItem/Avatar/Avatar'
import { Link } from 'react-router-dom'
import { MenuItem } from '@mui/material'

type Props = {}

const UserMenu = (props: Props) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggleOpen = useCallback(() => {
    setIsOpen((prev) => !prev);
  }, []);

  return (
    <div className='relative z-30'>
      <div 
        onClick={toggleOpen} 
        className='
        p-2 border-[1px] 
        border-slate-400 
        flex 
        flex-row 
        items-center 
        gap-1 
        rounded-full
        cursor-pointer
        hover:shadow-md
        transition
        text-slate-700
        '>
        <Avatar />
        <AiFillCaretDown />
      </div>
      {isOpen && (
        <div className='
        absolute
        rounded-md
        shadow-md
        w-[170px]
        bg-white
        overflow-hidden
        right-0
        top-12
        text-sm
        flex
        flex-col
        cursor-pointer
        '>
          <div>
            <Link to={`/`}>
              <MenuItem onClick={toggleOpen}>
                Your Orders
              </MenuItem>
            </Link>
          </div>
        </div>
      )}
    </div>
  )
}

export default UserMenu;