import { apiClient} from '@shared/api/ApiClient'; 
import type {AuthResponse, LoginCredentials, RegisterCredentials}  from '../types/auth.types';

export const authApi = {
    register: async (credentials: RegisterCredentials) => {
        const response = await apiClient.post<AuthResponse>('', credentials);
        return response.data;
    },

    login: async (credentials: LoginCredentials) => {
        const response = await apiClient.post<AuthResponse>('', credentials);
        return response.data;
    }
}
