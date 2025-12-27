using BotService.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChessEngineService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessEngineController : ControllerBase
    {
        private readonly string _stockfishPath;

        public ChessEngineController(IConfiguration configuration)
        {
            _stockfishPath = "./Stockfish/stockfish/stockfish-windows-x86-64-avx2.exe";
        }

        // GET /ChessEngine/GetEvaluation?fen=...
        [HttpGet("GetEvaluation")]
        public async Task<IActionResult> GetEvaluation(string fen, int maxThinkTimeMs = 1000)
        {
            try
            {
                using var stockfishProcess = new Process(); 
                stockfishProcess.StartInfo.FileName = _stockfishPath;
                stockfishProcess.StartInfo.UseShellExecute = false;
                stockfishProcess.StartInfo.RedirectStandardInput = true;
                stockfishProcess.StartInfo.RedirectStandardOutput = true;
                stockfishProcess.StartInfo.CreateNoWindow = true;

                stockfishProcess.Start();

                await stockfishProcess.StandardInput.WriteLineAsync("uci");
                await WaitForOutputAsync(stockfishProcess, "uciok");

                await stockfishProcess.StandardInput.WriteLineAsync("ucinewgame");

                await stockfishProcess.StandardInput.WriteLineAsync($"position fen {fen}");
                await stockfishProcess.StandardInput.WriteLineAsync($"go movetime {maxThinkTimeMs}");

                string lastInfoLine = "";
                while (!stockfishProcess.HasExited)
                {
                    string line = await stockfishProcess.StandardOutput.ReadLineAsync();
                    if (line == null) break;

                    if (line.StartsWith("info") && (line.Contains("score cp") || line.Contains("score mate")))
                    {
                        lastInfoLine = line; 
                    }

                    if (line.StartsWith("bestmove"))
                    {
                        break;
                    }
                }

                await stockfishProcess.StandardInput.WriteLineAsync("quit");
                stockfishProcess.WaitForExit(1000);

                string evaluation = ParseEvaluationFromInfoLine(lastInfoLine);
                return Ok(new { fen, evaluation, thinkTimeMs = maxThinkTimeMs });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        private string ParseEvaluationFromInfoLine(string infoLine)
        {
            if (string.IsNullOrEmpty(infoLine))
                return "Evaluation not found";

            var parts = infoLine.Split(' ');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "score")
                {
                    string type = parts[i + 1]; 
                    string valueStr = parts[i + 2]; 

                    if (!int.TryParse(valueStr, out int value))
                        return $"Error during parsing evaluation: {valueStr}";

                    if (type == "cp")
                    {
                        string side = value >= 0 ? "white" : "black";
                        return $"{Math.Abs(value)} santipawns  {side}";
                    }
                    else if (type == "mate")
                    {
                        string side = value > 0 ? "white" : "black";
                        return $"Mate in {Math.Abs(value)} moves ({side})";
                    }
                }
            }
            return "No evaluation was parsed";
        }

        

        private async Task WaitForOutputAsync(Process process, string expectedText)
        {
            while (true)
            {
                string line = await process.StandardOutput.ReadLineAsync();
                if (line?.Contains(expectedText) == true)
                    return;
            }
        }

        [HttpPost("GetBestMove")]
        public async Task<IActionResult> GetBestMove([FromBody] BestMoveRequest request)
        {
            Process stockfishProcess = null;
            try
            {
                stockfishProcess = new Process();
                stockfishProcess.StartInfo.FileName = _stockfishPath;
                stockfishProcess.StartInfo.UseShellExecute = false;
                stockfishProcess.StartInfo.RedirectStandardInput = true;
                stockfishProcess.StartInfo.RedirectStandardOutput = true;
                stockfishProcess.StartInfo.CreateNoWindow = true;

                stockfishProcess.Start();
                if (request.MoveTime <= 300) request.MoveTime = 300;
                await stockfishProcess.StandardInput.WriteLineAsync("uci");
                await WaitForOutputAsync(stockfishProcess, "uciok");

                await stockfishProcess.StandardInput.WriteLineAsync($"setoption name UCI_Elo value {request.Elo}");

                await stockfishProcess.StandardInput.WriteLineAsync("ucinewgame");
                await stockfishProcess.StandardInput.WriteLineAsync($"position fen {request.Fen}");
                await stockfishProcess.StandardInput.WriteLineAsync($"go movetime {request.MoveTime ?? 2000}"); 
                
                string bestMoveLine = "";
                CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromMilliseconds((request.MoveTime ?? 2000) + 500)); 

                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        string line = await stockfishProcess.StandardOutput.ReadLineAsync().WaitAsync(cts.Token);
                        if (line == null) continue;

                        if (line.StartsWith("bestmove"))
                        {
                            bestMoveLine = line;
                            break;
                        }
                    }
                }
                catch (OperationCanceledException)
                {

                }

                string bestMove = "(none)";
                if (!string.IsNullOrEmpty(bestMoveLine))
                {
                    var parts = bestMoveLine.Split(' ');
                    if (parts.Length >= 2 && parts[1] != "(none)")
                    {
                        bestMove = parts[1];
                    }
                }

                return Ok(new
                {
                    fen = request.Fen,
                    elo = request.Elo,
                    bestMove,
                    analysisTimeMs = request.MoveTime ?? 2000
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка обработки: {ex.Message}");
            }
            finally
            {

                if (stockfishProcess != null && !stockfishProcess.HasExited)
                {
                    try
                    {
                        await stockfishProcess.StandardInput.WriteLineAsync("quit");
                        stockfishProcess.WaitForExit(1000);
                        if (!stockfishProcess.HasExited) stockfishProcess.Kill();
                    }
                    catch {}
                }
            }
        }

    }
}