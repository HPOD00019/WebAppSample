import { chessApi } from "../api/chess-api"

export const useHttpGameConnection = (): [(fen:string, elo:number) => Promise<string>] => {
    const getBestMove = async (fen: string, elo:number): Promise<string> => {
        const move = await chessApi.getBestMove(fen, elo);
        return move;
    }
    
    return [getBestMove]
}