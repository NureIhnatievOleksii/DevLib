import React, { FC, useEffect, useState } from 'react'
import RecordType from '../../components/RecordType/RecordType'
import styles from './DirectoryManeger.module.css'
import AddRecordInputRow from '../../components/AddRecordInputRow/AddRecordInputRow'
import ListEditor, { IListItem } from '../../components/ListEditor/ListEditor'
import BlueButton from '../../UI/BlueButton/BlueButton'
import useDirectoryManagerStore from './store/directoryManeger'
import { v4 as uuidv4 } from 'uuid';
import { useNavigate } from 'react-router-dom'
interface DirectoryManegerProps {
    action: "create" | "edit"
}
const DirectoryManeger: FC<DirectoryManegerProps> = (props) => {
    const naviagte = useNavigate()

    const [articlesList, setArticlesList] = useState<IListItem[]>();
    const addDirectory = useDirectoryManagerStore(state => state.addDirectory);
    const setFile = useDirectoryManagerStore(state => state.setFile);
    const file = useDirectoryManagerStore(state => state.file);
    const directoryName = useDirectoryManagerStore(state => state.directoryName);
    const setDirectoryName = useDirectoryManagerStore(state => state.setDirectoryName);
    const articles = useDirectoryManagerStore(state => state.articles);

    const handleCreate = async () => {
        await addDirectory()
        naviagte('/')
    }
    const handleAction = () => {
        if (props.action == 'create') handleCreate();
    }

    useEffect(() => {
        const newArticlesList = articles.map(article => 
            ({ text: article.articleName, id: article.articleId })
        );
        setArticlesList(newArticlesList);
    }, [articles])

    return (
        <div className={styles.main}>
            <RecordType recordType={'directory'} />
            <AddRecordInputRow
                title='Назва довідника' placeholder='Введіть назву'
                value={directoryName} setValue={setDirectoryName}
                type={'normal'}
            />
            <AddRecordInputRow
                title='Фото' placeholder='Прикріпить фото'
                onFileChange={setFile}
                type={'file'}
                file={file}
            />
            <div className={styles.text}>
                Статті
            </div>
            <ListEditor
                setList={setArticlesList}
                list={articlesList}
                
                itemPlaceholder='Нова стаття' type='article' />
            <BlueButton onClick={handleAction}>
                Опублікувати
            </BlueButton>
        </div>
    )
}

export default DirectoryManeger
