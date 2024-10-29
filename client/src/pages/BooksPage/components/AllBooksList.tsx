import React, { FC } from 'react';
import { IBookItem } from '../../../app/models/IBookItem';
import BookItem from '../../../components/BookItem/BookItem';
import styles from './AllBooksList.module.css';

interface AllBooksListProps {
    books: IBookItem[];
}

const AllBooksList: FC<AllBooksListProps> = ({ books }) => {
    return (
        <div className={styles.allBooksList}>
            {books.map((book) => (
               <div className={styles.item} key={book.book_id}>
               <BookItem book={book} />
           </div>
            ))}
        </div>
    );
};

export default AllBooksList;
