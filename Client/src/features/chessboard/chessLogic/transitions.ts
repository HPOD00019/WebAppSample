import type { coordinates, pieceColor, pieceType } from "../types/chessboard.types";        

export const chessNotationToCoordinates = (notation: string): coordinates => {
  if (!/^[a-h][1-8]$/.test(notation)) {
    throw new Error('Некорректная шахматная нотация');
  }
  
  const files = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
  const file = notation[0];
  const rank = parseInt(notation[1]);
  
  return {
    x: files.indexOf(file),
    y: 8 - rank
  };
}


export const coordinatesToChessNotation = (coords: coordinates): string  => {
  if (coords.x < 0 || coords.x > 7 || coords.y < 0 || coords.y > 7) {
    throw new Error('Координаты должны быть в диапазоне от 0 до 7');
  }
  
  const files = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
  
  const file = files[coords.x];
  const rank = 8 - coords.y;
  
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

