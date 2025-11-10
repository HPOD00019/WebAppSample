import axios from 'axios'

const BASE_URL = import.meta.env.VITE_API_URL;

export const apiClient = axios.create({
    baseURL : BASE_URL,
    timeout : 10000,
    headers: {
        'Content-Type' : 'apllication/json',
    },
});

apiClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('authToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);