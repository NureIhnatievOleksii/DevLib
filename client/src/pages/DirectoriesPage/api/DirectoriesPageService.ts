import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";
//import directoriesList from '../../../api/directories/list.json';
//import { IDirectoryItem } from "../../../app/models/IDirectoryItem";

interface Directory {
    DirectoryId: number;
    DirectoryName: string;
    ImgLink: string;
}

export default class DirectoriesPageService {
    static async getDirectories(directoryName: string = ""): Promise<Directory[]> {
        const response: AxiosResponse<Directory[]> = await $api.get(`directory/search-directories/${directoryName}`);
        return response.data;
    }
}
