import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useHeaderStore } from '../../layouts/Header/store/header';
import ReviewModal from './components/ReviewModal/ReviewModal';
import DownloadModal from './components/DownloadModal/DownloadModal';
import styles from './BookDetailsPage.module.css';
import BookmarkIcon from './components/BookmarkIcon/BookmarkIcon';
import BookService from './api/BookService';
import { IBookDetails } from '../../models/IBookDetails';
import axios from 'axios';

const BookDetailsPage = () => {
    const { bookId } = useParams<{ bookId: string }>();
    const navigate = useNavigate();
    const setHeaderVersion = useHeaderStore((store) => store.setHeaderVersion);

    const [bookDetails, setBookDetails] = useState<IBookDetails | null>(null);
    const [rating, setRating] = useState<number>(0);
    const [reviewText, setReviewText] = useState('');
    const [reviews, setReviews] = useState<{ text: string }[]>([]);
    const [isReviewModalOpen, setIsReviewModalOpen] = useState(false);
    const [isDownloadModalOpen, setIsDownloadModalOpen] = useState(false);
    const [isBookmarked, setIsBookmarked] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        setHeaderVersion('minimized');

        const fetchBookDetails = async () => {
            if (bookId) {
                try {
                    const response = await BookService.getBookDetails(bookId);
                    console.log('Fetched Book Details:', response);
                    setBookDetails(response);
                } catch (error) {
                    console.error('Error fetching book details:', error);
                    setError('Failed to fetch book details.');
                    if (axios.isAxiosError(error)) {
                        console.error('Axios error message:', error.message);
                        console.error('Response data:', error.response?.data);
                    } else if (error instanceof Error) {
                        console.error('Error message:', error.message);
                    }
                }
            } else {
                console.error('Book ID is undefined');
                setError('Book ID is not available.');
            }
        };

        fetchBookDetails();
    }, [setHeaderVersion, bookId]);

    const handleRatingChange = (star: number) => {
        setRating(star);
    };

    const handleReviewSubmit = () => {
        if (reviewText) {
            const newReview = { text: reviewText };
            setReviews((prevReviews) => [...prevReviews, newReview]);
            setReviewText('');
            closeReviewModal();
        }
    };

    const handleRatingSubmit = () => {
        if (rating > 0) {

            setRating(0);
        }
    };

    const openReviewModal = () => setIsReviewModalOpen(true);
    const closeReviewModal = () => setIsReviewModalOpen(false);

    const openDownloadModal = () => setIsDownloadModalOpen(true);
    const closeDownloadModal = () => setIsDownloadModalOpen(false);

    const handleDownload = (format: string) => {
        console.log(`Download book in format: ${format}`);
    };

    const handleReadOnline = () => {
        navigate(`/reading/${bookId}/read`);
    };

    const toggleBookmark = () => {
        setIsBookmarked((prevIsBookmarked) => !prevIsBookmarked);
    };


    if (error) {
        return <div>{error}</div>;
    }


    if (!bookDetails) {
        return <div>Loading...</div>;
    }

    return (
        <div className={styles.bookDetails}>
            <BookmarkIcon
                isBookmarked={isBookmarked}
                onToggleBookmark={toggleBookmark}
            />
    
            {bookDetails.bookImg ? (
                
                <img
                    src={'http://localhost:3200' + bookDetails.bookImg}
                    alt={bookDetails.bookName}
                    className={styles.bookImage}
                />
            ) : (
                <p>Зображення книги недоступне.</p>
            )}


            <div className={styles.bookInfo}>
                <div className={styles.titleContainer}>
                    <h1 className={styles.title}>{bookDetails.bookName}</h1>
                </div>
                <p className={styles.author}>Автор: {bookDetails.author}</p>

                <div className={styles.bookStats}>
                    <div className={styles.ratingContainer}>
                        {/* <p className={styles.averageRating}>{bookDetails.averageRating.toFixed(1)} ⭐️</p> */}
                        <p className={styles.reviewsCount}>{reviews.length} відгуків</p>
                    </div>
                    <div className={styles.separator}></div>
                    <div className={styles.bookmarksContainer}>
                        <p className={styles.bookmarksCount}>
                            {isBookmarked ? 1 : 0}<br />
                            разів додано до закладок
                        </p>
                    </div>
                </div>

                <div className={styles.buttons}>
                    <button className={styles.readButton} onClick={handleReadOnline}>Читати онлайн</button>
                    <button className={styles.downloadButton} onClick={openDownloadModal}>Завантажити у форматі...</button>
                </div>
            </div>

            <div className={styles.lineSeparator}></div>

            <div className={styles.ratingSection}>
                <h2 className={styles.ratingTitle}>Оцініть цю книгу</h2>
                <p className={styles.ratingDescription}>
                    Розповісте усім, що ви думаєте про цю книгу.
                </p>
                <div className={styles.starsContainer}>
                    {[1, 2, 3, 4, 5].map((star) => (
                        <span
                            key={star}
                            onClick={() => handleRatingChange(star)}
                            className={rating >= star ? styles.filledStar : styles.emptyStar}
                        >
                            ★
                        </span>
                    ))}
                </div>
                <button className={styles.rateButton} onClick={handleRatingSubmit}>
                    Ставити оцінку
                </button>
                <button className={styles.reviewButton} onClick={openReviewModal}>
                    Написати відгук
                </button>
            </div>

            <ReviewModal
                isOpen={isReviewModalOpen}
                onRequestClose={closeReviewModal}
                reviewText={reviewText}
                setReviewText={setReviewText}
                onReviewSubmit={handleReviewSubmit}
            />

            <DownloadModal
                isOpen={isDownloadModalOpen}
                onRequestClose={closeDownloadModal}
                onDownload={handleDownload}
            />

            <div className={styles.reviews}>
                <h2>Відгуки</h2>
                {reviews.length > 0 ? (
                    reviews.map((review, index) => (
                        <div key={index} className={styles.review}>
                            <p className={styles.reviewText}>{review.text}</p>
                        </div>
                    ))
                ) : (
                    <p>Ще немає відгуків.</p>
                )}
            </div>
        </div>
    );
};

export default BookDetailsPage;
