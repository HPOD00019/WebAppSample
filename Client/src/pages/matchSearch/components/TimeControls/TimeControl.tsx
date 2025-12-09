export interface TimeControlProps{
    textContent: string,
    clickHandler: () => void;
    className?: string;
}

export const TimeControl = (props: TimeControlProps) => {
    const classNames = props.className == null? "" : props.className;
    return(
        <button className = {"search-match__time-control" + " " + classNames } onClick={() => props.clickHandler()}>
            <p className="search-match__time-control-text">{props.textContent}</p>
        </button>
    )
}