import path from 'path';
import React from 'react';
import LoginPage from '../../pages/LoginPage/LoginPage';
import MainPage from '../../pages/MainPage/MainPage';
import BookMarksPage from '../../pages/BookMarksPage/BookMarksPage';
import AddRecord from '../../pages/AddRecord/AddRecord';
import Forum from '../../pages/Forum/Forum';
import UsersAdmin from '../../pages/UsersAdmin/UsersAdmin';
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
  USER_ACCOUNT = "/user-account",
  ADMIN_ACCOUNT = "/admin-account", //note есть ли у нас вообще аккаунт админа?
  ADD_RECORD = '/add-record',
  USERS_ADMIN = '/users',
}

export const adminRoutes: IRoute[] = [
/*   { path: RouteNames.ADMIN, element: AdminPage }, */
  { path: RouteNames.ADD_RECORD, element: AddRecord },
  { path: RouteNames.USERS_ADMIN, element: UsersAdmin },
]

export const userRoutes: IRoute[] = [
  { path: RouteNames.BOOK_MARKS, element: BookMarksPage },
]

export const publicRoutes: IRoute[] = [
  { path: RouteNames.LOGIN, element: LoginPage },
  { path: RouteNames.MAIN, element: MainPage },
  { path: RouteNames.FORUM, element: Forum }
]