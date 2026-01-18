import { TimeControl } from "./TimeControl";

export interface TimeControlsPanelProps{
    controlChoosedHandler: (control: number) => void;
    className?: string;
}

export const TimeControlsPanel = (props: TimeControlsPanelProps) => {
    const classNames = props.className == null? "" : props.className;
    return(
        <div className={"search-match__time-controls-panel" + " " + classNames }>
            <p className="game-kind-label" style={{paddingTop: '0px'}}>Blitz</p>
            <div style={{ display: 'flex', flexDirection: 'row', gap: '20px'}}>
                <TimeControl clickHandler={() => props.controlChoosedHandler(1)} textContent="3min + 2sec"/>
                <TimeControl clickHandler={() => props.controlChoosedHandler(2)} textContent="5min + 0sec"/>
                <TimeControl clickHandler={() => props.controlChoosedHandler(3)} textContent="5min + 5sec"/>
            </div>
            <p className="game-kind-label">Rapid</p>
            <div style={{ display: 'flex', flexDirection: 'row', gap: '20px'}}>
                <TimeControl clickHandler={() => props.controlChoosedHandler(4)} textContent="10min + 5sec"/>
                <TimeControl clickHandler={() => props.controlChoosedHandler(5)} textContent="15min + 5sec"/>
            </div>
                
            <p className="game-kind-label">Classical</p>
            <div style={{ display: 'flex', flexDirection: 'row', gap: '20px'}}>
                <TimeControl clickHandler={() => props.controlChoosedHandler(6)} textContent="45min + 0sec"/>
                <TimeControl clickHandler={() => props.controlChoosedHandler(7)} textContent="60min + 30sec"/>
            </div>
            
        </div>
    )
}