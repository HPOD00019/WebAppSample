import { useEffect, useRef, useState } from "react";
import { StyleOption } from "./vector-option";
import type { boardStyle } from "@features/chessboard/types/chessboard.types";
import { Button } from "@shared/ui";

export interface stylesListProps extends React.HTMLAttributes<HTMLDivElement> {
    onStyleClicked: (choosedStyle: boardStyle) => void;
}
export const StylesList = (props: stylesListProps) => {
    const [isOpen, changeOpenState] = useState(false);
    const dropdownRef = useRef<HTMLDivElement>(null);
    console.log(isOpen);
    const classes = `${props.className} styles-list`;
    useEffect(() => {
        function handleClickOutside(event: globalThis.MouseEvent) {
            console.log(dropdownRef.current);
            console.log(event.target as Node);
            if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
                changeOpenState(false);
            }
        }
        document.addEventListener('mousedown', handleClickOutside);
        return () => 
            {
                document.removeEventListener('mousedown', handleClickOutside);
            };
    }, [isOpen]);

    const stylesButtonClickedHandler = () => {
        changeOpenState(true);

    }
    return(
        <div ref={dropdownRef} style={{paddingLeft:'15px', position: 'relative', display: 'inline-block', height: 'fit-content'}} >
            <Button size="large" onClick={stylesButtonClickedHandler}> <span style={{justifyContent: 'center', alignItems: 'center', display: 'flex', flexDirection: 'row', whiteSpace: 'nowrap', margin: 'none', paddingTop: '0px'}}><p style={{margin: '0px', padding: 'none', fontSize: '1.5em'}}>{'\u2655'}  </p> Change set</span>  </Button>
        {isOpen && <div className={classes} style={{position: 'absolute',
          top: '80px',
          left: 15,
          marginTop: '8px',
          backgroundColor: 'white',
          borderRadius: '15px',
          boxShadow: '0 2px 10px rgba(0,0,0,0.1)',
          minWidth: '200px',
          zIndex: 1000}}>
            <StyleOption boardStyle="classical" onStyleClicked={() => props.onStyleClicked('classical')}/>
            <StyleOption boardStyle="vector" onStyleClicked={() => props.onStyleClicked('vector')}/>
        </div>}
        </div>
        
        
    )
}