import React from 'react';
import styles from './FlatPanelButton.module.css';

interface FlatPanelButtonProps {
    /** Размер кнопки */
    size?: 'small' | 'medium' | 'large';
    /** Вариант кнопки */
    variant?: 'default' | 'primary' | 'success' | 'danger';
    /** Текст на кнопке */
    children: React.ReactNode;
    /** Описание под текстом */
    description?: string;
    /** Отключена ли кнопка */
    disabled?: boolean;
    /** Активное/выбранное состояние */
    active?: boolean;
    /** Обработчик клика */
    onClick?: (e: React.MouseEvent<HTMLButtonElement>) => void;
    /** Иконка слева (можно использовать react-icons) */
    icon?: React.ReactNode;
    /** Бейдж справа (число, текст) */
    badge?: string | number;
    /** Компактный режим (без закруглений между кнопками) */
    compact?: boolean;
    /** Дополнительные классы */
    className?: string;
    /** Тип HTML-кнопки */
    type?: 'button' | 'submit' | 'reset';
    /** Дополнительные inline стили */
    style?: React.CSSProperties;
}

export const FlatPanelButton: React.FC<FlatPanelButtonProps> = ({
    size = 'medium',
    variant = 'default',
    children,
    description,
    disabled = false,
    active = false,
    onClick,
    icon,
    badge,
    compact = false,
    className = '',
    type = 'button',
    style,
}) => {
    const buttonClasses = [
        styles.flatButton,
        styles[size],
        styles[variant],
        active ? styles.active : '',
        compact ? styles.compact : '',
        className,
    ]
        .filter(Boolean)
        .join(' ');

    return (
        <button
            type={type}
            className={buttonClasses}
            disabled={disabled}
            onClick={onClick}
            style={style}
            aria-pressed={active}
            aria-disabled={disabled}
        >
            {icon && <span className={styles.icon}>{icon}</span>}
            
            <div style={{ flex: 1 }}>
                <div>{children}</div>
                {description && (
                    <div className={styles.description}>{description}</div>
                )}
            </div>
            
            {badge !== undefined && (
                <span className={styles.badge}>
                    {badge}
                </span>
            )}
        </button>
    );
};

// Хук для управления состоянием активной кнопки (опционально)
export const useFlatButtonGroup = (initialIndex?: number) => {
    const [activeIndex, setActiveIndex] = React.useState<number | null>(
        initialIndex ?? null
    );

    const handleButtonClick = (index: number, onClick?: () => void) => {
        setActiveIndex(index);
        if (onClick) onClick();
    };

    return {
        activeIndex,
        setActiveIndex,
        handleButtonClick,
    };
};