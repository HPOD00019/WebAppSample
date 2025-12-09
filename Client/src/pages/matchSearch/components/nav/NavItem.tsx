
export interface NavItemProps{
    text: string;
    onClick : () => void;
    className?: string;
}


export const NavItem  = (props: NavItemProps) => {
    const classNames = props.className == null? "" : props.className;
    return(
        <p       className={"search-match-nav-item" + " " + classNames } onClick={() =>{ props.onClick();}}>{props.text}</p>
    );
}