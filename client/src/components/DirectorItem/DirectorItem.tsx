import React, { FC } from 'react';
import styles from './DirectorItem.module.css'
import { Link } from 'react-router-dom';
import { IDirectoryItem } from '../../app/models/IDirectoryItem';
interface DirectorItemProps {
  directory: IDirectoryItem
}
const DirectorItem: FC<DirectorItemProps> = ({ directory }) => {
  const staticUrl = process.env.STATIC_URL || 'http://localhost:3200/';
  console.log(directory)
  return (
    <Link to={`/reference/${directory.directoryId}`} className={styles.item} key={directory.directoryId}>
      <img src={staticUrl + directory.imgLink} alt={directory.directoryName} />
      <div className={styles.title}>
        {directory.directoryName}
      </div>
    </Link>
  )
}

export default DirectorItem
