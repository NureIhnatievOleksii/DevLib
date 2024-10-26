import path from 'path';
import React from 'react';
import LoginPage from '../../pages/LoginPage/LoginPage';
import MainPage from '../../pages/MainPage/MainPage';
import BookMarksPage from '../../pages/BookMarksPage/BookMarksPage';
import AddRecord from '../../pages/AddRecord/AddRecord';
import Forum from '../../pages/Forum/Forum';
import UsersAdmin from '../../pages/UsersAdmin/UsersAdmin';
import BooksPage from '../../pages/BooksPage/BooksPage';
import DirectoriesPage from '../../pages/DirectoriesPage/DirectoriesPage';
import AccountPage from '../../pages/AccountPage/AccountPage';
import Register from '../../pages/Register/Register';
/* import LoginPage from '../../pages/LoginPage/LoginPage';
import SeatchRealEstate from '../../pages/SeatchRealEstate/SeatchRealEstate'; */



export interface IRoute {
  path: string;
  element: React.ComponentType;

}
export enum RouteNames {
  LOGIN = "/login",
  MAIN = "/",
  ADMIN = "/admin",
  BOOK_MARKS = "/book-marks",
  FORUM = "/forum",
  ADD_RECORD = '/add-record',
  USERS_ADMIN = '/users',
  BOOKS = "/books",
  DIRECTORIES = "/directories",
  ACCOUNT = "/account",
  REGISTER = "/register"
}

// маршруты только для админа 
export const adminRoutes: IRoute[] = [
  { path: RouteNames.ADD_RECORD, element: AddRecord },
  { path: RouteNames.USERS_ADMIN, element: UsersAdmin },
]

// маршруты только для пользователя 
export const userRoutes: IRoute[] = [
  { path: RouteNames.BOOK_MARKS, element: BookMarksPage },
  { path: RouteNames.ACCOUNT, element: AccountPage },
]

// маршруты для всех пользователей, включая как зарегистрированных, так и незарегистрированных  
export const publicRoutes: IRoute[] = [
  { path: RouteNames.LOGIN, element: LoginPage },
  { path: RouteNames.MAIN, element: MainPage },
  { path: RouteNames.FORUM, element: Forum },
  { path: RouteNames.BOOKS, element: BooksPage },
  { path: RouteNames.DIRECTORIES, element: DirectoriesPage },
  { path: RouteNames.REGISTER, element: Register },
]