import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";

interface ILoginDto{

    email:string,
    password: string
}

export default class LoginService {
    static async login(data:ILoginDto) :Promise<AxiosResponse>{
        return await $api.post('login',data)
    }
}