import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";
import booksList from '../../../api/books/list.json'

import { IBookItem } from "../../../app/models/IBookItem";

export default class BooksPageService {

    static async getAllBooks(): Promise<AxiosResponse<IBookItem[]>> {
        return $api.get('/book/search-books'); 
    }
}