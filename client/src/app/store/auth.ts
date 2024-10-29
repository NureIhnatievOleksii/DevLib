import create from 'zustand';
import {devtools} from 'zustand/middleware';

import axios from 'axios';
import AppService from '../api/service/AppService';
import { jwtDecode } from 'jwt-decode';
import $api from '../api/http';




interface BearState {
  loggedIn: boolean;
  role: string;
  isLoading: boolean;
  setLoggedIn: (value: boolean) => void;
  setRole: (role: string) => void;
  setIsLoading: (value: boolean) => void;
  login: (email: string, password: string) => Promise<void>;
  checkAuth: () => Promise<void>;
  logout: () => void
}
export const useAuthStore = create<BearState>()(
  devtools((set) => ({
    loggedIn: false,
    setLoggedIn: (value: boolean) => set(() => ({ loggedIn: value })),
    role: '',
    setRole: (role: string) => set(() => ({ role })),
    isLoading: true,
    setIsLoading: (value: boolean) => set(() => ({ isLoading: value })),

    login: async (email: string, password: string) => {
      set({ isLoading: true });
      try {
        const { data } = await AppService.login(email, password);
        const decodedToken: any = jwtDecode(data.token);
        set({
          loggedIn: true,
          role: (decodedToken.role == 'Client') ? 'user' : decodedToken.role,
          isLoading: false,
        });

        localStorage.setItem('token', data.token);
      } catch (error: any) {
        console.error(error);
        set({ isLoading: false });
      }
    },
    checkAuth: async () => {
      set({ isLoading: true });
      try {
     

        const decodedToken: any = jwtDecode(localStorage.getItem('token') as string);
        console.log(decodedToken)
        set({
          loggedIn: true,
          role: decodedToken.role,
          isLoading: false,
        });
      } catch (error: any) {
        console.error(error);
      } finally {
        set({ isLoading: false });
      }
    },
    logout: () => {
      set({ loggedIn: false, role: '', isLoading: false });
      /*  localStorage.removeItem('token'); */
      /* $api.post('/logout', {}, { withCredentials: true })
        .then(() => {
          console.log('Successfully logged out');
        })
        .catch((error: any) => {
          console.error('Logout error', error);
        }); */
    }
  }))
);
