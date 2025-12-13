import { useState, useEffect } from 'react';
import { authApi } from '../api/auth-api';
import type { LoginCredentials, AuthState} from '../types/auth.types'


export const useAuth = () => {
    const [state, setState] = useState<AuthState>({
        isAuthenticated : false,
        isLoading : false,
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
        const ans = await authApi.login(credentials);
        console.log(`AuthAnswer ${ans.success}`);
        setState({
            isLoading: false,
            isAuthenticated: true,
            error: null,
        });
        return {success : true };
    }
    return {
        ...state,
        login,
    }
}