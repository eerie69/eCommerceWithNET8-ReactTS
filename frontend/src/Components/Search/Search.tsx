import React, { ChangeEvent, FormEvent, SyntheticEvent, useState } from 'react'

interface Props {
  onClick: (e: FormEvent) => void;
  search: string | undefined;
  handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
}

const Search : React.FC<Props> = ({
  onClick, 
  search, 
  handleChange
}: Props): JSX.Element => {
  
  return (
    <div>
        <input value={search} onChange={(e) => handleChange(e)}></input>
        <button onClick={(e) => onClick(e)} />
    </div>
  )
}

export default Search; 