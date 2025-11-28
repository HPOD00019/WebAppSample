import { RegisterForm } from "@features/auth/components/RegisterForm";
import { AuthLayout } from "./components/AuthLayout";

export interface RegisterPageProps{
    onSuccess?: () => void;
}

export const RegisterPage = (props: RegisterPageProps) => {
    if(props.onSuccess != null) console.log("444");
    return(
        <AuthLayout
            title = "Welcome!"
            footerLinkText="Already have an account? Login..."
        >
            <RegisterForm/>
        </AuthLayout>
    )
}


