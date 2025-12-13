import type { BaseError } from "@shared/lib/error";


export interface User{
    userName : string;
    email : string;
    avatar? : string;
}

export interface LoginCredentials{
    username : string;
    password : string;
}
export interface RegisterCredentials{
    email: string;
    password: string;
    username: string;
}
export interface AuthResponse{
    date: string;
    success: boolean;
    data?: object;
    errorCode?: string;
}

export interface AuthState{
    isAuthenticated : boolean;
    isLoading : boolean;
    error : BaseError | null;
}

export interface RegisterResponse{
    user: User;
    refreshToken: string;
    accessToken: string;
}