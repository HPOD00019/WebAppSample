import React, { useState } from "react";
import type {coordinates, pieceType, pieceColor, pieceOnBoard, squareOnBoard} from '../types/chessboard.types.ts';
import {Piece} from './piece.tsx';
import '../styles/board.css'
import {findPossibleMoves} from '../chessLogic/findPossibleMoves.ts'
import type {SquareProps} from './square.tsx';
import {Square} from './square.tsx';
import {generateId} from '@shared/utils/generateId.ts';


export interface BoardProps{
    boardImgSrc : string;
    pieces : pieceOnBoard[];
    squares: squareOnBoard[];
    onPositionChange: ( changes : pieceOnBoard[] ) => void;
    
}


export const PlayBoard  = (props : BoardProps) => {
    const {boardImgSrc} = props;
    const [squares, SetSquares] = useState(props.squares);
    const [pieces, SetPieces] = useState(props.pieces);
    console.log(squares);

    const pieceFocusedHandler = (item : pieceOnBoard) => {
        const additionalSquares: squareOnBoard[] = [];
        
        for (const move of findPossibleMoves(item.piece, item.Position, item.color)){
            additionalSquares.push({
                squareId: generateId(),
                Position: {x: move.x, y: move.y},
                state: 'move-destination-square',
                issuerId: item.pieceId,
            });
        }
        console.log(additionalSquares);
        SetSquares(prev => [...prev.filter((item) => item.state!='move-destination-square'), ...additionalSquares]);
        
    }

    const pieceLoseFocuseHandler = (piece: pieceOnBoard) => {
        SetSquares(prev => prev.filter((item) => item.issuerId != piece.pieceId));
    }
    
    const squareClickedHandler = (square: squareOnBoard) => {
        const issuer = pieces.find((item) => item.pieceId == square.issuerId);
        if(issuer){
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
            width: '3000px',
            height: '3000px',
            margin: '0 auto'
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

            {props.pieces.map(item => {
                return(
                    <Piece 
                        key = {item.pieceId}
                        pieceId = {item.pieceId}
                        side = {item.color}
                        kind = {item.piece}
                        pieceSrc = {item.svgSource}
                        onPieceFocus={() => pieceFocusedHandler(item)}
                        onPieceLoseFocus={() => pieceLoseFocuseHandler(item)}
                        style={{
                            zIndex: 3,
                            gridRow: item.Position.y ,
                            gridColumn: item.Position.x,
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
                            zIndex: 2,
                            gridRow: item.Position.y,
                            gridColumn: item.Position.x,
                        }}
                    />
                )
            })}
        </div>
        
    )
}