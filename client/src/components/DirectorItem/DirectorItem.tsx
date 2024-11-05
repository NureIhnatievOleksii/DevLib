import React, { FC, MouseEvent } from 'react';
import styles from './DirectorItem.module.css'
import { Link, useNavigate } from 'react-router-dom';
import { IDirectoryItem } from '../../models/IDirectoryItem';
import EditItemButton from '../../UI/EditItemButton/EditItemButton';

interface DirectorItemProps {
  directory: IDirectoryItem
}
const DirectorItem: FC<DirectorItemProps> = ({ directory }) => {
  const staticUrl = process.env.STATIC_URL || 'http://localhost:3200/';
  const navigate = useNavigate()
  const handleEdit = () => {
    console.log(directory.directoryId)
};
  return (
    <div onClick={()=> navigate(`/reference/${directory.directoryId}`)} className={styles.item} key={directory.directoryId}>
      <EditItemButton onClick={handleEdit} />
      <img src={staticUrl + (directory.imgLink || directory.directoryImg)} alt={directory.directoryName} />
      <div className={styles.title}>
        {directory.directoryName}
      </div>
    </div>
  )
}

export default DirectorItem
