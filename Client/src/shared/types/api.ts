export interface ApiResponse<T = unknown>{
    data : T;
    message? : string;
    status : number;
}

export interface ApiError{
    message : string;
    status : number;
    errors? : Record<string, string[]>;
}
export interface AccessTokenResponse{
    date: string;
    success: boolean;
    errorCode?: string;
    data: string;
}
export type LoadingState = 'loading' | 'succeeded' | 'failed'