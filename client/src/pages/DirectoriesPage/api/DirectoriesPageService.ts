import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";
import directoriesList from '../../../api/directories/list.json';
import { IDirectoryItem } from "../../../app/models/IDirectoryItem";

export default class DirectoriesPageService {
    static async getDirectories(): Promise<IDirectoryItem[]> {
        // Якщо JSON:
        return directoriesList;

        // Якщо дані з API, то замість рядка вище наступний рядок:
        //return $api.get('/api/directories').then(response => response.data);
    }
}
