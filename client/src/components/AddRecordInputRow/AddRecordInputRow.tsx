import React, { FC } from 'react'
import styles from './AddRecordInputRow.module.css'
interface AddRecordInputRow{
    title: string, 
    value: string,
    setValue: (value:string) => void,
    placeholder: string,
    type: 'normal' ;
}

const AddRecordInputRow:FC<AddRecordInputRow> = (props) => {
  return (
    <div className = {styles.main}>
        <div className={styles.title}>{props.title}</div>
        <input type="text" className={styles.input} placeholder={props.placeholder} 
        value={props.value} onChange={e=> props.setValue(e.target.value)}
        />
    </div>
  )
}

export default AddRecordInputRow
