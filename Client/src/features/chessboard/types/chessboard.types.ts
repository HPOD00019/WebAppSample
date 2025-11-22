import type { SquareProps } from "../components/square";

export type square_cooardinates = {
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
  isClickable: boolean | null;
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
export interface ChessMove{
  San: string;
}
export interface ChessMessage{
  GameId: string;
  MessageType: "SuggestDraw" | "Resign" | "Move";
  Issuer: UserDTO;
  Move: ChessMove | null;
}
export interface UserDTO{
  Id: string;
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
