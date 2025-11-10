import { useState, useEffect } from 'react';
import { authApi } from '../api/auth-api';
import type { LoginCredentials, AuthState, User, AuthResponse } from '../types/auth.types'


export const useAuth = () => {
    const [state, setState] = useState<AuthState>({
        user : null,
        isAuthenticated : false,
        isLoading : true,
        error : null,
    });

    useEffect (() => {
        checkAuth();
    }, []);

    const checkAuth = async () => {
        const token = localStorage.getItem('accessToken');
        if (!token){
            setState(prev => ({ ...prev, isLoading: false}));
            return;
        }
    }

    const login = async (credentials: LoginCredentials) => {
        setState((prev) => ({...prev, isLoading : true, error : null }));
        const {user, refreshToken} = await authApi.login(credentials);
        setState({
            user,
            isLoading: false,
            isAuthenticated: true,
            error: null,
        })
        return {success : true };
    }
    return {
        ...state,
        login,
    }
}