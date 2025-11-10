import type {pieceType, movePattern} from './chessboard.types'


export const pieceMoveSchemas: Record<pieceType, movePattern> = {
  pawn: {
    directions: [
      { dx: 0, dy: 1 },  
      { dx: 0, dy: 2 },  
      { dx: 1, dy: 1 },  
      { dx: -1, dy: 1 }, 
    ],
    maxSteps: 1,
    isPawn: true
  },
  
  knight: {
    directions: [
      { dx: 2, dy: 1 }, { dx: 2, dy: -1 },
      { dx: -2, dy: 1 }, { dx: -2, dy: -1 },
      { dx: 1, dy: 2 }, { dx: 1, dy: -2 },
      { dx: -1, dy: 2 }, { dx: -1, dy: -2 }
    ],
    maxSteps: 1
  },
  
  bishop: {
    directions: [
      { dx: 1, dy: 1 }, { dx: 1, dy: -1 },
      { dx: -1, dy: 1 }, { dx: -1, dy: -1 }
    ]
  },
  
  rook: {
    directions: [
      { dx: 1, dy: 0 }, { dx: -1, dy: 0 },
      { dx: 0, dy: 1 }, { dx: 0, dy: -1 }
    ]
  },
  
  queen: {
    directions: [
      { dx: 1, dy: 0 }, { dx: -1, dy: 0 },
      { dx: 0, dy: 1 }, { dx: 0, dy: -1 },
      { dx: 1, dy: 1 }, { dx: 1, dy: -1 },
      { dx: -1, dy: 1 }, { dx: -1, dy: -1 }
    ]
  },
  
  king: {
    directions: [
      { dx: 1, dy: 0 }, { dx: -1, dy: 0 },
      { dx: 0, dy: 1 }, { dx: 0, dy: -1 },
      { dx: 1, dy: 1 }, { dx: 1, dy: -1 },
      { dx: -1, dy: 1 }, { dx: -1, dy: -1 }
    ],
    maxSteps: 1
  }
};