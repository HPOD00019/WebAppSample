import React, {useState} from 'react';
import {Button} from '@shared/ui/index';
import {useAuth} from '../hooks/useAuth';
import type {LoginCredentials} from '../types/auth.types';
import { Input } from '@shared/ui/index';

export interface LoginFormProps{
    onSuccess? : () => void;
}

export const LoginForm : React.FC<LoginFormProps> = ({onSuccess}) => {
    const [credentials, setCredentials] = useState<LoginCredentials>({
        password: '',
        email : '',
    });
    
    const {login, isLoading,} = useAuth();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const result = await login(credentials);

        if(result.success){
            onSuccess?.();
        }
    }

    const handleChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        setCredentials(prev => ({
            ...prev,
            [e.target.name] : e.target.value,
        }));
    }
    return (
        <form className='login-form space-ver-lg' onSubmit={handleSubmit}>
            <h2>Login</h2>
            <div id='input-components' className='space-ver-xs' >
                <div className='input-group'>
                    <Input
                    placeholder='Email'
                    type='email'
                    name='email'
                    id='email'
                    disabled={isLoading}
                    value={credentials.email}
                    onChange={handleChange}
                    
                    required
                    />
                </div>

                <div className='input-group'>
                    <Input
                    placeholder='Password'
                    type='password'
                    name='password'
                    id='password'
                    disabled={isLoading}
                    value={credentials.password}
                    onChange={handleChange}

                    required
                    />

                </div>
            </div>
            

            <Button
            btnType='submit'
            btnStyle='primary'
            >
                {isLoading? 'Wait...' : 'Login'}
            </Button>
        </form>
    )
}