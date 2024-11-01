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
    navigate(`/books/${book.bookId}`);
  };
  const bookImg = process.env.STATIC_URL || 'https://localhost:3200';
  return (
    <div className={styles.item} onClick={handleClick}>
      <img src={bookImg+ book.photoBook} alt={book.bookName} />
      <div className={styles.title}>{book.bookName}</div>
    </div>
  );
};

export default BookItem;
