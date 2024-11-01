import React, { useEffect, useState } from 'react';
import AppLayout from '../../layouts/AppLayout/AppLayout';
import { useParams, useNavigate } from 'react-router-dom';
import { useHeaderStore } from '../../layouts/Header/store/header';
import styles from './ArticlePage.module.css';
import ArticlePageService from './api/ArticlePageService';
import homeIcon from '../../assets/images/home-icon.png';
import toggleIcon from '../../assets/images/ToggleSidePanel.png';

interface ArticleData {
    ArticleId: string;
    ChapterName: string;
    Text: string;
}

interface ChapterData {
    ChapterId: string;
    Name: string;
}

const ArticlePage: React.FC = () => {
    const { articleId, directoryId } = useParams<{ articleId: string; directoryId: string }>();
    const [article, setArticle] = useState<ArticleData | null>(null);
    const [chapters, setChapters] = useState<ChapterData[]>([]);
    const [isSidebarOpen, setSidebarOpen] = useState(true);
    const navigate = useNavigate();
    const setHeaderVersion = useHeaderStore(store => store.setHeaderVersion);

    useEffect(() => {
        setHeaderVersion('minimized');

        const fetchArticleAndChapters = async () => {
            if (articleId && directoryId) {
                const fetchedArticle = await ArticlePageService.getArticle(articleId);
                const fetchedChapters = await ArticlePageService.getChapters(directoryId);

                setArticle(fetchedArticle);
                setChapters(fetchedChapters);
            }
        };

        fetchArticleAndChapters();

        return () => setHeaderVersion('normal');
    }, [setHeaderVersion, articleId, directoryId]);

    const toggleSidebar = () => {
        setSidebarOpen(!isSidebarOpen);
    };


    const handleHomeClick = () => {
        if (directoryId) {
            navigate(`/reference/${directoryId}`);
        } else {
            console.log("No directoryId available to navigate.");
        }
    };


    return (
        <div className={styles.articlePage}>
            <div className={`${styles.sidebar} ${isSidebarOpen ? '' : styles.hidden}`}>
                <h2 className={styles.sectionTitle}>Розділи</h2>
                {chapters.length > 0 ? (
                    chapters.map((chapter) => (
                        <h3 key={chapter.ChapterId} className={styles.sectionName}>{chapter.Name}</h3>
                    ))
                ) : (
                    <p className={styles.noChapters}>Немає розділів для відображення</p>
                )}
            </div>
            <div className={styles.content}>
                <div className={styles.buttonContainer}>
                    <button className={styles.toggleButton} onClick={toggleSidebar}>
                        <img src={toggleIcon} alt="Toggle Sidebar" className={styles.toggleIcon} />
                    </button>
                    <button className={styles.homeButton} onClick={handleHomeClick}>
                        <img src={homeIcon} alt="Home" className={styles.homeIcon} />
                    </button>
                </div>
                <h1 className={styles.articleTitle}>{article ? article.ChapterName : 'Стаття зараз недоступна'}</h1>
                <p className={styles.articleText}>{article ? article.Text : 'Тут буде контент статті'}</p>

                <section className={styles.commentsSection}>
                    <h2>Коментарі</h2>
                    <form className={styles.commentForm}>
                        <textarea placeholder="Додати коментар" className={styles.commentInput}></textarea>
                        <button type="submit" className={styles.submitButton}>Надіслати</button>
                    </form>
                    <div className={styles.comment}>
                        <p><strong>User Name</strong> - 1 хв. тому</p>
                        <p>Приклад коментаря до статті.</p>
                    </div>
                </section>
            </div>
        </div>
    );
};

export default ArticlePage;
