import { IArticleItem } from "./article";


export interface IDirectory {
    directoryId: string;
    directoryName: string;
    articles: IArticleItem[]
}