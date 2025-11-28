import type { pieceColor, pieceType  } from '../types/chessboard.types'

export const FillBoard = () => {
    
}




function parseFEN(fen: string): void {
    const positionPart = fen.split(' ')[0];
    const rows = positionPart.split('/');
    
    for (let rowIndex = 0; rowIndex < 8; rowIndex++) {
        const row = rows[rowIndex];
        let colIndex = 0;
        
        for (let i = 0; i < row.length; i++) {
            const char = row[i];
            
            if (!isNaN(parseInt(char))) {
                colIndex += parseInt(char);
            } else {
                const isWhite = char === char.toUpperCase();
                const color: pieceColor = isWhite ? 'white' : 'black';
                const pieceType: pieceType = getPieceType(char.toLowerCase());
                
                const x: number = colIndex;
                const y: number = 7 - rowIndex; 
                
                onPieceFound(pieceType, color, x, y);
                
                colIndex++;
            }
        }
    }
}

function getPieceType(char: string): pieceType {
    const pieceMap: { [key: string]: pieceType } = {
        'p': 'pawn',
        'n': 'knight',
        'b': 'bishop',
        'r': 'rook',
        'q': 'queen',
        'k': 'king'
    };
    
    if (!pieceMap[char]) {
        throw new Error(`Неизвестный тип фигуры: ${char}`);
    }
    
    return pieceMap[char];
}

function onPieceFound(pieceType: pieceType, color: pieceColor, x: number, y: number): void {
    console.log(`Фигура: ${pieceType}, цвет: ${color}, координаты: (${x}, ${y})`);
}

const fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
parseFEN(fen);