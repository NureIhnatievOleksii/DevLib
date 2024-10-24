import React, { useEffect } from 'react'
import AppLayout from '../../layouts/AppLayout/AppLayout'
import { useHeaderStore } from '../../layouts/Header/store/header'

const MainPage = () => {
  const setHeaderVersion = useHeaderStore(store => store.setHeaderVersion);



  useEffect(()=>{
    setHeaderVersion('normal')
  },[]);
  
  return (
    <div>
      MainPage
    </div>
  )
}

export default MainPage
