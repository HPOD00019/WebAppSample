import { NavItem } from "./NavItem";

export interface NavPanelProps{
    onAccountClicked: () => void;
    onSettingsClicked: () => void;
    className?: string;
}

export const NavPanel = (props: NavPanelProps) => {
    const classNames = props.className == null? "" : props.className;
    
    return(
        <div className={"search-match-nav-panel" + " " + classNames }>
            <NavItem text="My Account" onClick={props.onAccountClicked}/>
            <NavItem text="Options" onClick={props.onSettingsClicked}/>
        </div>
    )
}