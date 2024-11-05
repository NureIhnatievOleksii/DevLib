import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { useHeaderStore } from '../../layouts/Header/store/header';
import PdfViewer from './components/PdfViewer';
import BookService from '../BookDetailsPage/api/BookService';
import styles from './ReadingPage.module.css';
import { IBookDetails } from '../../models/IBookDetails';
import SimplePdfViewer from './components/PdfViewer';

interface Note {
    id: number;
    text: string;
    quote: string;
}

const ReadingPage = () => {
    const { bookId } = useParams<{ bookId: string }>();
 
    const setHeaderVersion = useHeaderStore((store) => store.setHeaderVersion);

    const [notes, setNotes] = useState<Note[]>([]);
    const [newNote, setNewNote] = useState('');
    const [quote, setQuote] = useState('');
    const [pdfUrl, setPdfUrl] = useState<string>(''); 

    useEffect(() => {
        setHeaderVersion('minimized');
    
        const fetchBookDetails = async () => {
            if (!bookId) {
                console.error('No book ID provided');
                return;
            }
        
            try {
                const bookDetails: IBookDetails = await BookService.getBookDetails(bookId);
                const baseUrl = 'http://localhost:3200'; 
                const fullPdfUrl = `${baseUrl}${bookDetails.pdf}`; 
                console.log('Fetched Book Details:', bookDetails);
                console.log('PDF URL:', fullPdfUrl);
                setPdfUrl(fullPdfUrl); 
            } catch (error) {
                console.error('Error fetching book details:', error);
            }
        };
        
        fetchBookDetails();
    }, [setHeaderVersion, bookId]);
    

    const handleAddNote = () => {
        if (newNote.trim()) {
            const note: Note = {
                id: Date.now(),
                text: newNote,
                quote: quote,
            };
            setNotes([...notes, note]);
            setNewNote('');
            setQuote('');
        }
    };

    const handleTextSelection = () => {
        const selection = window.getSelection();
        if (selection && selection.toString().trim()) {
            setQuote(selection.toString());
            setNewNote(selection.toString());
        }
    };

    const handleDeleteNote = (id: number) => {
        setNotes(notes.filter((note) => note.id !== id));
    };

    return (
        <div className={styles.container}>
            <div className={styles.notesSection}>
                <h2>Додати нотатки</h2>
                <div className={styles.addNoteForm}>
                    {quote && (
                        <blockquote className={styles.quote}>
                            {quote}
                        </blockquote>
                    )}
                    <textarea
                        value={newNote}
                        onChange={(e) => setNewNote(e.target.value)}
                        placeholder="Введіть нотатку..."
                        className={styles.noteInput}
                    />
                    <button 
                        onClick={handleAddNote}
                        className={styles.addButton}
                    >
                        Зберегти
                    </button>
                </div>
            </div>

            <div className={styles.pageContent}>
            <embed src={pdfUrl} width="800px" height="1000px" />
             {/*    {pdfUrl && <PdfViewer fileUrl={pdfUrl} />}  */}
            {/*  <SimplePdfViewer fileUrl = {pdfUrl}/> */}
   
            </div>

            <div className={styles.notesList}>
                <h2>Ваші нотатки:</h2>
                {notes.map((note) => (
                    <div key={note.id} className={styles.noteItem}>
                        <blockquote className={styles.noteQuote}>
                            {note.quote}
                        </blockquote>
                        {note.text}
                        <button 
                            onClick={() => handleDeleteNote(note.id)} 
                            className={styles.deleteButton}
                        >
                            ×
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ReadingPage;
