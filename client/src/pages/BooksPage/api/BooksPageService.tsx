import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";
import { IBookItem } from "../../../app/models/IBookItem";

export default class BooksPageService {
    static async searchBooks(bookName?: string): Promise<AxiosResponse<IBookItem[]>> {
        const url = `/book/search-books/${bookName}`;
        return await $api.get<IBookItem[]>(url);
    }
    static async getAllBooks(): Promise<AxiosResponse<IBookItem[]>> {
        return $api.get('/book/search-books'); 
    }
}
