import { createBrowserRouter } from 'react-router-dom';
import { RegisterPage } from '@pages/Auth/RegisterPage';
import { LoginPage } from '@pages/Auth';
import { GamePage } from '@pages/gameRoom/GamePage';
import { MatchSearchPage } from '@pages/matchSearch/MatchSearchPage';
import { AnalisysPage } from '@pages/analisysPage/analisysPage';
import { BotPage } from '@pages/BotPage/BotPage';


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
    },
    {
        path: '/analisys',
        element: <AnalisysPage />
    },
    {
        path: '/botPage',
        element: <BotPage/>
    }
]);