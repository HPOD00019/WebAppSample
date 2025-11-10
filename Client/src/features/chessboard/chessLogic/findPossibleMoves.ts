import type {pieceType, coordinates, direction, pieceColor} from '../types/chessboard.types';
import {pieceMoveSchemas} from '../types/piecesMoveSchemas';

export const findPossibleMoves = (
  piece: pieceType, 
  position: coordinates, 
  color: pieceColor = 'white'
): coordinates[] => {
  const moves: coordinates[] = [];
  const schema = pieceMoveSchemas[piece];
  
  for (const direction of schema.directions) {
    if (schema.isPawn) {
      handlePawnMoves(position, direction, color, moves);
    } 

    else if (schema.maxSteps === 1) {
      handleSingleStepMove(position, direction, moves);
    } 
    
    else {
      handleMultipleStepsMove(position, direction, moves);
    }
  }
  
  return moves.filter(move => 
    move.x >= 1 && move.x <= 8 && move.y >= 1 && move.y <= 8
  );
};

const handleSingleStepMove = (
  position: coordinates, 
  direction: direction, 
  moves: coordinates[]
): void => {
  const newX = position.x + direction.dx;
  const newY = position.y + direction.dy;
  
  moves.push({ x: newX, y: newY });
};

const handleMultipleStepsMove = (
  position: coordinates, 
  direction: direction, 
  moves: coordinates[]
): void => {
  let steps = 1;
  
  while (steps <= 8) { 
    const newX = position.x + (direction.dx * steps);
    const newY = position.y + (direction.dy * steps);
    
    if (newX < 1 || newX > 8 || newY < 1 || newY > 8) {
      break;
    }
    
    moves.push({ x: newX, y: newY });
    steps++;
  }
};

const handlePawnMoves = (
  position: coordinates,
  direction: direction,
  color: pieceColor,
  moves: coordinates[]
): void => {
  const directionMultiplier = color === 'white' ? 1 : -1;
  const startRow = color === 'white' ? 2 : 7;
  
  const newX = position.x + direction.dx;
  const newY = position.y + (direction.dy * directionMultiplier);
  
  if (newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8) {
    moves.push({ x: newX, y: newY });
  }
  
  if (position.y === startRow) {
    const doubleMoveY = position.y + (2 * directionMultiplier);
    if (doubleMoveY >= 1 && doubleMoveY <= 8) {
      moves.push({ x: position.x, y: doubleMoveY });
    }
  }
};