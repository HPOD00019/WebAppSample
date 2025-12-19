import { useEffect, useRef, useState } from "react"
import { ChessCore } from "../chessLogic/ChessCore";
import { useWSconnection } from "../connection/useWSconection";
import type { ChessMessage } from "../types/chessboard.types";


export const useChessNotationGame = (IssuerId: string, gameID: number, resetWhiteClock: (milliseconds: number) => void, resetBlackClock: (milliseconds: number) => void, wsConnectionString: string, fen?: string) : [string, () => string , (from: string, to: string) => boolean, (from: string, to: string, promotionType?: 'q'| 'n' | 'b' |'r') => boolean, (square: string) => string[], (from: string, to: string) => boolean] => {
    if(fen == null) fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    const OnRefreshPosition = (fen: string) => {
        ChangeFen(fen);
        chessCoreRef.current.reset(fen);
    }
    const OnChessMessageReceived = (message: ChessMessage) => {
        console.log(message);
        console.log(message.messageType === 'Move');
        console.log(message.messageType);
        if(message.messageType === 'Resign') console.log("Resign");
        if(message.messageType === 'SuggestDraw') console.log("Draw");
        if(message.messageType === 'Move'){
            const sanNotation = message.move?.san;
            {
                if (!sanNotation) {console.log(sanNotation); return;}
                if (!message.whiteRemainingTime) {console.log(message.whiteRemainingTime); return;}
                if (!message.blackRemainingTime) {console.log(message.blackRemainingTime); return;}
            }
            console.log(sanNotation);
            OnMovePieceSAN(sanNotation);
            resetWhiteClock(message.whiteRemainingTime);
            resetBlackClock(message.blackRemainingTime);
        }
    }
    
    const [SendChessMessage] = useWSconnection(IssuerId, OnChessMessageReceived, OnRefreshPosition, wsConnectionString);
    const [_fen, ChangeFen] = useState(fen);
    const chessCoreRef = useRef<ChessCore>(new ChessCore(fen));
    const GetCurrentPosition = () => {
        const ans = chessCoreRef.current.getFen();
        return ans;
    }
    useEffect(() => {
        ChangeFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        chessCoreRef.current.reset();
    }, [gameID]);
    
    const OnMovePiece = (from: string, to: string, promotionType?: 'q'| 'n' | 'b' |'r') => {
        const moves = chessCoreRef.current.getLegalMoves(from);
        const isLegal = moves.includes(to);
        
        if(promotionType && isLegal){
            chessCoreRef.current.makeMove(from, to, 'n');
            const newFen = chessCoreRef.current.getFen();
            ChangeFen(newFen);
            return isLegal;
        }
        
        if(isLegal){
            const result = chessCoreRef.current.makeMove(from, to);
            const newFen = chessCoreRef.current.getFen();
            ChangeFen(newFen);
            const message: ChessMessage = {
                GameId: gameID.toString(),
                messageType: 'Move',
                move: {
                    san: result.san ? result.san : "0-0",
                },
                issuer: {
                    Id: IssuerId
                }
            }
            
            console.log(message);
            SendChessMessage(message);
        }
        
        return isLegal;
    }
    const FindLegalMoves = (square: string) => {
        const moves = chessCoreRef.current.getLegalMoves(square);
        return moves;
    }
    const ValidateMove = (from: string, to: string) => {
        const moves = chessCoreRef.current.getLegalMoves(from);
        const isLegal = moves.includes(to);
        return isLegal;
    }
    const isPromotion = (from: string, to: string) => {
        const ans = chessCoreRef.current.isPromotion(from, to);
        return ans
    }
    const OnMovePieceSAN = (san: string) => {
        chessCoreRef.current.makeMoveSan(san);
        const fen = chessCoreRef.current.getFen();
        ChangeFen(fen);
    }
    return [_fen, GetCurrentPosition, isPromotion,  OnMovePiece, FindLegalMoves, ValidateMove]
}