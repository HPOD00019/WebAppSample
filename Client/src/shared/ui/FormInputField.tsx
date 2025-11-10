import React from 'react';
import './formInputField.css';

export interface FormInputFieldProps extends React.InputHTMLAttributes<HTMLInputElement> {
    sizeMark? : 'large' | 'medium' | 'small';
    ClassNames?: string;
}

export const Input = (props: FormInputFieldProps) => {
    const {sizeMark = 'medium', ClassNames, ...rest} = props;
    const classes = `input--${sizeMark} ${ClassNames}`;

    return(
        <input className= {classes}
            {...rest}
        />
    )
};