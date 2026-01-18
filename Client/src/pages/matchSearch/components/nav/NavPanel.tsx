import { FlatPanelButton } from "@shared/ui/FlatPanelButton";
import { useNavigate, useNavigation } from "react-router-dom";

export interface NavPanelProps{
    onAccountClicked?: () => void;
    onSettingsClicked?: () => void;
    onBotPageClicked?: () => void;
    onNewGameClicked?: () => void;
    className?: string;
}

export const NavPanel = (props: NavPanelProps) => {
    const classNames = props.className == null? "" : props.className;
    const navigate = useNavigate();
    return(
        <div className={"search-match-nav-panel" + " " + classNames }>
            <FlatPanelButton onClick={() => navigate('/botPage')}><p style={{margin:'0px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>My Account</p></FlatPanelButton>
            <FlatPanelButton onClick={props.onSettingsClicked}><p style={{margin:'0px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>Options</p></FlatPanelButton>
            <FlatPanelButton onClick={() => navigate('/search')}><p style={{margin:'0px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>New Game</p></FlatPanelButton>
            <FlatPanelButton onClick={() => navigate('/botPage')}><p style={{margin:'0px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>Play with Bot</p></FlatPanelButton>
        </div>
    )
}