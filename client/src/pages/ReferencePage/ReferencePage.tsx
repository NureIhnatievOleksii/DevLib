import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
// import AppService from '../../app/api/service/AppService';
import DirectoryContent from './components/DirectoryContent';
import DirectorItem from '../../components/DirectorItem/DirectorItem';
import data from '../../api/directories/list.json';

interface Article {
  article_id: string;
  name: string;
}

interface DirectoryData {
  directory_id: string;
  directory_name: string;
  articles: Article[];
}

const ReferencePage: React.FC = () => {
//     const { directoryId } = useParams<{ directoryId: string }>();
//     const [directory, setDirectory] = useState<any>(null);
//   const [loading, setLoading] = useState(true);

  // useEffect(() => {
    // const fetchDirectoryData = async () => {
    //   try {
    //     setLoading(true);
    //     // Отправляем запрос на сервер
    //     const response = await AppService.getDirectory(directoryId!);
    //     console.log("Fetched directory data:", response.data); 

    //     // Проверяем, получили ли мы данные
    //     if (response.data) {
    //       setDirectoryData(response.data);
    //     } else {
    //       console.log("No data returned for directory:", directoryId); // Сообщение, если данных нет
    //       setDirectoryData(null); 
    //     }
    //   } catch (error) {
    //     console.error("Ошибка при загрузке данных справочника:", error);
    //     setDirectoryData(null);
    //   } finally {
    //     setLoading(false);
    //   }
        const { directoryId } = useParams<{ directoryId: string }>();
        const [directory, setDirectory] = useState<any>(null); 
    
        useEffect(() => {
            const fetchDirectoryData = () => {
                const foundDirectory = data.find((item: any) => item.directory_id.toString() === directoryId);
                setDirectory(foundDirectory);
            };
    
            fetchDirectoryData();
        }, [directoryId]);
    
        if (!directory) {
            return <div>Завантаження...</div>;
        }
    
        return (
            <div>
                <h1>Довідник</h1>
                <DirectoryContent 
                directory_name={directory.directoryName} 
                articles={directory.articles} />
            </div>
        );
    };
    
    export default ReferencePage;