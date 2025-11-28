import type { pieceOnBoard } from "../types/chessboard.types";
import { fenToPieces } from "../chessLogic/transitions";
import { attachPiecePicture } from "./attachPiecePicture";

export const updatePieces = (currentPieces: pieceOnBoard[], newFen: string, piecesSetPath: string = ""): pieceOnBoard[] => {
  const newPieces = fenToPieces(newFen);
  
  const currentPieceMap = new Map(currentPieces.map(p => [`${p.Position.x},${p.Position.y}`, p]));
  
  const updatedPieces = newPieces.map(newPiece => {
    const key = `${newPiece.Position.x},${newPiece.Position.y}`;
    const existingPiece = currentPieceMap.get(key);
    
    if (existingPiece && 
        existingPiece.piece === newPiece.piece && 
        existingPiece.color === newPiece.color) {
      return existingPiece;
    } else {
      return attachPiecePicture({
        ...newPiece,
        pieceId: existingPiece?.pieceId || newPiece.pieceId, 
        isClickable: existingPiece?.isClickable ?? newPiece.isClickable, 
      }, piecesSetPath);
    }
  });
  const n = [];
  for(const p of updatedPieces){
    n.push({...p});
  }
  console.log(n);
  return n;
}