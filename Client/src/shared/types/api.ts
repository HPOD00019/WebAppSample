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

export type LoadingState = 'loading' | 'succeeded' | 'failed'