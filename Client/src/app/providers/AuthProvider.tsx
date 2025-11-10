import React, {createContext, useContext} from 'react';
import {useAuth} from '@features/auth';
import type {User, LoginCredentials} from '@features/auth/index.ts';


interface AuthContextType{
    user: User | null;
    isLoading: boolean;
    isAuthenticated: boolean;
    login: (credentials: LoginCredentials) => Promise<{success: boolean}>;
    error: string | null;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider : React.FC<{children : React.ReactNode}> = ({ children }) => {
    const auth = useAuth();
    return(
        <AuthContext.Provider value = {auth} >
            {children}
        </AuthContext.Provider>
    );
};

export const UseAuthContext = () => {
    const context = useContext(AuthContext);
    if (context === undefined){
        throw new Error('Context is undefined');
    }
    return context;
};