import $api from "../../../app/api/http";
import { AxiosResponse } from "axios";

interface ArticleData {
    ArticleId: string;
    ChapterName: string;
    Text: string;
}

interface ChapterData {
    ChapterId: string;
    Name: string;
}

interface DirectoryData {
    DirectoryId: string;
    DirectoryName: string;
    Articles: { ArticleId: string; ChapterName: string }[];
}

export default class ArticlePageService {
    static async getArticle(articleId: string): Promise<ArticleData> {
        const response: AxiosResponse<ArticleData> = await $api.get(`/directory/get-article/${articleId}`);
        return response.data;
    }

    static async getChapters(directoryId: string): Promise<ChapterData[]> {
        const response: AxiosResponse<ChapterData[]> = await $api.get(`/directory/get-all-chapter-name/${directoryId}`);
        return response.data;
    }

    static async getDirectory(directoryId: string): Promise<DirectoryData> {
        const response: AxiosResponse<DirectoryData> = await $api.get(`/directory/get-directory/${directoryId}`);
        return response.data;
    }
}
