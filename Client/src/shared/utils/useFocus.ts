import {useRef, useEffect, useState, useCallback} from 'react';

export const useFocus = (onFocus?: () => void, onLoseFocus?: () => void) => {

    const [hasFocus, setFocus] = useState(false);
    const [element, setElement] = useState<HTMLElement | null>(null);

    const handlersRef = useRef({onFocus, onLoseFocus})

    const setElementRef = useCallback((node: HTMLElement | null) => {
        setElement(node);
    }, []);

    const hasFocusRef = useRef(hasFocus);

    useEffect(() => {
        hasFocusRef.current = hasFocus;
    }, [hasFocus]);

    useEffect(() => {
        if (element == null) return;

        const handleFocus = (ev: FocusEvent) => {
            if(element.contains(ev.target as Node) && !hasFocusRef.current){
                if(element.contains(ev.relatedTarget as Node)) return;

                handlersRef.current.onFocus?.();
                setFocus(true);
            }

            if(!element.contains(ev.target as Node) && hasFocusRef.current){
                if(element.contains(ev.relatedTarget as Node)) return;
                
                handlersRef.current.onLoseFocus?.();
                setFocus(false);
            }
        }

        document.addEventListener('click', handleFocus);
        document.addEventListener('click', handleFocus);

        return () => {
            document.removeEventListener('click', handleFocus);
            document.removeEventListener('click', handleFocus);
        }
        
    }, [element]);

    return({hasFocus, setElementRef});
}