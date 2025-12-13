import { getAccessToken } from "@shared/utils/storage";
import type { InternalAxiosRequestConfig } from "axios";

export const authInterceptor = (config: InternalAxiosRequestConfig) => {
    const token = getAccessToken();
    config.headers.Authorization = `Bearer ${token}`;
    return config;
}