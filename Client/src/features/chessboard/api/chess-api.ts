import { apiClient } from "@shared/api/ApiClient"

export const chessApi = {
    getEvaluation: async (fen: string): Promise<number> => {
        const response =await apiClient.post(`http://localhost:5004/ChessEngine/GetBestMove`);
        return parseInt(response.data) ;
    },
    getBestMove: async (fen: string, elo: number): Promise<string> => {
        const arg = {"fen": fen, "elo": elo, "moveTime": 2000};
        console.log(arg);
        const response = await apiClient.post(`http://localhost:5004/ChessEngine/GetBestMove`, arg);
        console.log(response.data.bestMove);
        return response.data.bestMove;
    }
}