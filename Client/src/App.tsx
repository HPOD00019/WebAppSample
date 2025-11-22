import React from 'react';
import {LoginPage} from '@pages/Auth/index';
import { AuthProvider } from '@app/providers';
import './App.css';

import {PlayBoard} from '@features/chessboard/index';
import type { BoardProps } from '@features/chessboard/index';
import boardimg from "@features/chessboard/assets/board.png";
import whiteRook from '@shared/pieceSets/white_rook.svg';
import blackKnight from '@shared/pieceSets/black_knight.svg';
import blackQueen from '@shared/pieceSets/black_queen.svg';

import type { SquareProps } from '@features/chessboard/components/square';

export function App() {
  const handleLoginSuccess = () => {
    console.log('Login success');
  }
  /*
  return (
    <AuthProvider>
      <div className="App">
        <LoginPage
          onSuccess = {handleLoginSuccess}
        />
      </div>
    </AuthProvider>
    
  );
  */
 /*
  return(
    <PlayBoard
      boardImgSrc= {boardimg}
      squares={[
        
      ]}
      pieces={[ 
          {
              pieceId: 178,
              Position: { x: 4, y: 5 },
              piece: "queen",
              color: "black",
              svgSource: blackQueen
          },
          {
              pieceId: 1,
              Position: { x: 8, y: 8 },
              piece: "rook",
              color: "white",
              svgSource: whiteRook
          },
          
          {
              pieceId: 2,
              Position: { x: 7, y: 7 },
              piece: "knight",
              color: "black",
              svgSource: blackKnight
          },
          {
              pieceId: 3,
              Position: { x: 1, y: 1  },
              piece: "rook",
              color: "white",
              svgSource: whiteRook
          },
          {
              pieceId: 4,
              Position: { x: 6, y: 7  },
              piece: "rook",
              color: "white",
              svgSource: whiteRook
          },
          {
              pieceId: 5,
              Position: { x: 7, y: 6  },
              piece: "knight",
              color: "black",
              svgSource: blackKnight
          }
      ]}
      onPositionChange={() => {console.log('position changed')}}
    />
    )
    */
}

export default App;