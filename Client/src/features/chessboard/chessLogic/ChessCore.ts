
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
  public makeMove(from: string, to: string, promotionTo?: 'q'| 'n' | 'b' |'r' ): { success: boolean; error?: string; san?: string } {
    try {
      const result = this.game.move({ from, to, promotion: promotionTo});
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

  public isPromotion(from: string, to: string): boolean {
    const pieceType = this.game.get(from)?.type;
    const color = this.game.get(from)?.color;

    if(pieceType !== 'p') return false;

    const rank = to[1];

    if (color === 'w' && rank === '8') return true; 
    if (color === 'b' && rank === '1') return true; 
  
  return false;
  }
}