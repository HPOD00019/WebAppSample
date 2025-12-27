import { apiClient } from "@shared/api/ApiClient"

export const chessApi = {
    getEvaluation: async (fen: string): Promise<number> => {
        const response =await apiClient.get(`http://localhost:5001/Auth/Register?Elo=${10}`);
        return parseInt(response.data) ;
    },
    getBestMove: async (fen: string, elo: number): Promise<string> => {
        const response =await apiClient.get(`http://localhost:5001/Auth/Register?Elo=${elo}`);
        return response.data;
    }
}