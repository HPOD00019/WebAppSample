
import { Chessboard, type ChessboardOptions, type PieceDropHandlerArgs} from 'react-chessboard';
import {vectorOptions} from '../sets/vector';

interface AnalisysBoardProps {
  width?: number;
  boardOrientation?: 'white' | 'black';
  position: string;
  playableSide?: 'white' | 'black';
  pieceMoveAttemptHandler: (from: string, to: string) => boolean;
}

export const AnalisysBoard = (props : AnalisysBoardProps) => {
  const PieceDropHandler = (args: PieceDropHandlerArgs) => {
    const from = args.sourceSquare;
    const to = args.targetSquare;
    if(!to) return false;
    
    const isValid = props.pieceMoveAttemptHandler(from, to);
    console.log(args);
    return isValid;
  }
  
  const side = props.playableSide ? props.playableSide : 'white';
  const options : ChessboardOptions = 
  {
    ...vectorOptions,
    id: "PlayBoard",
    showNotation: true,
    onPieceDrop: PieceDropHandler,
    position: props.position,
    boardOrientation: side,
  }
  return(
    <div className = "play-board-container" style={{width: `800px`, height: `800px`}}>
      <Chessboard options={options} />
    </div>
  )
}