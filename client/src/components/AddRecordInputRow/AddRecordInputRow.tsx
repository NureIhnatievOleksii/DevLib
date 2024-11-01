import React, { FC, ReactNode } from 'react'
import styles from './AddRecordInputRow.module.css'
import FileUpload from '../FileUpload/FileUpload';
import clipIcon from '../../assets/images/icons/Clip Icon.png'
interface AddRecordInputRow {
  title: string,
  value?: string,
  setValue?: (value: string ) => void,
  placeholder?: string,
  type: 'normal' | 'file' | 'custom';
  onFileChange?: (file: File | null) => void;
  children?: ReactNode
}

const AddRecordInputRow: FC<AddRecordInputRow> = (props) => {
  return (
    <div className={styles.main}>
      <div className={styles.title}>{props.title}</div>
      {props.type == 'normal' &&
        <input type="text" className={styles.input} placeholder={props.placeholder}
          value={props.value} onChange={e => props.setValue && props.setValue(e.target.value)}
        />
      }
      {props.type == 'file' &&
        <div className={`${styles.input} ${styles.file}`}>
          <FileUpload onFileChange={props.onFileChange as (file: File | null) => void} className={styles.file}>
            <img src={clipIcon} alt="" />
            <div className={styles.fileText}>{props.placeholder}</div>
          </FileUpload>
        </div>
      }
      {props.type == 'custom' &&
        props.children
      }
    </div>
  )
}

export default AddRecordInputRow
