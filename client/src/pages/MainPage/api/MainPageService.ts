import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";
import directoriesList from '../../../api/directories/list.json'
import booksList from '../../../api/books/list.json'

import { IDirectoryItem } from "../../../app/models/IDirectoryItem";
import { IBookItem } from "../../../app/models/IBookItem";
import { GetLastBooksRes } from "./res";

export default class MainPageService {
   
    static async get8Directories(): /* Promise<AxiosResponse<Directories8Response[]>> */ Promise<IDirectoryItem[]>{
        return directoriesList;
        return $api.get('/book/last-published-books')
    }

       
    static async getLastBooks(): Promise<AxiosResponse<GetLastBooksRes[]>> {
        return $api.get('/book/last-published-books')
    }
}

