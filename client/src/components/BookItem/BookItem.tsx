import React, { FC } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './BookItem.module.css';
import { IBookItem } from '../../app/models/IBookItem';

interface BookItemProps {
  book: IBookItem;
}

const BookItem: FC<BookItemProps> = ({ book }) => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/books/${book.book_id}`);
  };

  return (
    <div className={styles.item} onClick={handleClick}>
      <img src={book.book_photo} alt={book.book_name} />
      <div className={styles.title}>{book.book_name}</div>
    </div>
  );
};

export default BookItem;
