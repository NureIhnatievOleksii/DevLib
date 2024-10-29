import React, { useEffect, useState } from 'react';  
    import AppLayout from '../../layouts/AppLayout/AppLayout';
    import { useParams, useNavigate } from 'react-router-dom';
    import { useHeaderStore } from '../../layouts/Header/store/header';
    import styles from './ArticlePage.module.css';
    import homeIcon from '../../assets/images/home-icon.png';
    import toggleIcon from '../../assets/images/ToggleSidePanel.png';
    
    interface ArticlePageParams {
      articleId: string;
      name: string;
    }
    
    // Оголошення інтерфейсу Article
    interface Article {
    article_id: string; // або number, в залежності від  структури даних
    name: string;
    }

    interface Directory {
        directory_id: number;
        directoryName: string;
        articles: Article[];
      }

    const ArticlePage: React.FC = () => {
        const { articleId } = useParams<{ articleId: string }>();
        const [isSidebarOpen, setSidebarOpen] = useState(true);
      const navigate = useNavigate();
      const setHeaderVersion = useHeaderStore(store => store.setHeaderVersion);
      const toggleSidebar = () => {
        setSidebarOpen(!isSidebarOpen); // Перемикаємо видимість бічної панелі
    };
      
      // Додано для зберігання даних розділу
      const [sectionData, setSectionData] = useState({
        title: '',
        navigationItems: ['Пункт 1', 'Пункт 2'], // Це буде оновлено з API
      });
      const [articleTitle, setArticleTitle] = useState('');
    
      useEffect(() => {
        setHeaderVersion('minimized');
    
    // Зчитування даних з API
    const fetchArticleData = async () => {
    const response = await fetch('/api/directories/list.json'); // Потрібно вказати правильний шлях до файлу
    const data: Directory[] = await response.json();
    
    const article = data.flatMap(directory => directory.articles).find(article => article.article_id === articleId);
    if (article) {
      setArticleTitle(article.name);
    }
    };

  fetchArticleData();
    
        return () => setHeaderVersion('normal');
      }, [setHeaderVersion, articleId]);
    
      const handleHomeClick = () => {
        navigate('/reference');
      };
    
      const scrollToSection = (section: string) => {
        const sectionElement = document.getElementById(section);
        if (sectionElement) {
          sectionElement.scrollIntoView({ behavior: 'smooth' });
        }
      };
    
      return (
        <div className={styles.articlePage}>
          <div className={`${styles.sidebar} ${isSidebarOpen ? '' : styles.hidden}`}>
            <h2 className={styles.sectionTitle}>Розділ N</h2>
            <h3 className={styles.sectionName}>Назва розділу</h3>
            <h4 className={styles.navigationTitle}>Навігація</h4>
            <nav className={styles.navigation}>
              <ul>
                {sectionData.navigationItems.map((item, index) => (
                  <li key={index}>
                    <button onClick={() => scrollToSection(item)}>{item}</button>
                  </li>
                ))}
              </ul>
            </nav>
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

            <h1 className={styles.articleTitle}>Стаття {articleId}</h1>
            <p className={styles.commentsSection}>Тут буде контент статті {articleId}</p>
            <section className={styles.commentsSection}>
              <h2>Коментарі</h2>
              <form className={styles.commentForm}>
                <textarea placeholder="Додати коментар" className={styles.commentInput}></textarea>
                <button type="submit" className={styles.submitButton}>Відправити</button>
              </form>
      
              <div className={styles.comment}>
                <p><strong>User Name</strong> - 1 хв. тому</p>
                <p>Приклад коментаря до статті.</p>
              </div>
            </section>
          </div>
        </div>
      );
    }      
    
    export default ArticlePage;
    
