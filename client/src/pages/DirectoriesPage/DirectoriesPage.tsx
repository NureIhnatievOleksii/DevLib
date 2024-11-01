import React, { useEffect, useState } from 'react';
import { IDirectoryItem } from '../../app/models/IDirectoryItem';
import DirectorItem from '../../components/DirectorItem/DirectorItem';
import styles from './DirectoriesPage.module.css'; // Імпорт стилів
import DirectoriesPageService from './api/DirectoriesPageService'; // Імпорт сервісу

const DirectoriesPage = () => {
    const [directories, setDirectories] = useState<IDirectoryItem[]>([]);

    useEffect(() => {
        const fetchDirectories = async () => {
            try {
                const directoriesData = await DirectoriesPageService.getDirectories(); // Не передаємо параметр
                const formattedDirectories: IDirectoryItem[] = directoriesData.map(directory => ({
                    directory_id: directory.DirectoryId,
                    directoryName: directory.DirectoryName,
                    img_link: directory.ImgLink
                }));
                setDirectories(formattedDirectories);
            } catch (error) {
                console.error("Error fetching directories:", error);
            }
        };

        fetchDirectories();
    }, []);

    return (
        <div className={styles.container}>
            {directories.length === 0 ? (
                <p>Немає доступних довідників.</p>
            ) : (
                directories.map((directory) => (
                    <div className={styles.itemWrapper} key={directory.directory_id}>
                        <DirectorItem directory={directory} />
                    </div>
                ))
            )}
        </div>
    );
};

export default DirectoriesPage;
