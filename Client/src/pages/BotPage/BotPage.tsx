import { useChessHttpGame } from "@features/chessboard/hooks/useChessHttpGame";
import { PlayBoard } from "@features/chessboard/reactChessboard/playBoard";
import { NavPanel } from "@pages/matchSearch/components/nav/NavPanel";
import { FlatPanelButton } from "@shared/ui/FlatPanelButton";
import { useState } from "react"

export const BotPage = () => {
    const [isWhitePlayableSide, changePlayableSide] = useState(true);
    const [elo, changeElo] = useState(0);
    const [currentFen, changeFen] = useState();
    const [fen, GetCurrentPosition, isPromotion,  OnMovePiece, FindLegalMoves, ValidateMove] = useChessHttpGame(elo);

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
                <PlayBoard playableSide={isWhitePlayableSide? 'white' : 'black'} position={fen} pieceMoveAttemptHandler={moveAttemptHandler}/>
                <div className="match-search-page__time-controls match-search-page__section">
                    
                </div>
            </div>
        </div>
    )
}