import { useState, useRef} from "react"
import { ChessCore } from "../chessLogic/ChessCore";
import {chessNotationToCoordinates, coordinatesToChessNotation} from '../chessLogic/transitions';
import type { coordinates} from '../types/chessboard.types';



export const useChessGame = (wsConnectionString: string, fen?: string) : [string , (from: coordinates, to: coordinates) => void, (square: coordinates) => coordinates[]] => {
    if(fen == null) fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    const [currentPosition, MovePiece] = useState(fen);
    const chessCoreRef = useRef<ChessCore>(new ChessCore(fen));
    
    const OnMovePiece = (from: coordinates, to: coordinates) => {
        const f : string =  coordinatesToChessNotation(from);
        const t : string = coordinatesToChessNotation(to);
        console.log(f, t);
        chessCoreRef.current.makeMove(f, t);
        const _fen: string = chessCoreRef.current.getFen();
        MovePiece(_fen);
    }
    const FindLegalMoves = (square: coordinates): coordinates[] => {
        console.log(square);
        const _square = coordinatesToChessNotation(square);
        console.log(_square);
        const moves = chessCoreRef.current.getLegalMoves(_square);
        const _moves: coordinates[] = [];
        for(const m of moves){
            _moves.push(chessNotationToCoordinates(m));
        }
        return _moves;
    }
    return [currentPosition, OnMovePiece, FindLegalMoves]
}