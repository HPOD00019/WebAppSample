import { PlayBoard } from "@features/chessboard/reactChessboard/playBoard";
import { NavPanel } from "./components/nav/NavPanel"
import { TimeControlsPanel } from "./components/TimeControls/TimeControlsPanel";
import { useChessNotationGame } from "@features/chessboard/hooks/useChessNotationGame";
import { useMatchSearch } from "@features/matchSearch/hooks/useMatchSearch";
import { getAccessToken } from "@shared/utils/storage";
import { getSubByJWTtoken } from "@shared/utils/getSubByJWTtoken";
import { useEffect, useState } from "react";
import { Timer } from "./components/timer/timer";
import type { ApiResponse } from "@shared/types/api";
import { Opponent } from "./components/opponent/opponent";
import type { boardStyle } from "@features/chessboard/types/chessboard.types";
import { StylesList } from "@features/chessboard/components/board-style/styles-list";


const checkForActiveMatches =  async (requestForMatch: (request: number) => Promise<ApiResponse<string>>): Promise<string | undefined> => {
    let ans : string | undefined = "";
    ans = (await requestForMatch(0)).data;
    ans = (await requestForMatch(1)).data;
    ans = (await requestForMatch(2)).data;
    ans = (await requestForMatch(3)).data;
    ans = (await requestForMatch(4)).data;
    ans = (await requestForMatch(5)).data;
    ans = (await requestForMatch(6)).data;
    return ans;
}
export const MatchSearchPage = () => {
    const [chessSet, setChessSet] = useState<boardStyle>('classical');
    
    const [connectionString, ChangeConnectionString] = useState("");
    const [whiteTime, ChangeWhiteTime] = useState(0);
    const [blackTime, ChangeBlackTime] = useState(0);
    const [isWhitePlayableSide, changePlayableSide] = useState(true);

    const resetWhiteClock = (milliseconds: number): void => {
        console.log('white time: ' + milliseconds.toString());
        ChangeWhiteTime(milliseconds);
    }
    const resetBlackClock = (milliseconds: number): void  => {
        console.log('black time: ' + milliseconds.toString());
        ChangeBlackTime(milliseconds);
    }
    const onMatchFound = (link:string) => {
        console.log("MATCH FOUND");
        console.log(link);
        ChangeConnectionString(link);
    }

    
    const accessToken = getAccessToken();
    const id = getSubByJWTtoken(accessToken);
    const [isSearching, startSearch, checkForMatch] = useMatchSearch(onMatchFound, 2000);
    useEffect(() => {
        const check = async (requestForMatch: (request: number) => Promise<ApiResponse<string>>) => {
            const activematchLink = await checkForActiveMatches(requestForMatch);
            if(activematchLink) ChangeConnectionString(activematchLink);
        }
        check(checkForMatch);
    }, []);
    const [gameId, ChangeGameId] = useState(0);
    
    const [fen, GetCurrentPosition, isPromotion,OnMovePiece, FindLegalMoves, ValidateMove] = useChessNotationGame(id,gameId, changePlayableSide,  resetWhiteClock, resetBlackClock, connectionString);
    
    const accountClicked = () => {
        console.log("account clicked!");
    }
    const settingsClicked = () => {
        console.log("settings clicked!");
    }
    const moveAttemptHandler = (from: string, to: string) => {
        console.log("from: " + from);
        console.log("to: " + to);
        const isValid = ValidateMove(from, to);
        if(isValid){
            if(isPromotion(from, to)){
                console.log("Promo");
                OnMovePiece(from, to, 'n');
                return isValid;
            }
            OnMovePiece(from, to);

        }
        return false;
    }
    //there was whiteTime in Timer one
    //blackTime the second one 
    return (
        
        <div className="match-search-page">
            <NavPanel className="match-search-page__nav-panel match-search-page__section" onAccountClicked={accountClicked} onSettingsClicked={settingsClicked}/>
            <div style={{width: '100%',  height: 'fit-content', display:'flex', flexDirection: 'row', padding: '15px'}}>
               <div style={{ marginLeft: 'auto', height: 'fit-content'}}>
                    <div style={{display: 'flex', flexDirection: 'row'}}>
                        <div style={{marginTop: 'auto', marginBottom: 'auto', marginRight: 'auto'}}><Opponent name="Test_User_1" rating={1897}/></div>
                        <Timer totalMilliseconds={10000000}/>    
                    </div>
                    <PlayBoard chessSet={chessSet} playableSide={isWhitePlayableSide? 'white' : 'black'} pieceMoveAttemptHandler={moveAttemptHandler} position={'rnbq1rk1/pp2ppbp/2n3p1/2p5/2BPP3/2P1B3/P3NPPP/R2Q1RK1 w - - 0 11'}/>
                    <div style={{display: 'flex', flexDirection: 'row'}}>
                        <div style={{marginTop: 'auto', marginBottom: 'auto', marginRight: 'auto'}}><Opponent name="Test_User_0" rating={1908}/></div>
                        <Timer totalMilliseconds={1640120}/>    
                    </div>
                </div>
                <StylesList  onStyleClicked={(s: boardStyle) => setChessSet(s)}/>
                <TimeControlsPanel className="match-search-page__time-controls match-search-page__section" controlChoosedHandler={(n:number) => {startSearch(n)}}/>
             
            </div>
            
        </div>
    )
}