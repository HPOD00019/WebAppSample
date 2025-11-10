import type {Meta, StoryObj } from '@storybook/react';

import {LoginPage } from './LoginPage.tsx';

const meta = {
    title : 'Components/LoginPage',
    component : LoginPage,
} satisfies Meta<typeof LoginPage>;

export default meta;

type Story = StoryObj<typeof LoginPage>

export const Default : Story =  {
    args : {
        
    }
}


