

import { Chessboard, type ChessboardOptions, type PieceDropHandlerArgs, type PieceHandlerArgs } from 'react-chessboard';

interface PlayBoardProps{
  width?: number;
  boardOrientation?: 'white' | 'black';
  position: string;
  playableSide: 'white' | 'black';
  pieceMoveAttemptHandler: (from: string, to: string) => boolean;
}

export const PlayBoard = (props : PlayBoardProps) => {
  
  const PieceDropHandler = (args: PieceDropHandlerArgs) => {
    const from = args.sourceSquare;
    const to = args.targetSquare;
    if(to === null) return false;
    const isValid = props.pieceMoveAttemptHandler(from, to);
    console.log(args);
    return isValid;
  }
  
  const options : ChessboardOptions = {
    id: "PlayBoard",
    showNotation: true,
    onPieceDrop: PieceDropHandler,
    position: props.position,
    
  }
  return(
    <div className = "play-board-container" style={{width: `900px`, height: `900px`}}>
      <Chessboard options={options}/>
    </div>
  )
}