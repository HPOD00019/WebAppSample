import { useChessHttpGame } from "@features/chessboard/hooks/useChessHttpGame";
import { PlayBoard } from "@features/chessboard/reactChessboard/playBoard";
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
        <div>
            <PlayBoard playableSide={isWhitePlayableSide? 'white' : 'black'} position={fen} pieceMoveAttemptHandler={moveAttemptHandler}/>
        </div>
    )
}