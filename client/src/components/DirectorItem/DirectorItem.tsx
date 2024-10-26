import React, { FC } from 'react';
import styles from './DirectorItem.module.css'
import { IDirectoryItem } from '../../app/models/IDirectoryItem';
interface DirectorItemProps{
    directory: IDirectoryItem
}
const DirectorItem:FC<DirectorItemProps> = ({directory}) => {
  return (
    <div className={styles.item} key={directory.directory_id}>
                <img src={directory.img_link} alt={directory.directoryName} />
                <div className={styles.title}>
                    {directory.directoryName}
                </div>
            </div>
  )
}

export default DirectorItem
