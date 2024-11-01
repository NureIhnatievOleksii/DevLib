import React, { useEffect, useState } from 'react';
import BooksPageService from './api/BooksPageService';
import AllBooksList from './components/AllBooksList';
import styles from './BooksPage.module.css';
import { IBookItem } from '../../app/models/IBookItem';
import { useHeaderStore } from '../../layouts/Header/store/header';

const BooksPage: React.FC = () => {
    const setHeaderVersion = useHeaderStore((store) => store.setHeaderVersion);
    const [books, setBooks] = useState<IBookItem[]>([]);

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const {data} = await BooksPageService.getAllBooks();
                setBooks(data);
                console.log(data);
            } catch (error) {
                console.error("Ошибка при загрузке книг:", error);
            }
        };
        
        fetchBooks();
        setHeaderVersion('normal'); 
    }, []);

    return (
        <div className={`${styles.booksPage} container`}>
            <AllBooksList books={books} />
        </div>
    );
};

export default BooksPage;
