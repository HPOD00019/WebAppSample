
import {PlayBoard} from '@features/chessboard/index';
import boardimg from "@features/chessboard/assets/board.png";


export const GamePage = () => {
  return(
    <PlayBoard
      boardImgSrc= {boardimg}
      isWhite = {true}
      piecesSetPath='../../../shared/pieceSets'
      onPositionChange={() => {console.log('position changed')}}
    />
    )
}