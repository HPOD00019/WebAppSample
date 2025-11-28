import { useState, useRef} from "react"
import { ChessCore } from "../chessLogic/ChessCore";
import {chessNotationToCoordinates, coordinatesToChessNotation} from '../chessLogic/transitions';
import type {ChessMessage, coordinates} from '../types/chessboard.types';
import { useWSconnection } from "../connection/useWSconnection";



export const useChessGame = (colorReceivedHandler: (isWhite: boolean) => void, fen?: string) : [ string , (from: coordinates, to: coordinates) => void, (square: coordinates) => coordinates[]] => {
    if(fen == null) fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    
    const [currentPosition, MovePiece] = useState(fen);

    const chessCoreRef = useRef<ChessCore>(new ChessCore(fen));

    const poistionHandler = (n: string) => {
        chessCoreRef.current.SetPosition(n);
        MovePiece(n);
    }
    const messageHandler = (message: ChessMessage) => {
        if(message.Move == null) return;
        chessCoreRef.current.makeMoveSan(message.Move.San);
        const _fen = chessCoreRef.current.getFen();
        OnOpponentMove(_fen);
    }

    const [SendMessage] = useWSconnection(messageHandler, colorReceivedHandler, poistionHandler);

    const OnOpponentMove = (newFen: string) => {
        MovePiece(newFen);
    }
    const OnMovePiece = (from: coordinates, to: coordinates) => {
        const f : string =  coordinatesToChessNotation(from);
        const t : string = coordinatesToChessNotation(to);
        const result = chessCoreRef.current.makeMove(f, t);
        const _fen: string = chessCoreRef.current.getFen();
        MovePiece(_fen);
        const san = result.san;
        if(san == undefined){
            console.error("SAN notation was undefined, after move");
            return;
        }
        const msg: ChessMessage = {
          GameId: "1",
          MessageType: "Move",
          Issuer: {Id: "1" },
          Move: {San: san}
        }
        SendMessage(msg);
    }
    const FindLegalMoves = (square: coordinates): coordinates[] => {
        const _square = coordinatesToChessNotation(square);
        const moves = chessCoreRef.current.getLegalMoves(_square);
        const _moves: coordinates[] = [];
        for(const m of moves){
            _moves.push(chessNotationToCoordinates(m));
        }
        return _moves;
    }
    return [currentPosition, OnMovePiece, FindLegalMoves]
}