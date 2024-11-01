import React, { FC, useEffect, useState } from 'react'
import RecordType from '../../components/RecordType/RecordType';
import styles from './BookManager.module.css'
import AddRecordInputRow from '../../components/AddRecordInputRow/AddRecordInputRow';
import FileUpload from '../../components/FileUpload/FileUpload';
import ListEditor, { IListItem } from '../../components/ListEditor/ListEditor';
import BlueButton from '../../UI/BlueButton/BlueButton';
import { checkServerIdentity } from 'tls';
import { validateStringFields } from '../../helpers/checkStringFields';
import { AddBookReq } from './api/req';
import BookManagerService from './api/BookManagerService';
interface BookManagerProps {
  action: "create" | "edit"
}
const BookManager: FC<BookManagerProps> = (props) => {

  const [bookName, setBookName] = useState<string>('')
  const [bookAutor, setBookAutor] = useState<string>('')
  const [material, setMaterial] = useState<File | null>(null);
  const [photo, setPhoto] = useState<File | null>(null);
  const [tags, setTags] = useState<IListItem[]>([])

  const publish = async () => {
    const reqData: AddBookReq = {
      BookName: bookName,
      Author: bookAutor,
      BookPdf: material as File,
      BookImg: photo as File,
      tags: tags.map(item => item.text)
    }
    if (validateStringFields(reqData)) {
      console.log(reqData);
      try {
        await BookManagerService.addBook(reqData)
      } catch (e) {
        console.log(e)
        alert('Виникла помилка');
      }
    }

  }

  const handelAction = () => {
    if (props.action == 'create') {
      publish()
    } else {

    }

  }
  return (
    <div className={styles.main}>
      <RecordType recordType={'book'} />
      <AddRecordInputRow
        title='Назва книги' placeholder='Введіть назву'
        value={bookName} setValue={setBookName}
        type={'normal'}
      />
      <AddRecordInputRow
        title='Автор' placeholder='Введіть автора'
        value={bookAutor} setValue={setBookAutor}
        type={'normal'}
      />
      <AddRecordInputRow
        title='Матеріал' placeholder='Прикріпити файл'
        onFileChange={setMaterial}
        type={'file'}
      />
      <AddRecordInputRow
        title='Фото' placeholder='Прикріпити фото'
        onFileChange={setPhoto}
        type={'file'}
      />
      <AddRecordInputRow
        title='Теги'
        type={'custom'}
      >
        <ListEditor
          setList={setTags}
          itemPlaceholder='Новий тег' />
      </AddRecordInputRow>

      <BlueButton onClick={handelAction}>
        Опублікувати
      </BlueButton>
    </div>
  )
}

export default BookManager;
