import type { pieceOnBoard } from "../types/chessboard.types";
import { fenToPieces } from "../chessLogic/transitions";
import { attachPiecePicture } from "./attachPiecePicture";

export const updatePieces = (
  currentPieces: pieceOnBoard[], 
  newFen: string
): pieceOnBoard[] => {
  const newPieces = fenToPieces(newFen); 
  const currentMap = new Map(currentPieces.map(p => [`${p.Position.x},${p.Position.y}`, p]));
  const newMap = new Map(newPieces.map(p => [`${p.Position.x},${p.Position.y}`, p]));
  
  const result = [...currentPieces];
  let changes = 0;
  
  for (const [key, newPiece] of newMap) {
    const currentPiece = currentMap.get(key);
    if (!currentPiece) {
      const _newPiece = attachPiecePicture(newPiece, "");
      result.push(_newPiece);
      changes++;
    } else if (currentPiece.piece !== newPiece.piece || 
               currentPiece.color !== newPiece.color) {
      const index = result.findIndex(p => 
        p.Position.x === newPiece.Position.x && 
        p.Position.y === newPiece.Position.y
      );
      result[index] = newPiece;
      changes++;
    }
  }
  
  for (let i = result.length - 1; i >= 0; i--) {
    const key = `${result[i].Position.x},${result[i].Position.y}`;
    if (!newMap.has(key)) {
      result.splice(i, 1);
      changes++;
    }
  }
  
  return changes > 0 ? result : currentPieces;
}