import React from 'react';
import './AuthLayout.css';

interface AuthLayoutProps{
    title: string;
    subtitle? : string;
    children : React.ReactNode;
    footerText? : string;
    footerLinkText? : string;
    onFooterLinkTextClick? : () => void;
}


export const AuthLayout : React.FC<AuthLayoutProps> = ({
    title,
    subtitle,
    children,
    footerText,
    footerLinkText,
    onFooterLinkTextClick,
}) => {
    return(
        <div className='auth-layout'>
            <div className='auth-layout__container'>

                <div className='auth-layout__header'>
                    <h1 className='auth-layout__title'>
                        {title}
                    </h1>

                    {subtitle && <p className='auth-layout__subtitle'> {subtitle}</p>}
                </div>


                <div className='auth-layout__content'>
                    {children}
                </div>

                {(footerText || footerLinkText) && (
                    <div className='auth-layout__footer'>
                        {footerText && <span className='auth-layout__footer-text'> {footerText} </span>}
                        {footerLinkText && <button 
                            className='auth-layout__footer-text-link'
                            type='button'
                            onClick={onFooterLinkTextClick}
                        >
                            {footerLinkText}
                        </button> }
                    </div>
                )}

            </div>
        </div>
    );
};