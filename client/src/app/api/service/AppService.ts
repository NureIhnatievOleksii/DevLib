import { AxiosResponse } from "axios";
import $api from "../http";

interface LoginResponse {
    accessToken: string
}
export default class AppService {
    static async login(username: string, password: string): Promise<AxiosResponse<LoginResponse>> {
        return $api.post('/user/login', {
            username,
            password
        })
    }
}

