import { apiClient } from "@shared/api/ApiClient";
import type { AccessTokenResponse } from "@shared/types/api";
import { getRefreshToken, storeAccessToken } from "@shared/utils/storage";
import type { AxiosError } from "axios";

export const authResponseInterceptor = async (error: AxiosError) => {
    const originalRequest = error.config;

    if (!originalRequest || error.response?.status !== 401) {
        return Promise.reject(error);
    }
    
    if(error.response?.status === 401){

        const refreshToken = getRefreshToken();
        console.log(refreshToken);
        const url = `http://localhost:5001/Auth/RefreshAccessToken?refreshToken=${refreshToken}`;
        console.log(url);
        const response = await apiClient.get<AccessTokenResponse>(url);
        if(response.data.success === false) console.log(response.data.errorCode);
        const newAccessToken = response.data.data;
        storeAccessToken(newAccessToken);
        originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
        return apiClient(originalRequest);
    }
}