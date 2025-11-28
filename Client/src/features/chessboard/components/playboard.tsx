import  { useEffect, useState } from "react";
import type {pieceOnBoard, squareOnBoard} from '../types/chessboard.types.ts';
import {Piece} from './piece.tsx';
import '../styles/board.css'
import {Square} from './square.tsx';
import {generateId} from '@shared/utils/generateId.ts';
import {useChessGame} from '../hooks/useChessGame.ts';
import { updatePieces } from "../utils/updatePieces.ts";
import {attachPiecePicture} from "../utils/attachPiecePicture.ts";


export interface BoardProps{
    boardImgSrc : string;
    piecesSetPath: string;


    isWhite?: boolean;


    onPositionChange: ( changes : pieceOnBoard[] ) => void;
    
}


export const PlayBoard  = (props : BoardProps) => {
    const [pieces, SetPieces] = useState<pieceOnBoard[]>([]);
    const colorReceivedHandler = (_isWhite: boolean) => {
        SetPieces(prev =>{
            const n = [];
            for(const p of prev){
                n.push({...p});
            }
            return n;
        });
        SetColor(_isWhite);
    }

    const {boardImgSrc} = props;
    const [isWhite, SetColor] = useState(props.isWhite);
    const [gameState, ChangeGameState, FindMoves] = useChessGame(colorReceivedHandler);
    const [squares, SetSquares] = useState<squareOnBoard[]>([]);


    useEffect(() => {
        SetPieces(prev => {
            const _pieces = updatePieces(prev, gameState);
            return _pieces;
        });
        SetSquares([]);
    }, [gameState]);

    useEffect(() => {
        SetPieces(prev => {
            const newPieces: pieceOnBoard[] = []
            for(const p of prev){
                const newPiece = attachPiecePicture(p, props.piecesSetPath);
                newPieces.push(newPiece);
            }
            return newPieces;
        });
        
    }, []);

    
    const pieceFocusedHandler = (item : pieceOnBoard) => {
        const additionalSquares: squareOnBoard[] = [];
        for (const move of FindMoves(item.Position)){
            additionalSquares.push({
                squareId: generateId(),
                Position: {x: move.x, y: move.y},
                state: 'move-destination-square',
                issuerId: item.pieceId,
            });
        }
        SetSquares(prev => [...prev.filter((item) => item.state!='move-destination-square'), ...additionalSquares]);
        
    }

    const pieceLoseFocuseHandler = (piece: pieceOnBoard) => {
        SetSquares(prev => prev.filter((item) => item.issuerId != piece.pieceId));
    }
    
    const squareClickedHandler = (square: squareOnBoard) => {
        const issuer = pieces.find((item) => item.pieceId == square.issuerId);
        if(issuer){
            ChangeGameState(issuer.Position, square.Position);
            issuer.Position = square.Position;
            SetPieces(prev => [...prev.filter((item) => square.issuerId != item.pieceId), issuer]);
        }
        
    }
    return(
        <div className="board-container" 
            style={{
            justifyContent: 'center',
            alignContent: 'center',
            position: 'relative',
            width: '900px',
            height: '900px',
            margin: '0 auto',
            transform: isWhite ? 'none' : 'rotate(180deg)',
            }}
        >
            <img 
                src = {boardImgSrc} 
                alt=""
                style={{
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    width: '100%',
                    height: '100%',
                    objectFit: 'cover',
                    zIndex: 1
                }}
            
            />

            {pieces.map(item => {
                return(
                    <Piece 
                        key={`${item.pieceId}-${Date.now()}`}
                        pieceId = {item.pieceId}
                        side = {item.color}
                        kind = {item.piece}
                        pieceSrc = {item.svgSource}
                        isClickable = {isWhite === (item.color === 'white')}
                        onPieceFocus={() => pieceFocusedHandler(item)}
                        onPieceLoseFocus={() => pieceLoseFocuseHandler(item)}
                        style={{
                            zIndex: 5,
                            gridRow: item.Position.y ,
                            gridColumn: item.Position.x,
                            transform: isWhite ? 'none' : 'rotate(180deg)',
                        }}
                    />
                )
            })}

            {squares.map(item => {
                return(
                    <Square 
                        state = {item.state}
                        key={item.squareId}
                        squareId={item.squareId}
                        onMouseClick={() => squareClickedHandler(item)}
                        style={{
                            zIndex: 3,
                            gridRow: item.Position.y,
                            gridColumn: item.Position.x,
                        }}
                    />
                )
            })}
        </div>
        
    )
}