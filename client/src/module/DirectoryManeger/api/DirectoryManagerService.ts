import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";


interface addDirectoryDto {
    DirectoryName: string, File: File
}
interface addArticleDto {
    articleName: string,
    articleContent: string,
    directoryId: string
}

export default class DirectoryManagerService {
    static async addDirectory(dto: addDirectoryDto): Promise<AxiosResponse<string>> {
        const formData = new FormData();
        formData.append('DirectoryName', dto.DirectoryName) 
        formData.append('File', dto.File) 

        return $api.post('directory/add-directory', formData)
    }
    static async addArticle(dto: addArticleDto): Promise<AxiosResponse> {
        return $api.post('directory/add-article', dto)
    }
}