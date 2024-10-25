import React, { FC } from 'react'
import styles from './BookItem.module.css';
import { IBookItem } from '../../app/models/IBookItem';
interface BookItem{
    book: IBookItem
}
const BookItem:FC<BookItem> = ({book}) => {
  return (
    <div className={styles.item}>
        <img src={book.book_photo} alt={book.book_name}/>
        <div className={styles.title}>{book.book_name}</div>
    </div>
  )
}

export default BookItem
