import { useState, useEffect } from 'react';
import { authApi } from '../api/auth-api';
import type { LoginCredentials, AuthState} from '../types/auth.types'
import { storeRefreshToken } from '@shared/utils/storage';


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
        console.log(ans);
        if(ans.success === true){
            const response = ans.data;
            const str = JSON.stringify(response).slice(1, -1);
            
            console.log(str);
            if (response){
                storeRefreshToken(str);
            }
        }
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