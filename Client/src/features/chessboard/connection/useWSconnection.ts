import  { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import type {ChessMessage} from '@features/chessboard';
import { generateId } from '@shared/utils/generateId';

export const useWSconnection = (moveHandler: (message: ChessMessage) => void, colorReceivedHandler: (isWhite: boolean) => void, setPosition: (fen: string) => void): [(message: ChessMessage) => void] => {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
    if(moveHandler == null) console.error("sdf");
    
    const SendMessage = (message: ChessMessage) => {
        if (connection && connection.state === signalR.HubConnectionState.Connected) {
        connection.invoke('OnClientGameMessage',  message)
            .catch(err => console.error(err));
        }
    }
    useEffect(() => {
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:8080/GoinGame")
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information) 
            .build();

        setConnection(newConnection);

        newConnection.start()
            .then(() => {
                const id = generateId();
                newConnection.invoke('Register', id)
                    .catch(err => console.error("Register failed: ", err));
            })
            .catch(err => console.error('Connection failed: ', err));
        
        return () => {
            newConnection.stop();
        };
    }, []);
     useEffect(() => {
        if (connection) {
            connection.on('ReceivePlayerColor', (playerIsWhite) => {
                console.log('Received player color:', playerIsWhite);
                colorReceivedHandler(playerIsWhite);
            });
            return () => {
                connection.off('ReceivePlayerColor');
                connection.off('ConnectionEstablished');
            };
        }
    }, [connection]);
    useEffect(() => {
        if (connection) {
            connection.on('ReceiveNewPosition', (newPosition) => {
                setPosition(newPosition);
            });
            return () => {
                connection.off('ReceivePlayerColor');
                connection.off('ConnectionEstablished');
            };
        }
    }, [connection]);
    useEffect(() => {
        if (connection) {
            connection.on('OnServerMessage', (message: ChessMessage) => {
                console.log('TEST_ROOM:', message);
                console.log('GameId:', message.GameId);
                console.log('MessageType:', message.MessageType);
            });

            connection.on('ReceiveMessage', (user, message) => {
                console.log(`${user}: ${message}`);
            });

            return () => {
                connection.off('OnServerMessage');
                connection.off('ReceiveMessage');
            };
        }
    }, [connection]);
    return [SendMessage];
};
