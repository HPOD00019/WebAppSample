import type { coordinates, pieceColor, pieceType, pieceOnBoard} from "../types/chessboard.types";        
import { generateId } from "@shared/utils/generateId";

export const chessNotationToCoordinates = (notation: string): coordinates => {
  if (!/^[a-h][1-8]$/.test(notation)) {
    throw new Error('Некорректная шахматная нотация');
  }
  
  const files = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
  const file = notation[0];
  const rank = parseInt(notation[1]);
  
  return {
    x: files.indexOf(file) + 1, 
    y: 9 - rank 
  };
}

export const coordinatesToChessNotation = (coords: coordinates): string => {
  if (coords.x < 1 || coords.x > 8 || coords.y < 1 || coords.y > 8) {
    throw new Error('Координаты должны быть в диапазоне от 1 до 8');
  }
  
  const files = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
  
  const file = files[coords.x - 1];
  const rank = 9 - coords.y;
  
  return `${file}${rank}`;
}


export const pieceColorShort = (color: pieceColor): string =>{
  switch (color){
    case 'white': {
      return('w');
    }
    case 'black': {
      return('b');
    }
  }
}

export const pieceTypeShort = (piece: pieceType): string => {
  switch(piece){
    case 'bishop': return('b');
    case 'king': return('k');
    case 'pawn': return('p');
    case 'queen': return('q');
    case 'knight': return('n');
    case 'rook': return('r');
  }
}
function CharToPiece(char: string): { type: pieceType; color: pieceColor } | null {
  const pieceMap: { [key: string]: { type: pieceType; color: pieceColor } } = {
    'P': { type: 'pawn', color: 'white' },
    'N': { type: 'knight', color: 'white' },
    'B': { type: 'bishop', color: 'white' },
    'R': { type: 'rook', color: 'white' },
    'Q': { type: 'queen', color: 'white' },
    'K': { type: 'king', color: 'white' },
    'p': { type: 'pawn', color: 'black' },
    'n': { type: 'knight', color: 'black' },
    'b': { type: 'bishop', color: 'black' },
    'r': { type: 'rook', color: 'black' },
    'q': { type: 'queen', color: 'black' },
    'k': { type: 'king', color: 'black' }
  };  
  return pieceMap[char] || null;
}
export const fenToPieces = (fen: string): pieceOnBoard[] => {

  const boardPart = fen.split(' ')[0];
  const rows = boardPart.split('/');
  
  const pieces: pieceOnBoard[] = [];
  
  for (let rowIndex = 0; rowIndex < 8; rowIndex++) {
    const row = rows[rowIndex];
    let fileIndex = 0; 
    
    for (let charIndex = 0; charIndex < row.length; charIndex++) {
      const char = row[charIndex];
      
      if (isNaN(parseInt(char))) {
        const piece = CharToPiece(char);
        if (piece) {
          const x = fileIndex + 1;
          const y = rowIndex + 1;
          
          pieces.push({
            Position: { x, y },
            piece: piece.type,
            color: piece.color,
            svgSource: "",
            pieceId: generateId(),
            isClickable: true,
          });
        }
        fileIndex++;
      } else {
        fileIndex += parseInt(char);
      }
    }
  }
  
  return pieces;
}