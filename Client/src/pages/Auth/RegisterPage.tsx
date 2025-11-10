import { RegisterForm } from "@features/auth/components/RegisterForm";
import { AuthLayout } from "./components/AuthLayout";


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
        </AuthLayout>
    )
}