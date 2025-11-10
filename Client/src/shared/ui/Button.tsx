import React from 'react'
import './button.css'


export interface ButtonProps{
    children : React.ReactNode;
    btnStyle : 'primary' | 'secondary' | 'danger';
    size? : 'large' | 'medium' | 'small';
    disabled? : boolean;
    onClick? : () => void;
    btnType? : 'button' | 'submit' | 'reset';
    className? : string;
}

export const Button : React.FC<ButtonProps> = ({
    children,
    btnStyle = 'primary',
    size = 'medium',
    disabled = false,
    btnType = 'button',
    onClick,
    className = '',

}) => {
    const buttonClass = `btn btn-${btnStyle} btn-${size} ${className}`.trim();

  return (
    <button
      type={btnType}
      className={buttonClass}
      disabled={disabled}
      onClick={onClick}
    >
      {children}
    </button>
  );
};