import { Chess } from "chess.ts";
import type { coordinates, pieceColor, pieceType } from "../types/chessboard.types";
import {pieceTypeShort, coordinatesToChessNotation, chessNotationToCoordinates} from './transitions';
class PlayModeGameManager{
    fenPosition : string;
    chessLogic = new Chess();
    constructor(positionInFen: string){
        this.fenPosition = positionInFen;
        
    }

    public checkMoveForLegal (moves: {
        piece: pieceType;
        color?: pieceColor;
        from: coordinates;
        to: coordinates;
    }[]): boolean {
        const _moves: {piece: string; from: string; to: string; }[] = [];
        for(const move of moves){
            _moves.push({
                piece: pieceTypeShort(move.piece),
                from: coordinatesToChessNotation(move.from),
                to: coordinatesToChessNotation(move.to),
            });
        }

        this.chessLogic.moves(_moves)
    }
}