import React, { useState } from 'react';
import styles from './Button.module.css';

interface ButtonProps {
    /** Размер кнопки */
    size?: 'small' | 'medium' | 'large';
    /** Вариант кнопки */
    variant?: 'default' | 'success' | 'danger' | 'outline';
    /** Текст на кнопке */
    children: React.ReactNode;
    /** Отключена ли кнопка */
    disabled?: boolean;
    /** Обработчик клика */
    onClick?: () => void;
    /** Иконка слева (например, из react-icons) */
    icon?: React.ReactNode;
    /** Дополнительные классы */
    className?: string;
    /** Тип HTML-кнопки */
    type?: 'button' | 'submit' | 'reset';
    /** Показать анимацию нажатия */
    withPressAnimation?: boolean;
    /** Дополнительные inline стили */
    style?: React.CSSProperties;
}

export const Button = ({
    size = 'medium',
    variant = 'default',
    children,
    disabled = false,
    onClick,
    icon,
    className = '',
    type = 'button',
    withPressAnimation = false,
    style,
}: ButtonProps) => {
    const [isAnimating, setIsAnimating] = useState(false);

    const handleClick = () => {
        if (disabled) return;
        
        if (withPressAnimation) {
            setIsAnimating(true);
            setTimeout(() => setIsAnimating(false), 300);
        }
        
        if (onClick) {
            onClick();
        }
    };

    const buttonClasses = [
        styles.button3d,
        styles[size],
        styles[variant],
        isAnimating ? styles.pressAnimation : '',
        className,
    ]
        .filter(Boolean)
        .join(' ');

    return (
        <button
            type={type}
            className={buttonClasses}
            disabled={disabled}
            onClick={() => handleClick()}
            style={style}
            aria-disabled={disabled}
        >
            {icon && <span className={styles.icon}>{icon}</span>}
            {children}
        </button>
    );
};

// Пример иконок для быстрого использования
export const Button3DIcons = {
    Download: () => <i className="fas fa-download" />,
    Send: () => <i className="fas fa-paper-plane" />,
    Delete: () => <i className="fas fa-trash" />,
    Save: () => <i className="fas fa-save" />,
    Play: () => <i className="fas fa-play" />,
    Stop: () => <i className="fas fa-stop" />,
    Plus: () => <i className="fas fa-plus" />,
    Minus: () => <i className="fas fa-minus" />,
    Check: () => <i className="fas fa-check" />,
    Close: () => <i className="fas fa-times" />,
};