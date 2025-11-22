import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import type {ChessMessage, ChessMove, UserDTO} from '@features/chessboard';
const MyComponent = () => {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
    const args : ChessMessage = {
        GameId: "10", 
        MessageType: "Move", 
        Issuer: {
            Id: "1",
        },
        Move: {
            San: "e2e4" 
        }
    }

    
    const testMessage = () => {
        if (connection && connection.state === signalR.HubConnectionState.Connected) {
        connection.invoke('OnClientGameMessage',  args)
            .catch(err => console.error(err));
        }
    }
    useEffect(() => {
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5014/GoinGame")
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information) 
            .build();

        setConnection(newConnection);

        newConnection.start()
            .then(() => {
                console.log('SignalR Connected!');
            })
            .catch(err => console.error('Connection failed: ', err));
        
        return () => {
            newConnection.stop();
        };
    }, []);
    useEffect(() => {
    if (connection) {
        connection.on('ReceiveMessage', (user, message) => {
            console.log(`${user}: ${message}`);
        });

        return () => {
            connection.off('ReceiveMessage');
        };
    }
}, [connection]);
    return <button onClick={testMessage}/>;
};

export default MyComponent;