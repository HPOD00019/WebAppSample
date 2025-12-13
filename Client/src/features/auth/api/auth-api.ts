import { apiClient} from '@shared/api/ApiClient'; 
import type {AuthResponse, LoginCredentials, RegisterCredentials}  from '../types/auth.types';

export const authApi = {
    register: async (credentials: RegisterCredentials) => {
        const response = await apiClient.post<AuthResponse>('http://localhost:5001/Auth/Register', credentials);
        return response.data;
    },

    login: async (credentials: LoginCredentials) => {
        const response = await apiClient.post<AuthResponse>('http://localhost:5001/Auth/Login', credentials, {
        });
        console.log(response);
        return response.data;
    }
}
