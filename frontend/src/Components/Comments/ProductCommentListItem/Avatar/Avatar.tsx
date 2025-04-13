import React from 'react'
import { FaUserCircle } from "react-icons/fa"

type Props = {
    src?: string | null | undefined;
}

const Avatar: React.FC<Props> = ({src}) => {
    if(src) {
        return (
            <img
            src={src}
            alt='Avatar'
            className='rounded-full'
            height='30'
            width='30'
        />
      );
    }
  return <FaUserCircle size={24} />;
}

export default Avatar;