import { useEffect, useRef, useState } from "react";
import * as signalR from '@microsoft/signalr';
import type { ChessMessage } from "../types/chessboard.types";

export const useWSconnection = ( userid: string, OnRegister: (isWhite: boolean) => void, OnReceiveMessage: (message: ChessMessage) => void, OnRefreshPosition: (fen: string) => void, connectionEndpoint: string) : [(message: ChessMessage) => void] => {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
    const receiveHandlerRef = useRef(OnReceiveMessage);
    const refreshHandlerRef = useRef(OnRefreshPosition);
    const OnReceive = (message: ChessMessage) => {
        console.log(message);
        if (connection && connection.state === signalR.HubConnectionState.Connected) {
            connection.invoke('OnClientGameMessage',  message).catch(err => console.error(err));
        }
    }
    
    useEffect(() => {
        console.log(connectionEndpoint);
        if(connectionEndpoint != "") 
        {
            const issuerId: number = parseInt(userid);
            const newConnection = new signalR.HubConnectionBuilder()
                .withUrl(connectionEndpoint)
                .withAutomaticReconnect()
                .configureLogging(signalR.LogLevel.Information) 
                .build();            
            setConnection(newConnection);
            newConnection.on('OnServerMessage', (message: ChessMessage) => {
                console.log(message);
                receiveHandlerRef.current(message);
            });
            newConnection.on('SetBoardSide', (isWhite: boolean) => {
                console.log(isWhite);
                OnRegister(isWhite);
            });
            newConnection.on('OnGameEnd', (result: string) => {
                console.log(result);
            });
            newConnection.on("GetCurrentPosition", (position: string) => {
                refreshHandlerRef.current(position);
            });
            const onRegister = (): void => {
                newConnection.invoke('Register', issuerId).catch(err => console.error(err));
            }
            newConnection.start().then(onRegister);
            

            return () => {
                newConnection.off('OnGameEnd');
                newConnection.off('SetBoardSide');
                newConnection.off('GetCurrentPosition');
                newConnection.off('OnServerMessage');
                newConnection.stop();
            }
        }

    }, [connectionEndpoint]);

    return [OnReceive]
}