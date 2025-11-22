import { RegisterForm } from "@features/auth/components/RegisterForm";
import { AuthLayout } from "./components/AuthLayout";
import MyComponent from './components/SocketTestComponent';

export interface RegisterPageProps{
    onSuccess?: () => void;
}

export const RegisterPage = (props: RegisterPageProps) => {
    return(
        <AuthLayout
            title = "Welcome!"
            footerLinkText="Already have an account? Login..."
        >
            <RegisterForm/>



            <MyComponent/>




        </AuthLayout>
    )
}


