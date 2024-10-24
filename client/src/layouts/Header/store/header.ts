import create from 'zustand';
import $api from '../../../app/api/http';

export type THeaderVersion = 'normal' | 'small' | 'minimized';

interface HeaderState {
    value: string,
    headerVersion: THeaderVersion; //note версия шапки - маленькая или большая
    requestUrl: string, //note url для запроса, который отправляется при нажатии кнопки поиска в инпуте  
    response: any[], //note ответ который приходит от сервера после отработки запроса поска 
    filerValue: string,
    setValue: (value: string) => void;
    getData: () => void,
    setFilterValue: (value: string) => void,
    setHeaderVersion: (value: THeaderVersion) => void

}

export const useHeaderStore = create<HeaderState>((set, get) => ({
    value: '',
    headerVersion: 'minimized',
    filerValue: 'Книги',
    requestUrl: '',
    response: [],
    setValue: (value: string) => {
        set({ value })
    },

    getData: async () => {
        const requestUrl = get().requestUrl;
        const { data } = await $api.get(requestUrl);
        set({
            response: data
        })
    },

    setFilterValue: (value: string) => {
        set({ filerValue: value })
    },
    setHeaderVersion: (value: THeaderVersion) => {
        set({ headerVersion: value })
    }
}));
