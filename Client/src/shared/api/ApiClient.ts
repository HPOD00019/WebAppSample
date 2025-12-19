import axios from 'axios'
import { authInterceptor } from './interceptors/request/authInterceptor';
import { authResponseInterceptor } from './interceptors/response/authResponseInterceptor';


const BASE_URL = import.meta.env.VITE_API_URL;

export const apiClient = axios.create({
    baseURL : BASE_URL,
    timeout : 100000,
    headers: {
        'Content-Type' : 'application/json',
    },
});

apiClient.interceptors.request.use(
  authInterceptor,
  error => Promise.reject(error)
);
apiClient.interceptors.response.use(
  response => response,
  authResponseInterceptor
)