import { apiClient } from "@shared/api/ApiClient";
import { useState } from "react";
import type { AuthState, RegisterCredentials, RegisterResponse } from "../types/auth.types";

import { ErrorCode, type BaseError } from "@shared/lib/error";
import axios from "axios";

export const useRegister = () => {
    const [registerRequestState, changeRequestState] = useState<AuthState>({
        isAuthenticated: false,
        isLoading: false,
        error: null,
    });

    const SendRegisterRequest = async (cred: RegisterCredentials) => {
        changeRequestState((prev) => ({
            ...prev, 
            isLoading: true,
        }));
        try{
            const response = await apiClient.post<RegisterResponse>("my_adress", cred);
            changeRequestState((prev) => ({
                ...prev,
                user: response.data.user,
                isAuthenticated: true,
                isLoading: false,
            })); 
        }
        catch(r_error){
            if(axios.isAxiosError(r_error)){
                const status = r_error.status;
                let ResponseError : BaseError;
                switch(status){
                    case ErrorCode.USER_EXISTS: 
                        ResponseError = {
                            code: ErrorCode.USER_EXISTS,
                            message: r_error.message,
                        };
                }
                
                changeRequestState((prev) => ({
                    ...prev,
                    isLoading: false,
                    error: ResponseError,
                }));
            }   
        }
        
    }

    return {
        registerRequestState, 
        SendRegisterRequest
    }
}