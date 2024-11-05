import React, { FC } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './BookItem.module.css';
import { IBookItem } from '../../models/IBookItem';
import EditItemButton from '../../UI/EditItemButton/EditItemButton';


interface BookItemProps {
  book: IBookItem;
}

const BookItem: FC<BookItemProps> = ({ book }) => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/books/${book.bookId}`);
  };
  const bookImg = process.env.STATIC_URL || 'http://localhost:3200';
  const handleEdit = () =>{
    alert(book.bookId)
  }
  return (
    <div className={styles.item} onClick={handleClick}>
        <EditItemButton onClick={handleEdit} className={styles.editButton}/>
      <img src={bookImg + (book.photoBook??book.bookImg)} alt={book.bookName} />
      <div className={styles.title}>{book.bookName}</div>
    </div>
  );
};

export default BookItem;
