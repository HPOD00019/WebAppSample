import React from 'react';
import { AuthLayout } from './components/AuthLayout';
import { LoginForm } from '@features/auth';
import type {LoginPageProps} from './types/login.types';
import '../LoginPage.css';


export const LoginPage : React.FC<LoginPageProps> = ({
    onSuccess,
}) => {
    const handleLoginSuccess = () => {
        onSuccess?.();
    }
    handleLoginSuccess();
    return (
        <AuthLayout 
            title='Walcome back!'
            subtitle='Please sign in to your account'
            footerLinkText='Sign up'
            
        >
            <div className='login-page__content'>
                <LoginForm/>
            </div>
        </AuthLayout>
    )
};
