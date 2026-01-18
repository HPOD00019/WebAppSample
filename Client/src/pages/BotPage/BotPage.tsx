import { StylesList } from "@features/chessboard/components/board-style/styles-list";
import { useChessHttpGame } from "@features/chessboard/hooks/useChessHttpGame";
import { PlayBoard } from "@features/chessboard/reactChessboard/playBoard";
import type { boardStyle } from "@features/chessboard/types/chessboard.types";
import { NavPanel } from "@pages/matchSearch/components/nav/NavPanel";
import { Opponent } from "@pages/matchSearch/components/opponent/opponent";
import { FlatPanelButton } from "@shared/ui/FlatPanelButton";
import { useState } from "react"

export const BotPage = () => {
    const [isWhitePlayableSide, changePlayableSide] = useState(true);
    const [elo, changeElo] = useState(0);
    const [currentFen, changeFen] = useState();
    const [fen, GetCurrentPosition, isPromotion,  OnMovePiece, FindLegalMoves, ValidateMove] = useChessHttpGame(elo);
    const [s, ChangeStyle] = useState<boardStyle>('classical');
    const moveAttemptHandler = (from: string, to: string) => {
        console.log("from: " + from);
        console.log("to: " + to);
        const isValid = ValidateMove(from, to);
        if(isValid){
            if(isPromotion(from, to)){
                console.log("Promo");
                OnMovePiece(from, to, 'n');
                return isValid;
            }
            OnMovePiece(from, to);
        }
        return false;
    }
    return (
        <div className="match-search-page">
            <NavPanel className="match-search-page__nav-panel match-search-page__section" />
            <div style={{width: '100%',  height: 'fit-content', display:'flex', flexDirection: 'row', padding: '15px'}}>
                <div style={{marginRight: 'auto', marginLeft: 'auto'}}>
                    <div style={{marginTop: 'auto', marginBottom: 'auto', marginRight: 'auto'}}><Opponent name="Stockfish" rating={elo}/></div>
                    <PlayBoard chessSet={s}  playableSide={isWhitePlayableSide? 'white' : 'black'} position={fen} pieceMoveAttemptHandler={moveAttemptHandler}/>
                    <div style={{marginTop: 'auto', marginBottom: 'auto', marginRight: 'auto'}}><Opponent name="Test_User_0" rating={1908}/></div>
                </div>
                
                <StylesList  onStyleClicked={(s: boardStyle) => {ChangeStyle(s)}}/>
                <div className=" match-search-page__section" style={{borderRadius: '15px'}}>
                    <FlatPanelButton onClick={() => {changeElo(1300)}}> <span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>1300 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(1400)}><span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>1400 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(1500)}> <span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>1500 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(1600)}> <span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>1600 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(1700)}> <span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>1700 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(1800)}> <span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>1800 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(1900)}> <span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>1900 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(2000)}> <span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>2000 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(2100)}><span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>2100 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(2200)}> <span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>2200 Elo</span></FlatPanelButton>
                    <FlatPanelButton onClick={() => changeElo(2300)}> <span style={{paddingLeft: '55px', paddingRight: '55px', whiteSpace: 'nowrap', display: 'flex', alignItems: 'center', justifyContent: 'center'}}>2300 Elo</span></FlatPanelButton>
                    
                </div>
            </div>
            
        </div>
    )
}