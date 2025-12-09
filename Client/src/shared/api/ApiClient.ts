import axios from 'axios'
import { authInterceptor } from './interceptors/authInterceptor';


const BASE_URL = import.meta.env.VITE_API_URL;

export const apiClient = axios.create({
    baseURL : BASE_URL,
    timeout : 10000,
    headers: {
        'Content-Type' : 'application/json',
    },

});

apiClient.interceptors.request.use(
  authInterceptor,
  error => Promise.reject(error)
)