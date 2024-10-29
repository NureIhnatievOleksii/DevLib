import { AxiosResponse } from "axios";
import $api from "../http";

interface LoginResponse {
    token: string
}

interface Article {
    article_id: string;
    name: string;
  }
  
  interface DirectoryResponse {
    directory_id: string;
    directory_name: string;
    articles: Article[];
  }

export default class AppService {
    static async login(email: string, password: string): Promise<AxiosResponse<LoginResponse>> {
        return $api.post('/auth/login', {
            email,
            password
        })
    }

 /*    static async getDirectory(directoryId: string): Promise<AxiosResponse<DirectoryResponse>> {
        return $api.get(`/get_directory/${directoryId}`);
    } */
}

