import type { RegisterCredentials } from "../types/auth.types";
import type {FormInputFieldProps} from "@shared/ui/index";
import { Button, Input } from "@shared/ui/index";
import { useEffect, useState } from "react";
import { useRegister } from "../hooks/useRegister";


export interface RegisterFormProps{
    onSuccess? : () => void;
}

export const RegisterForm = (props: RegisterFormProps) => {
    const [registerProperties, setRegisterProperties] = useState<RegisterCredentials>({
        email: '',
        password: '',
        username: '',
    });

    const {registerRequestState, SendRegisterRequest} = useRegister();

    const confirmHandler = () => {
        SendRegisterRequest(registerProperties);
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setRegisterProperties(prev => ({
            ...prev,
            [e.target.name] : e.target.value,
        }));
    }
    return(
        <form className="register-form" onSubmit={confirmHandler}>
            <Input 
                id="register-form__username-input"
                sizeMark="medium" 
                name="usename"
                placeholder="Username"
                onChange={handleChange}
                />
            <Input
                id="register-form__email-input"
                sizeMark="medium"
                name="email"
                placeholder="Email"
                onChange={handleChange}
                />
            <Input 
                type="password"
                id="register-form__password-input"
                sizeMark="medium"
                name="password"
                placeholder="Password"
                onChange={handleChange}
                />
            <button
                disabled={registerRequestState.isLoading}
                id="register-form__confirm"
                className="register-form__confirm"
                type="submit"
            >
                {registerRequestState.isLoading? "Wait..." : "Confirm"}
            </button>

        </form>
    );
}