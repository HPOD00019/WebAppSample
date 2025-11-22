
import { Chess } from 'chess.ts';

export class ChessCore {
  private game: Chess;

  constructor(fen?: string) {
    this.game = new Chess(fen);
  }

  public getLegalMoves(square: string): string[] {
    const moves = this.game.moves({ square, verbose: true });
    return moves.map(move => move.to);
  }

  public makeMove(from: string, to: string): { success: boolean; error?: string; san?: string } {
    try {
      const result = this.game.move({ from, to });
      
      if (result) {
        return { success: true, san: result.san };
      } 
      else {
        return { success: false, error: 'Недопустимый ход' };
      }
    } 
    catch (error) {
      return { success: false, error: (error as Error).message };
    }
  }
  public getFen(): string {
    return this.game.fen();
  }
  
  public reset(): void {
    this.game.reset();
  }
}