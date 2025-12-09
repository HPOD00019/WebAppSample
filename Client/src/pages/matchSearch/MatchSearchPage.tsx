import { PlayBoard } from "@features/chessboard/reactChessboard/playBoard";
import { NavPanel } from "./components/nav/NavPanel"
import { TimeControlsPanel } from "./components/TimeControls/TimeControlsPanel";
import { useChessNotationGame } from "@features/chessboard/hooks/useChessNotationGame";
import { useMatchSearch } from "@features/matchSearch/hooks/useMatchSearch";

export const MatchSearchPage = () => {
    const [fen, GetCurrentPosition, isPromotion,OnMovePiece, FindLegalMoves, ValidateMove] = useChessNotationGame();
    const [find] = useMatchSearch();

    const accountClicked = () => {
        console.log("account clicked!");
    }
    const settingsClicked = () => {
        console.log("settings clicked!");
    }
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
            <NavPanel className="match-search-page__nav-panel" onAccountClicked={accountClicked} onSettingsClicked={settingsClicked}/>
            <PlayBoard playableSide="white" pieceMoveAttemptHandler={moveAttemptHandler} position={fen}/>
            <TimeControlsPanel className="match-search-page__time-controls" controlChoosedHandler={(n:number) => {find(n)}}/>
        </div>
    )
}