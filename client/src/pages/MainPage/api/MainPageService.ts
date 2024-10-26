import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";
import directoriesList from '../../../api/directories/list.json'
import booksList from '../../../api/books/list.json'

import { IDirectoryItem } from "../../../app/models/IDirectoryItem";
import { IBookItem } from "../../../app/models/IBookItem";

export default class MainPageService {
   
    static async get8Directories(): /* Promise<AxiosResponse<Directories8Response[]>> */ Promise<IDirectoryItem[]>{
        return directoriesList;
       /*  $api.get('/get-8-directories') */
    }

       
    static async getLastBooks(): /* Promise<AxiosResponse<Directories8Response[]>> */ Promise<IBookItem[]>{
        return booksList;
       /*  $api.get('/get-8-directories') */
    }
}

