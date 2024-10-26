import React, { useEffect, useState } from 'react'
import AppLayout from '../../layouts/AppLayout/AppLayout'

import $api from '../../app/api/http';
import MainPageService from './api/MainPageService';
import { IDirectoryItem } from '../../app/models/IDirectoryItem';
import styles from './MainPage.module.css'
import DirectoriesList from './components/DirectoriesList/DirectoriesList';
import LastBooksList from './components/LastBooksList/LastBooksList';
import { useHeaderStore } from '../../layouts/Header/store/header';
import { IBookItem } from '../../app/models/IBookItem';
const MainPage = () => {
  const setHeaderVersion = useHeaderStore(store => store.setHeaderVersion);
  const [directories,setDirectories] = useState<IDirectoryItem[]>([]);
  const [books,setBooks] = useState<IBookItem[]>([]);


  useEffect(() => {
    
    const fetch = async () =>{
      const directoriesData = await MainPageService.get8Directories();
      setDirectories(directoriesData);
      const booksData = await MainPageService.getLastBooks();
      setBooks(booksData)
    }
    fetch()
    setHeaderVersion('normal')
  }, []);

  return (
    <div className={`${styles.main} container`}>
        <DirectoriesList directories={directories}/>
        <LastBooksList books = {books}/>
    </div>
  )
}

export default MainPage