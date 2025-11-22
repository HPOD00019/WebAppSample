import blackQueen from '@shared/pieceSets/black_queen.svg';
import blackKing from '@shared/pieceSets/black_king.svg';
import blackRook from '@shared/pieceSets/black_rook.svg';
import blackBishop from '@shared/pieceSets/black_bishop.svg';
import blackKnight from '@shared/pieceSets/black_knight.svg';
import blackPawn from '@shared/pieceSets/black_pawn.svg';
import whiteQueen from '@shared/pieceSets/white_queen.svg';
import whiteKing from '@shared/pieceSets/white_king.svg';
import whiteRook from '@shared/pieceSets/white_rook.svg';
import whiteBishop from '@shared/pieceSets/white_bishop.svg';
import whiteKnight from '@shared/pieceSets/white_knight.svg';
import whitePawn from '@shared/pieceSets/white_pawn.svg';

export const getPieceImages = () => {
  return {
    "black_queen": blackQueen,
    "black_king": blackKing,
    "black_rook": blackRook,
    "black_bishop": blackBishop,
    "black_knight": blackKnight,
    "black_pawn": blackPawn,
    "white_queen": whiteQueen,
    "white_king": whiteKing,
    "white_rook": whiteRook,
    "white_bishop": whiteBishop,
    "white_knight": whiteKnight,
    "white_pawn": whitePawn
  };
};

export const PIECE_IMAGES = {
  "black_queen": blackQueen,
  "black_king": blackKing,
  "black_rook": blackRook,
  "black_bishop": blackBishop,
  "black_knight": blackKnight,
  "black_pawn": blackPawn,
  "white_queen": whiteQueen,
  "white_king": whiteKing,
  "white_rook": whiteRook,
  "white_bishop": whiteBishop,
  "white_knight": whiteKnight,
  "white_pawn": whitePawn
} as const;

export type PieceImageKey = keyof typeof PIECE_IMAGES;

export const getPieceImage = (color: 'black' | 'white', piece: string): string => {
  const key = `${color}_${piece.toLowerCase()}` as PieceImageKey;
  return PIECE_IMAGES[key];
};