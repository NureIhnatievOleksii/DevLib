import React from 'react';
import styles from './DirectoryContent.module.css'
import { Link } from 'react-router-dom';

interface Article {
  article_id: string;
  name: string;
}

interface DirectoryContentProps {
  directory_name: string;
  articles: Article[];
}

const DirectoryContent: React.FC<DirectoryContentProps> = ({ directory_name, articles }) => (
  <div className={styles.DirectoryContent}>
    <h1>{directory_name}</h1>
    <h3>Статті</h3>
    <ol>
      {articles.map((article) => (
        <li key={article.article_id}>
        <Link to={`/article/${article.article_id}`}>{article.name}</Link></li>
      ))}
    </ol>
  </div>
);

export default DirectoryContent;
