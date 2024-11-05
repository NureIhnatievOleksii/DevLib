import { AxiosResponse } from "axios";
import $api from "../../../app/api/http";
import { IBookDetails } from "../../../models/IBookDetails";

export default class BookService {
    static async getBookDetails(bookId: string): Promise<IBookDetails> {
        const response: AxiosResponse<IBookDetails> = await $api.get(`book/get-book/${bookId}`);
        console.log(response.data);
        return response.data;
         
    }
}

