import type { SquareProps } from "../components/square";

type square_cooardinates = {
    x: number;
    y: number;
}
export type pieceType = 'pawn' | 'knight' | 'bishop' | 'rook' | 'queen' | 'king';
export type pieceColor = 'white' | 'black';
export type coordinates = { x: number; y: number };
export type direction = { dx: number; dy: number };

interface piece{
    currentLocation : square_cooardinates;
    
}
export interface movePattern {
  directions: direction[];
  maxSteps?: number; 
  isPawn?: boolean;
}

export interface pieceOnBoard {
  Position: coordinates;
  piece: pieceType;
  color: pieceColor;
  svgSource: string; 
  pieceId: number;
}

export interface squareOnBoard extends SquareProps{
  Position: coordinates, 
  issuerId?: number
}

interface piece_knight{

}

interface piece_pawn{
    
}

interface piece_bishop{
    
}

interface piece_king{
    
}

interface piece_rook{
    
}

interface board{

}

interface square{
    
}
