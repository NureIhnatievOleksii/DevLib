// import React, { useEffect, useState } from 'react';
// import { useParams } from 'react-router-dom';
// import ReferencePageService from './api/ReferencePageService';
// import DirectoryContent from './components/DirectoryContent';
// import DirectorItem from '../../components/DirectorItem/DirectorItem';
// import data from '../../api/directories/list.json';

// const ReferencePage: React.FC = () => {
//         const { directoryId } = useParams<{ directoryId: string }>();
//         const [directory, setDirectory] = useState<any>(null); 
    
//         useEffect(() => {
//             const fetchDirectoryData = () => {
//                 const foundDirectory = data.find((item: any) => item.directory_id.toString() === directoryId);
//                 setDirectory(foundDirectory);
//             };
    
//             fetchDirectoryData();
//         }, [directoryId]);
    
//         if (!directory) {
//             return <div>Завантаження...</div>;
//         }
    
//         return (
//             <div>
//                 <h1>Довідник</h1>
//                 <DirectoryContent 
//                 directory_name={directory.directoryName} 
//                 articles={directory.articles} />
//             </div>
//         );
//     };
    
//     export default ReferencePage;
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import ReferencePageService from './api/ReferencePageService';
import DirectoryContent from './components/DirectoryContent';
import { IArticleItem } from '../../app/models/IArticleItem';
import { IDirectoryItem } from '../../app/models/IDirectoryItem';
import { useHeaderStore } from '../../layouts/Header/store/header';

interface Article {
  chapterName: string; // This should map to IArticleItem's chapter_name
}

const ReferencePage: React.FC = () => {
  const setHeaderVersion = useHeaderStore(store => store.setHeaderVersion);
  const { directoryId } = useParams<{ directoryId: string }>();
  const [articles, setArticles] = useState<Article[]>([]);
  const [loading, setLoading] = useState(true);
  const [directory, setDirectory] = useState<IDirectoryItem>();

  useEffect(() => {
    const fetchArticles = async () => {
      try {
        setLoading(true);
        // Send request to fetch chapter names
        const response = await ReferencePageService.getAllChapterNames(directoryId!);
        const directoryResponse = await ReferencePageService.getDirectory(directoryId!);
        console.log("Fetched articles:", response.data); // Debugging output


        // Set articles state with fetched data
        setArticles(response.data);
        setDirectory(directoryResponse.data);
      } catch (error) {
        console.error("Error loading articles:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchArticles();
  }, [directoryId]);
  setHeaderVersion('minimized')
  // Loading and error display logic
  if (loading) return <p>Завантаження...</p>;
  if (!articles.length) return <p>Статті не знайдені.</p>;

  return (
    <DirectoryContent 
      directory_name={`${directory?.directoryName}`} // Optionally display directory name if needed
      articles={articles} 
    />
  );
};

export default ReferencePage;
