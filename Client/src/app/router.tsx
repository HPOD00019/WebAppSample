import { createBrowserRouter } from 'react-router-dom';
import {RegisterPage} from '@pages/Auth/RegisterPage';
import { LoginPage } from '@pages/Auth';



export const router = createBrowserRouter([
    {
        path: '/register', 
        element: <RegisterPage />
    },
    {
        path: '/login', 
        element: <LoginPage />
    }
]);