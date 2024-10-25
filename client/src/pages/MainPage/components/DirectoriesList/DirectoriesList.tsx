import React, { FC } from 'react'
import { IDirectoryItem } from '../../../../app/models/IDirectoryItem';
import styles from './DirectoriesList.module.css'
import img from '../../../../assets/images/api/puthon1.png'
import DirectorItem from '../../../../components/DirectorItem/DirectorItem';
interface DirectoriesListProps{
    directories: IDirectoryItem[]
}
const DirectoriesList:FC<DirectoriesListProps> = (props) => {
  return (
    <div className={styles.list}>
        {props.directories.map(directory =>
            <DirectorItem directory= {directory} key = {directory.directory_id}/> 
        )}
    </div>
  )
}

export default DirectoriesList
