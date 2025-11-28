
export interface User{
    userId : string;
    userName : string;
    email : string;
    avatar? : string;
}

export interface LoginCredentials{
    email : string;
    password : string;
}
export interface RegisterCredentials{
    email: string;
    password: string;
    username: string;
}
export interface AuthResponse{
    user : User;
    refreshToken : string;
}

export interface AuthState{
    user : User | null;
    isAuthenticated : boolean;
    isLoading : boolean;
    error : string | null;
}

export interface RegisterResponse{
    user: User;
    refreshToken: string;
    accessToken: string;
}