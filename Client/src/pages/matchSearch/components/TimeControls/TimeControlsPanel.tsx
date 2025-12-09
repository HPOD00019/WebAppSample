import { TimeControl } from "./TimeControl";

export interface TimeControlsPanelProps{
    controlChoosedHandler: (control: number) => void;
    className?: string;
}

export const TimeControlsPanel = (props: TimeControlsPanelProps) => {
    const classNames = props.className == null? "" : props.className;
    return(
        <div className={"search-match__time-controls-panel" + " " + classNames }>
            <p className="game-kind-label">Blitz</p>
            <TimeControl clickHandler={() => props.controlChoosedHandler(1)} textContent="3+2"/>
            <TimeControl clickHandler={() => props.controlChoosedHandler(2)} textContent="5+0"/>
            <TimeControl clickHandler={() => props.controlChoosedHandler(3)} textContent="5+5"/>
            <p className="game-kind-label">Rapid</p>
            <TimeControl clickHandler={() => props.controlChoosedHandler(3)} textContent="5+5"/>
            <TimeControl clickHandler={() => props.controlChoosedHandler(3)} textContent="5+5"/>
                
            <p className="game-kind-label">Classical</p>
            <TimeControl clickHandler={() => props.controlChoosedHandler(3)} textContent="45+0"/>
            <TimeControl clickHandler={() => props.controlChoosedHandler(3)} textContent="60+30"/>
            
        </div>
    )
}