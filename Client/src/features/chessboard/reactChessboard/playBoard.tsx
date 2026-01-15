import { Chessboard, type ChessboardOptions, type PieceDropHandlerArgs } from 'react-chessboard';
import type { boardStyle } from '../types/chessboard.types';
import { classicalOptions } from '../sets/classical';
import { vectorOptions } from '../sets/vector';

interface PlayBoardProps{
  chessSet?: boardStyle;
  width?: number;
  boardOrientation?: 'white' | 'black';
  position: string;
  playableSide: 'white' | 'black';
  pieceMoveAttemptHandler: (from: string, to: string) => boolean;
}

export const PlayBoard = (props : PlayBoardProps) => {
  
  const PieceDropHandler = (args: PieceDropHandlerArgs) => {
    const playablePieceColorWhite = props.playableSide == 'white';
    const from = args.sourceSquare;
    const to = args.targetSquare;
    const isColorWhite = args.piece.pieceType[0] === 'w';
    const ans = isColorWhite === playablePieceColorWhite;
    if(!ans)  { console.log(ans); return false};
    if(!to) return false;
    
    const isValid = props.pieceMoveAttemptHandler(from, to);
    console.log(args);
    return isValid;
  }
  let styleOptions: ChessboardOptions = classicalOptions;
  if(props.chessSet){
    if(props.chessSet == 'classical'){
      styleOptions = classicalOptions;
    }
    if(props.chessSet == 'vector'){
      styleOptions = vectorOptions;
    }
  }
  const options : ChessboardOptions = {
    ...styleOptions,
    id: "PlayBoard",
    showNotation: true,
    onPieceDrop: PieceDropHandler,
    position: props.position,
    boardOrientation: props.playableSide,
  }
  return(
    <div className = "play-board-container" style={{width: `800px`, height: `800px`}}>
      <Chessboard options={options}/>
    </div>
  )
}