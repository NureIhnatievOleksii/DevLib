import create from "zustand";
import DirectoryManagerService from "../api/DirectoryManagerService";

interface IArticle {
  articleName: string;
  articleContent: string;
/*   directoryId?: string; */
  articleId: string
}

interface DirectoryManagerState {
  articles: IArticle[];
  directoryName: string;
  file: File | null;
  directoryId: string;
  setArticles: (articles: IArticle[]) => void;
  setDirectoryName: (name: string) => void;
  setFile: (file: File | null) => void;
  setDirectoryId: (id: string) => void;
  addArticle: (article: IArticle) => void;
  removeArticle: (id:string) => void;

  addDirectory: () => void
}

const useDirectoryManagerStore = create<DirectoryManagerState>((set, get) => ({
  articles: [],
  directoryName: '',
  file: null,
  directoryId: '',
  
  addArticle: (article: IArticle) => set((state) => ({ articles: [...state.articles, article] })),

  setArticles: (articles) => set({articles}),
  setDirectoryName: (name) => set({ directoryName: name }),
  setFile: (file) => set({ file }),
  setDirectoryId: (id) => set({ directoryId: id }),
  removeArticle: (id: string) => {
    set((state) => ({
      articles: state.articles.filter((item) => item.articleId !== id)
    }));
  },
  addDirectory: async () => {
    const currentArticles = get().articles; 
    const file = get().file; 
    const directoryName = get().directoryName; 
    console.log(directoryName, file,currentArticles)
    try{
        const {data} = await DirectoryManagerService.addDirectory({DirectoryName : directoryName, File: file as File});
        currentArticles.forEach(async (article) => {
            await DirectoryManagerService.addArticle({
                articleContent: article.articleContent,
                articleName: article.articleName,
                directoryId: data
            })
        })
    }catch(e){
        console.log(e);
    }
  }

}));

export default useDirectoryManagerStore;
