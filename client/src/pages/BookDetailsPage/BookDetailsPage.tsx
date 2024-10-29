//фото,какие то числа на этой странице временные, они были надо для того чтобы прописать примерно стили :)

import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom'; 
import { useHeaderStore } from '../../layouts/Header/store/header';
import ReviewModal from './components/ReviewModal';
import DownloadModal from './components/DownloadModal'; 
import styles from './BookDetailsPage.module.css';

const BookDetailsPage = () => {
  const { bookId } = useParams<{ bookId: string }>();
  const navigate = useNavigate(); 
  const setHeaderVersion = useHeaderStore((store) => store.setHeaderVersion);
  
  const [bookDetails, setBookDetails] = useState({
    title: 'Чистий код',
    author: 'Роберт Мартін',
    tags: ['#js', '#c#', '#react'],
    reviewsCount: 0,
    averageRating: 0,
    bookmarksCount: 183,
  });

  const [rating, setRating] = useState<number>(0);
  const [reviewText, setReviewText] = useState('');
  const [reviews, setReviews] = useState<{ text: string; rating: number }[]>([]);
  const [isReviewModalOpen, setIsReviewModalOpen] = useState(false);
  const [isDownloadModalOpen, setIsDownloadModalOpen] = useState(false); 

  useEffect(() => {
    setHeaderVersion('minimized');
    
    const fetchBookDetails = async () => {
      try {
        const response = await fetch(`/api/books/${bookId}`);
        const data = await response.json();
        setBookDetails(data);
      } catch (error) {
        console.error('Помилка при отриманні деталей книги:', error);
      }
    };

    fetchBookDetails();
  }, [setHeaderVersion, bookId]);

  const handleRatingChange = (star: number) => {
    setRating(star);
    setIsReviewModalOpen(true);
  };

  const handleReviewSubmit = () => {
    if (reviewText && rating > 0) {
      const newReview = { text: reviewText, rating: rating };
      setReviews((prevReviews) => [...prevReviews, newReview]);

      setBookDetails((prevDetails) => {
        const newReviewsCount = prevDetails.reviewsCount + 1;
        const newAverageRating =
          (prevDetails.averageRating * prevDetails.reviewsCount + rating) / newReviewsCount;

        return {
          ...prevDetails,
          reviewsCount: newReviewsCount,
          averageRating: newAverageRating,
        };
      });

      setReviewText('');
      setRating(0);
      closeReviewModal();
    }
  };

  const openReviewModal = () => setIsReviewModalOpen(true);
  const closeReviewModal = () => setIsReviewModalOpen(false);

  const openDownloadModal = () => setIsDownloadModalOpen(true);
  const closeDownloadModal = () => setIsDownloadModalOpen(false);

  const handleDownload = (format: string) => {
    console.log(`Завантажити книгу у форматі: ${format}`);
  };

  const handleReadOnline = () => {
    navigate(`/reading/${bookId}/read`);
  };

  return (
    <div className={styles.bookDetails}>
      <img
        src="https://i.pinimg.com/736x/42/8d/00/428d009ef0e54bf82bdac6dc28cde302.jpg"
        alt={bookDetails.title}
        className={styles.bookImage}
      />
  
      <div className={styles.bookInfo}>
        <div className={styles.titleContainer}>
          <h1 className={styles.title}>{bookDetails.title}</h1>
        </div>
        <p className={styles.author}>Автор: {bookDetails.author}</p>
        <p className={styles.tags}>{bookDetails.tags.join(' ')}</p>
        
        <div className={styles.bookStats}>
          <div className={styles.ratingContainer}>
            <p className={styles.averageRating}>{bookDetails.averageRating.toFixed(1)} ⭐️</p>
            <p className={styles.reviewsCount}>{bookDetails.reviewsCount} відгуків</p>
          </div>
          <div className={styles.separator}></div>
          <div className={styles.bookmarksContainer}>
            <p className={styles.bookmarksCount}>
              {bookDetails.bookmarksCount}<br />
              людей додали у закладки
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
        <button className={styles.rateButton} onClick={openReviewModal}>
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
              <p className={styles.reviewRating}>{review.rating} ⭐️</p>
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
