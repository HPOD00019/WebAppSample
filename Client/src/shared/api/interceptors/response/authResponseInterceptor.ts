import { apiClient } from "@shared/api/ApiClient";
import type { AccessTokenResponse } from "@shared/types/api";
import { storeAccessToken } from "@shared/utils/storage";
import type { AxiosError } from "axios";

export const authResponseInterceptor = async (error: AxiosError) => {
    const originalRequest = error.config;

    if (!originalRequest || error.response?.status !== 401) {
        return Promise.reject(error);
    }
    
    if(error.response?.status === 401){
        const response = await apiClient.get<AccessTokenResponse>('http://localhost:5001/Auth/RefreshAccessToken', {
            withCredentials: true
        });
        if(response.data.success === false) console.log(response.data.errorCode);
        const newAccessToken = response.data.data;
        storeAccessToken(newAccessToken);
        originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
        return apiClient(originalRequest);
    }
}