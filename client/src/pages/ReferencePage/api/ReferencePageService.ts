import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";
import { IArticleItem } from "../../../app/models/IArticleItem";
import { IDirectoryItem } from "../../../app/models/IDirectoryItem";

export default class ReferencePageService {
    
    static async getDirectory(directoryId: string): Promise<AxiosResponse<IDirectoryItem>> {
        return $api.get(`/directory/get-directory/${directoryId}`);
    }

    static async getAllChapterNames(directory_id: string): Promise<AxiosResponse<IArticleItem[]>> {
        return $api.get(`/directory/get-all-chapter-name/${directory_id}`);
    }
}