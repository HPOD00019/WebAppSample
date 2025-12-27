import { useRef, useState } from "react"
import { ChessCore } from "../chessLogic/ChessCore";
import { useHttpGameConnection } from "../connection/useHttpGameConnection";


export const useChessHttpGame = (elo:number, fen?: string) : [string, () => string , (from: string, to: string) => boolean, (from: string, to: string, promotionType?: 'q'| 'n' | 'b' |'r') => Promise<boolean>, (square: string) => string[], (from: string, to: string) => boolean] => {
    if(fen == null) fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    const [_fen, ChangeFen] = useState(fen);
    const chessCoreRef = useRef<ChessCore>(new ChessCore(fen));
    const GetCurrentPosition = () => {
        const ans = chessCoreRef.current.getFen();
        return ans;
    }
    const [getBestMove] = useHttpGameConnection();
    const OnMovePiece = async (from: string, to: string, promotionType?: 'q'| 'n' | 'b' |'r') => {
        const moves = chessCoreRef.current.getLegalMoves(from);
        const isLegal = moves.includes(to);
        
        if(promotionType && isLegal){
            chessCoreRef.current.makeMove(from, to, 'n');
            const newFen = chessCoreRef.current.getFen();
            ChangeFen(newFen);
            return isLegal;
        }
        
        if(isLegal){
            chessCoreRef.current.makeMove(from, to);
            const newFen = chessCoreRef.current.getFen();
            ChangeFen(newFen);
        }
        const move = await getBestMove(_fen, elo);
        chessCoreRef.current.makeMoveSan(move);
        const fen = chessCoreRef.current.getFen();
        ChangeFen(fen);
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
    return [_fen, GetCurrentPosition, isPromotion,  OnMovePiece, FindLegalMoves, ValidateMove]
}