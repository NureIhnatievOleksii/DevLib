import { AxiosResponse } from "axios";
import { AddBookReq } from "./req";
import $api from "../../../app/api/http";

export default class BookManagerService {
    static async addBook(dto: AddBookReq): Promise<AxiosResponse> {
        const formData = new FormData();
        formData.append('BookName', dto.BookName)
        formData.append('Author', dto.Author)
        formData.append('BookPdf', dto.BookPdf)
        formData.append('BookImg', dto.BookImg)
        dto.tags.forEach((item) => {
            formData.append('Tgas', item)
        });
        return $api.post('book/add-book', formData);
    }
}