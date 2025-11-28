import type { pieceOnBoard } from "../types/chessboard.types";
import { PIECE_IMAGES } from './findPieceImg.ts';

export const attachPiecePicture = (piece: pieceOnBoard, path: string) : pieceOnBoard => {
    if(path == "sdf") console.log(path);
    const pieceName = `${piece.color}_${piece.piece}`as keyof typeof PIECE_IMAGES;
    const pathToPiece = PIECE_IMAGES[pieceName];
    piece = {...piece, svgSource: pathToPiece}
    console.log(pieceName);
    console.log(pathToPiece);

    return piece;
}