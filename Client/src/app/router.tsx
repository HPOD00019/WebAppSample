import { createBrowserRouter } from 'react-router-dom';
import { RegisterPage } from '@pages/Auth/RegisterPage';
import { LoginPage } from '@pages/Auth';
import { GamePage } from '@pages/gameRoom/GamePage';
import { MatchSearchPage } from '@pages/matchSearch/MatchSearchPage';


export const router = createBrowserRouter([
    {
        path: '/register', 
        element: <RegisterPage />
    },
    {
        path: '/login', 
        element: <LoginPage />
    },
    {
        path: '/game', 
        element: <GamePage />
    },
    
    {
        path: '/search', 
        element: <MatchSearchPage />
    }
]);