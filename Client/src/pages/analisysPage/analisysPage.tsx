import { StylesList } from "@features/chessboard/components/board-style/styles-list";
import { useChessAnalisys } from "@features/chessboard/hooks/useChessAnalisys";
import { useChessHttpGame } from "@features/chessboard/hooks/useChessHttpGame";
import { AnalisysBoard } from "@features/chessboard/reactChessboard/analysisBoard"
import { useState } from "react";

export const AnalisysPage = () => {
    const [isWhitePlayableSide, changePlayableSide] = useState(true);
    const [elo, changeElo] = useState(0);
    const [currentFen, changeFen] = useState();

    const [fen, GetCurrentPosition, isPromotion,  OnMovePiece, FindLegalMoves, ValidateMove] = useChessAnalisys();
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
            return true;
        }
        return false;
    }
    
    return(
        <div>
            <AnalisysBoard position={fen} pieceMoveAttemptHandler={moveAttemptHandler}/>
            <StylesList />
        </div>
    )
}