import React, { FC, useEffect, useState } from 'react';
import styles from './ListEditor.module.css'
import addButtonIcon from '../../assets/images/icons/Add button.png'
import AutoResizeInput from '../../UI/AutoResizeInput/AutoResizeInput';
import { v4 as uuidv4 } from 'uuid';

export interface IListItem {
    text: string, id: string 
}
interface ListEditorProps {
    list?: IListItem[],
    setList: (list: IListItem[]) => void
    itemPlaceholder: string,
}
const ListEditor: FC<ListEditorProps> = (props) => {
    const [list, setList] = useState<IListItem[]>([]);
    const handelSetList = (list: IListItem[]) =>{
        setList(list);
        props.setList(list)
    }

    useEffect(() => {
        if (props.list) setList(props.list)
    }, [props.list])

    const handelChangeItemText = (value: string, id: string) => {
        const newList = list.map(item => 
            item.id === id ? { ...item, text: value } : item
        )

        handelSetList(newList);
    };
    
    const handleDeleteItem = (id: string) => {
        const newList = list.filter(item => 
            item.id !== id
        )
        handelSetList(newList);
    };
    
    const handelAddNewItem = () =>{
        const newId = uuidv4();
        const newList = [...list, {text: props.itemPlaceholder, id: newId} ]
        handelSetList(newList);
    }
    return (
        <div className={styles.list}>
            {list.map(listItem =>
                <div className={styles.listItem} key={listItem.id}>
                    <AutoResizeInput type="text" className={styles.input} value={listItem.text} onChange={e => handelChangeItemText(e.target.value,listItem.id)} />
                    <img src={addButtonIcon} className={styles.icon} alt="" onClick={()=>handleDeleteItem(listItem.id)}/>


                </div>
            )}
            <img src={addButtonIcon} className={styles.addItemIcon} onClick={handelAddNewItem}/>



        </div>
    )
}

export default ListEditor
