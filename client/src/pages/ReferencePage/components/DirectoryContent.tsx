import React from 'react';
import styles from './DirectoryContent.module.css';
import { Link } from 'react-router-dom';

interface Article {
  chapterName: string;
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
      {articles.map((article, index) => (
        <li key={index}>
          <Link to={`/article/${index}`}>{article.chapterName}</Link>
        </li>
      ))}
    </ol>
  </div>
);

export default DirectoryContent;
