import React, { FC } from 'react';
import styles from './DirectorItem.module.css'
import { Link } from 'react-router-dom';
import { IDirectoryItem } from '../../app/models/IDirectoryItem';
interface DirectorItemProps {
  directory: IDirectoryItem
}
const DirectorItem: FC<DirectorItemProps> = ({ directory }) => {
  const staticUrl = process.env.STATIC_URL || 'https://localhost:3200';

  return (
    <Link to={`/reference/${directory.directory_id}`} className={styles.item} key={directory.directory_id}>
      <img src={staticUrl + directory.img_link} alt={directory.directoryName} />
      <div className={styles.title}>
        {directory.directoryName}
      </div>
    </Link>
  )
}

export default DirectorItem
