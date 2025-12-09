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
    onPositionChange: ( changes : pieceOnBoard[] ) => void;
    
}


export const PlayBoard  = (props : BoardProps) => {
    const {boardImgSrc} = props;
    const [gameState, ChangeGameState, FindMoves] = useChessGame();
    const [squares, SetSquares] = useState<squareOnBoard[]>([]);
    const [pieces, SetPieces] = useState<pieceOnBoard[]>([]);
    console.log(squares);

    useEffect(() => {
        SetPieces(prev => {
            const _pieces = updatePieces(prev, gameState);
            return _pieces;
        });
        
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
        SetPieces(prev => {
            return prev.map(piece => {
                const foundSquare = additionalSquares.find(square => 
                piece.Position.x === square.Position.x && 
                piece.Position.y === square.Position.y
                );
                
                if (foundSquare) {
                return {...piece, isClickable: false};
                }
                
                return piece;
            });
        });
        SetSquares(prev => [...prev.filter((item) => item.state!='move-destination-square'), ...additionalSquares]);
        
    }

    const pieceLoseFocuseHandler = (piece: pieceOnBoard) => {
        SetPieces(prev => {
            return prev.map(piece => {
                if (piece.isClickable == false) {
                    console.log("ASDASDASDDS")
                    return {...piece, isClickable: true};
                }
                return piece;
            });
        });
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
            width: '1000px',
            height: '1000px',
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

            {pieces.map(item => {
                return(
                    <Piece 
                        key = {item.pieceId}
                        pieceId = {item.pieceId}
                        side = {item.color}
                        kind = {item.piece}
                        pieceSrc = {item.svgSource}
                        isClickable = {item.isClickable}
                        onPieceFocus={() => pieceFocusedHandler(item)}
                        onPieceLoseFocus={() => pieceLoseFocuseHandler(item)}
                        style={{
                            zIndex: 5,
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