import { FlatPanelButton } from "@shared/ui/FlatPanelButton";
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
            <FlatPanelButton onClick={props.onAccountClicked}><p style={{margin:'0px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>My Account</p></FlatPanelButton>
            <FlatPanelButton onClick={props.onAccountClicked}><p style={{margin:'0px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>Options</p></FlatPanelButton>
            <FlatPanelButton onClick={props.onAccountClicked}><p style={{margin:'0px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>My Games</p></FlatPanelButton>
            <FlatPanelButton onClick={props.onAccountClicked}><p style={{margin:'0px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>Play with Bot</p></FlatPanelButton>
        </div>
    )
}