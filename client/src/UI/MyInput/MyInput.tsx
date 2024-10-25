import React, { FC } from 'react';
import styles from './MyInput.module.css'
interface MyInput {
    value: string;
    setValue: (value: string) => void,
    className?:string;
    placeholder: string
}
const MyInput:FC<MyInput> = (props) => {
  return (
    <input type="text" 
    placeholder={props.placeholder}
    className={`${props.className&&props.className} ${styles.input}`}
    value={props.value} onChange={e => props.setValue(e.target.value)}/>
  )
}

export default MyInput
