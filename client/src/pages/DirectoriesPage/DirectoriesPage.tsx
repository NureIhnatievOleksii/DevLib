import React, { useEffect, useState } from 'react';
import { IDirectoryItem } from '../../app/models/IDirectoryItem';
import DirectorItem from '../../components/DirectorItem/DirectorItem';
import styles from './DirectoriesPage.module.css'; // Імпорт стилів
import DirectoriesPageService from './api/DirectoriesPageService'; // Імпорт сервісу

const DirectoriesPage = () => {
  const [directories, setDirectories] = useState<IDirectoryItem[]>([]);

  useEffect(() => {
    const fetchDirectories = async () => {
      const directoriesData = await DirectoriesPageService.getDirectories();
      setDirectories(directoriesData);
    };

    fetchDirectories();
  }, []);

  return (
    <div className={styles.container}>
      {directories.map((directory) => (
        <div className={styles.itemWrapper} key={directory.directory_id}>
          <DirectorItem directory={directory} />
        </div>
      ))}
    </div>
  );
};

export default DirectoriesPage;
