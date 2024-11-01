import { ChangeEvent, FC, ReactNode, useRef } from 'react';

interface FileUploadProps {
    onFileChange: (file: File | null) => void;
    children: ReactNode;
    className?: string
}

const FileUpload: FC<FileUploadProps> = ({ onFileChange, children,className }) => {
    const inputRef = useRef<HTMLInputElement | null>(null);

    const handleClick = () => {
        inputRef.current?.click();
    };

    const handleFileChange = (event: ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files ? event.target.files[0] : null;
        onFileChange(file);
        if (inputRef.current) inputRef.current.value = ''; // очистка после выбора файла
    };

    return (
        <>
            <input
                type="file"
                ref={inputRef}
                style={{ display: 'none' }}
                onChange={handleFileChange}
            />
            <div onClick={handleClick} /* style={{ display: 'inline-block' }} */ className={className}>
                {children}
            </div>
        </>
    );
};

export default FileUpload;
