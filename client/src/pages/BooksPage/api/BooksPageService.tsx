import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";
import booksList from '../../../api/books/list.json'

import { IBookItem } from "../../../app/models/IBookItem";

export default class BooksPageService {

    static async getAllBooks(): Promise<IBookItem[]> {
        return booksList; // Replace with API call if needed
        // return await $api.get('/get-all-books');
    }
}