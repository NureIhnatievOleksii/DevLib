import React, { FC, ReactNode, Suspense, useEffect, useState } from 'react'
import { Navigate, Route, Routes } from 'react-router-dom';
import { RouteNames, publicRoutes } from '../../router';
import Loader from '../../../UI/Loader/Loader';
import { useAuthStore } from '../../store/auth';
interface AuthGuardProps {
    children: ReactNode
}
const AuthGuard: FC<AuthGuardProps> = (props) => {
    const isAuth = useAuthStore(state => state.loggedIn);
    const isLoading = useAuthStore(state => state.isLoading);
    const role = useAuthStore(state => state.role);
    const setIsLoadingAuth = useAuthStore(state => state.setIsLoading);
   /*  const checkAuth = useAuthStore(state => state.checkAuth); */
    const setLoggedIn = useAuthStore(state => state.setLoggedIn)
    const setRole = useAuthStore(state => state.setRole)

    useEffect(() => {
        setLoggedIn(true);
        setRole('admin')
        setIsLoadingAuth(false)
    }, [])


    return (
        <>
            {((role === 'admin' || role === 'user')) ?
                <>
                    {props.children}
                    
                </>
                :
                
                (!isLoading && (
                    <>
                    
                        <Routes>
                            {publicRoutes.map((route) => (
                                <Route
                                    key={route.path}
                                    path={route.path}
                                    element={<Suspense fallback={<Loader />}><route.element /></Suspense>}
                                />
                            ))}
                            <Route path="*" element={<Navigate to={RouteNames.LOGIN} replace />} />
                        </Routes>
                    </>
                ))
            }
        </>
    );
}


export default AuthGuard
