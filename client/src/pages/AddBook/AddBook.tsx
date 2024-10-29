import React, { useEffect, useState } from 'react'
import RecordType from '../../components/RecordType/RecordType';
import styles from './AddBook.module.css'
import AddRecordInputRow from '../../components/AddRecordInputRow/AddRecordInputRow';
const AddBook = () => {

  const [bookName,setBookName] = useState<string>('')
  const [bookAutor,setBookAutor] = useState<string>('')

  return (
    <div className={styles.main}>
      <RecordType recordType = {'book'}/>
      <AddRecordInputRow 
        title='Назва книги' placeholder='Введіть назву'
        value={bookName} setValue={setBookName}
        type={'normal'}
      />
      <AddRecordInputRow 
        title='Автор' placeholder='Введіть автора'
        value={bookAutor} setValue={setBookAutor}
        type={'normal'}
      />
    </div>
  )
}

export default AddBook
