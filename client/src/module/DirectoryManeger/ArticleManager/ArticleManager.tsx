import React, { useEffect, useState } from 'react'
import useDirectoryManagerStore from '../store/directoryManeger';
import TextEditor from '../../../components/TextEditor/TextEditor';
import styles from './ArticleManager.module.css'
import AddRecordInputRow from '../../../components/AddRecordInputRow/AddRecordInputRow';
import BlueButton from '../../../UI/BlueButton/BlueButton';
import { useNavigate, useParams } from 'react-router-dom';
import { RouteNames } from '../../../app/router';
import { v4 as uuidv4 } from 'uuid';
const ArticleManager = () => {
    const { articleId } = useParams();
    const navigate = useNavigate()

    const articles = useDirectoryManagerStore(state => state.articles);
    const setArticles = useDirectoryManagerStore(state => state.setArticles);
    const addArticle = useDirectoryManagerStore(state => state.addArticle);
    const [name, setName] = useState<string>('');
    const [content, setContent] = useState<string>('');
 

    useEffect(() => {
        if (articleId !== 'new') {
            const article = articles.find(article => article.articleId === articleId);
            if (article) {
                setName(article.articleName)
                setContent(article.articleContent)
            }

            console.log(articles, article)
        }
    }, [])
    const haldleSave = () => {
        if (articleId !== 'new') {
            setArticles(
                articles.map(article => (article.articleId == articleId) ? { ...article, articleName: name, articleContent: content } : article)
            )
        } else {
            addArticle({
                articleContent: content,
                articleName: name,
                articleId: uuidv4()
            })
        }

        navigate(RouteNames.ADD_DIRECTORY)
    }
    return (
        <div className={styles.main}>
            <div className={styles.text}>
                Статті
            </div>
            <AddRecordInputRow
                title='Назва статті' placeholder='Введіть назву'
                value={name} setValue={setName}
                
                type={'normal'}
            />
            <TextEditor setValue={setContent} value={content} className={styles.content} />
            <BlueButton onClick={haldleSave}>
                Зберегти
            </BlueButton>
        </div>
    )
}

export default ArticleManager
