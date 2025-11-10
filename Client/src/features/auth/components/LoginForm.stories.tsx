import type {Meta, StoryObj } from '@storybook/react';

import {LoginForm } from './LoginForm.tsx';

const meta = {
    title : 'Components/LoginForm',
    component : LoginForm,
} satisfies Meta<typeof LoginForm>;

export default meta;

type Story = StoryObj<typeof LoginForm>

export const Default : Story =  {
    args : {
        
    }
}


