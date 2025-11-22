import { useState } from 'react';
import '../styles/square.css';

export interface SquareProps extends React.HTMLAttributes<HTMLDivElement>{
    state: 'selected-square' | 'last-move-square' | 'move-destination-square';
    squareId: number;

    onMouseEnter? : () => void;
    onMouseExit? : () => void;
    
    onMouseClick? : () => void;
}

export const Square = (props : SquareProps) => {

    const [isHovered, setHover] = useState(false);
    if(props.onMouseEnter == null && props.onMouseExit == null){
        
        const handleMouseEnter = () => setHover(true);
        const handleMouseLeave = () => setHover(false);

        const squareClassName = `square ${props.state} ${isHovered? 'square-hovered': ''}`
        return(
            <div 
                className={squareClassName} 
                style= {props.style}
                onMouseEnter = {handleMouseEnter}
                onMouseDown = {props.onMouseClick}
                onMouseLeave={handleMouseLeave}
            />
        )
    }

    const { state } = props;
    const squareClassName = `square ${state}`;

    return(
        <div 
            className={squareClassName} 
            style= {props.style}
            onMouseEnter = {props.onMouseEnter}
            onMouseDown = {props.onMouseClick}
            onMouseLeave={props.onMouseExit}
        />
    )
}