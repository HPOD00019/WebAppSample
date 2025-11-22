import React from 'react';
import {LoginPage} from '@pages/Auth/index';
import { AuthProvider } from '@app/providers';

import {PlayBoard} from '@features/chessboard/index';
import type { BoardProps } from '@features/chessboard/index';
import boardimg from "@features/chessboard/assets/board.png";
import whiteRook from '@shared/pieceSets/white_rook.svg';
import blackKnight from '@shared/pieceSets/black_knight.svg';
import blackQueen from '@shared/pieceSets/black_queen.svg';

import type { SquareProps } from '@features/chessboard/components/square';


export const GamePage = () => {
  return(
    <PlayBoard
      boardImgSrc= {boardimg}
      piecesSetPath='../../../shared/pieceSets'
      onPositionChange={() => {console.log('position changed')}}
    />
    )
}